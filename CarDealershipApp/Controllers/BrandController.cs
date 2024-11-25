using Microsoft.AspNetCore.Mvc;
using CarDealershipApp.Services.Interfaces;
using CarDealership.Web.ViewModels.Models.Brand;
using Microsoft.AspNetCore.Authorization;
using CarDealershipApp.Data.Models;

namespace CarDealershipApp.Controllers
{
    [Authorize]
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
        public IActionResult Add()
        {
            return View();
        }
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
                ModelState.AddModelError(nameof(viewModel.Name),e.Message);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Brand brand = await brandService.GetByIdAsync(id);
            if (brand is null)
            {
                RedirectToAction(nameof(Index));
            }
            BrandIndexViewModel viewModel = new BrandIndexViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                ImageUrl = brand.ImageUrl
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BrandIndexViewModel viewModel)
        {
            
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await brandService.EditAsync(viewModel);
            TempData["SuccesMessage"] = "Brand updated successfully!";
            return View(viewModel);
        }
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
