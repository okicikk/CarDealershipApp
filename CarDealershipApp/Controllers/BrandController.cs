using Microsoft.AspNetCore.Mvc;
using CarDealershipApp.Services.Interfaces;
using CarDealership.Web.ViewModels.Models.Brand;
using Microsoft.AspNetCore.Authorization;
using CarDealershipApp.Data.Models;
using NuGet.Versioning;

namespace CarDealershipApp.Controllers
{
	[Authorize(Roles = "User")]
	public class BrandController : Controller
	{
		private readonly IBrandService brandService;
		public BrandController(IBrandService _brandService)
		{
			brandService = _brandService;
		}
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var brands = await brandService
				.GetAllBrandsAsync();
			return View(brands);
		}
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CarsWithBrand(int id)
        {
            string brandName = (await brandService.GetByIdAsync(id)).Name;
            return Redirect($"/Car/Cars?brandName={brandName}&modelName=&category=&minReleaseYear=&maxReleaseYear=");
        }
        [Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Add(BrandAddViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}
			try
			{
				await brandService.AddBrandAsync(viewModel);
			}
			catch (ArgumentException e)
			{
				ModelState.AddModelError(nameof(viewModel.Name), e.Message);
				return View(viewModel);
			}
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			Brand brand = await brandService.GetByIdAsync(id);
			if (brand is null)
			{
				return RedirectToAction(nameof(Index)); 
			}
			BrandIndexViewModel viewModel = new BrandIndexViewModel
			{
				Id = brand.Id,
				Name = brand.Name,
				ImageUrl = brand.ImageUrl
			};
			return View(viewModel);
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Edit(BrandIndexViewModel viewModel)
		{

			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Brand could not be updated! Try again with a different name or url.";
				return View(viewModel);
			}
			await brandService.EditAsync(viewModel);
			TempData["SuccessMessage"] = "Brand updated successfully!";
			return RedirectToAction(nameof(Index));
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			bool success = await brandService.SoftDeleteByIdAsync(id);
			if (success)
			{
				TempData["SuccessMessage"] = "Brand and its models/cars deleted successfully!";
			}
			else
			{
				TempData["ErrorMessage"] = "Brand could not be deleted.";
			}
			return RedirectToAction(nameof(Index));
		}


	}
}
