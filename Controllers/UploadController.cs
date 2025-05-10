using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using KinadecWebsite.Data;
using KinadecWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KinadecWebsite.Controllers
{
	public class UploadController : Controller
	{


		private readonly Cloudinary _cloudinary;
		private readonly AppDbContext _context;

		public UploadController(IOptions<CloudinarySettings> config, AppDbContext context)
		{
			var acc = new Account(
				config.Value.CloudName,
				config.Value.ApiKey,
				config.Value.ApiSecret
			);

			_cloudinary = new Cloudinary(acc);
			_context = context;
		}

		[HttpGet]
		public IActionResult UploadImage()
		{
			return View();
		}

		//[HttpPost]
		//public async Task<IActionResult> UploadImage(IFormFile file)
		//{
		//    if (file == null || file.Length == 0)
		//    {
		//        ViewBag.Message = "Please select a file.";
		//        return View();
		//    }

		//    await using var stream = file.OpenReadStream();

		//    var uploadParams = new ImageUploadParams
		//    {
		//        File = new FileDescription(file.FileName, stream)
		//    };

		//    var uploadResult = _cloudinary.Upload(uploadParams);

		//    var metadata = new ImageMetadata
		//    {
		//        PublicId = uploadResult.PublicId,
		//        Url = uploadResult.SecureUrl.ToString(),
		//        Format = uploadResult.Format,
		//        ResourceType = uploadResult.ResourceType,
		//        CreatedAt = DateTime.UtcNow
		//    };

		//    _context.ImageMetadata.Add(metadata);
		//    await _context.SaveChangesAsync();

		//    ViewBag.Message = "Image uploaded successfully!";
		//    ViewBag.ImageUrl = metadata.Url;

		//    return View();
		//}


		[HttpPost]
		public async Task<IActionResult> UploadImage(IFormFile file, string name)
		{
			if (file == null || file.Length == 0)
			{
				ViewBag.Message = "Please select an image.";
				return View();
			}

			using var stream = file.OpenReadStream();
			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(file.FileName, stream)
			};
			var result = _cloudinary.Upload(uploadParams);

			var metadata = new ImageMetadata
			{
				PublicId = result.PublicId,
				Url = result.SecureUrl.ToString(),
				Format = result.Format,
				ResourceType = result.ResourceType,
				CreatedAt = DateTime.UtcNow,
				Name = name
			};

			_context.ImageMetadata.Add(metadata);
			await _context.SaveChangesAsync();

			ViewBag.Message = "Image uploaded!";
			return View();
		}



		[HttpGet]
		public IActionResult UploadMultiple()
		{
			return View();
		}



		[HttpPost]
		public async Task<IActionResult> UploadMultiple(List<IFormFile> files)
		{
			if (files == null || !files.Any())
			{
				ViewBag.Message = "Please select files.";
				return View();
			}

			foreach (var file in files)
			{
				await using var stream = file.OpenReadStream();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(file.FileName, stream)
				};
				var result = _cloudinary.Upload(uploadParams);

				var metadata = new ImageMetadata
				{
					PublicId = result.PublicId,
					Url = result.SecureUrl.ToString(),
					Format = result.Format,
					ResourceType = result.ResourceType,
					CreatedAt = DateTime.UtcNow
				};

				_context.ImageMetadata.Add(metadata);
			}

			await _context.SaveChangesAsync();

			ViewBag.Message = "All images uploaded successfully!";
			return View();
		}


		public async Task<IActionResult> ImageDetails(int id)
		{
			var image = await _context.ImageMetadata.FindAsync(id);
			if (image == null)
				return NotFound();

			return View(image);
		}



		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var image = await _context.ImageMetadata.FindAsync(id);
			if (image == null) return NotFound();
			return View(image);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ImageMetadata updated)
		{
			var image = await _context.ImageMetadata.FindAsync(updated.Id);
			if (image == null) return NotFound();

			image.Name = updated.Name;
			await _context.SaveChangesAsync();

			TempData["Message"] = "Image updated!";
			return RedirectToAction("Index");
		}

	}
}
