using KinadecWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KinadecWebsite.Controllers
{
	public class GalleryController : Controller
	{
		private readonly AppDbContext _context;

		public GalleryController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var images = await _context.ImageMetadata
				.OrderByDescending(i => i.CreatedAt)
				.ToListAsync();

			return View(images);
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


		public async Task<IActionResult> Delete(int id)
		{
			var image = await _context.ImageMetadata.FindAsync(id);
			if (image == null) return NotFound();

			_context.ImageMetadata.Remove(image);
			await _context.SaveChangesAsync();

			TempData["Message"] = "Image deleted successfully.";
			return RedirectToAction("Index");
		}
	}
}
