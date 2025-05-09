using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using KinadecWebsite.Data;

namespace KinadecWebsite.Controllers
{



	public class GalleryController : Controller
	{
		private readonly AppDbContext _context;
		private readonly Cloudinary _cloudinary;

		public GalleryController(AppDbContext context, Cloudinary cloudinary)
		{
			_context = context;
			_cloudinary = cloudinary;
		}

		public IActionResult Index()
		{
			var images = _context.ImageMetadata.OrderByDescending(i => i.CreatedAt).ToList();
			return View(images);
		}

		public IActionResult Upload() => View();

		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file, string name)
		{
			if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(file.FileName, file.OpenReadStream()),
				Folder = "gallery"
			};

			var result = await _cloudinary.UploadAsync(uploadParams);

			var image = new ImageMetadata
			{
				PublicId = result.PublicId,
				Name = name,
				Url = result.SecureUrl.ToString(),
				Format = result.Format,
				ResourceType = result.ResourceType,
				CreatedAt = result.CreatedAt
			};

			_context.ImageMetadata.Add(image);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int id)
		{
			var image = _context.ImageMetadata.Find(id);
			return image == null ? NotFound() : View(image);
		}

		public IActionResult Edit(int id)
		{
			var image = _context.ImageMetadata.Find(id);
			return image == null ? NotFound() : View(image);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,PublicId,Name,Url,Format,ResourceType,CreatedAt")] ImageMetadata image)
		{
			if (id != image.Id) return NotFound();

			_context.Update(image);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int id)
		{
			var image = _context.ImageMetadata.Find(id);
			return image == null ? NotFound() : View(image);
		}



		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var image = _context.ImageMetadata.Find(id);
			if (image == null) return NotFound();

			var resourceTypeEnum = image.ResourceType.ToLower() switch
			{
				"video" => ResourceType.Video,
				"raw" => ResourceType.Raw,
				_ => ResourceType.Image // Default case
			};

			var delParams = new DeletionParams(image.PublicId)
			{
				ResourceType = resourceTypeEnum
			};

			await _cloudinary.DestroyAsync(delParams);

			_context.ImageMetadata.Remove(image);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}




		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//	var image = _context.ImageMetadata.Find(id);
		//	if (image == null) return NotFound();

		//	var delParams = new DeletionParams(image.PublicId)
		//	{
		//		ResourceType = image.ResourceType
		//	};
		//	await _cloudinary.DestroyAsync(delParams);

		//	_context.ImageMetadata.Remove(image);
		//	await _context.SaveChangesAsync();
		//	return RedirectToAction(nameof(Index));
		//}
	}
}
