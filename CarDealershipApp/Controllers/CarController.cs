using CarDealershipApp.ViewModels.CarViewModels;
using CarDealershipApp.Services;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using CarDealership.ViewModels.Models.Car;
using CarDealershipApp.Data.Models;
using Microsoft.Extensions.FileProviders.Physical;
using System.Security.Claims;
using Nest;

namespace CarDealershipApp.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        public CarController(ICarService carServ)
        {
            this.carService = carServ;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await carService.LoadDetailsAsync(id));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Cars()
        {
            return View(await carService.CheckAllCarsAsync());
        }
        [HttpGet]
        public async Task<IActionResult> SelectBrand()
        {
            List<Brand> brands = await carService.GetAllBrandsAsync();

            return View(new CarAddBrandSelectionViewModel { Brands = brands });
        }

        [HttpPost]
        public async Task<IActionResult> SelectBrand(CarAddBrandSelectionViewModel viewModel)
        {
            if (viewModel.SelectedBrandId <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.SelectedBrandId), "Select the brand of the car you want to add!");
            }

            // Check if there are any validation errors
            if (!ModelState.IsValid)
            {
                // Repopulate the Brands list before returning to the view
                viewModel.Brands = await carService.GetAllBrandsAsync();
                return View(viewModel);
            }

            return RedirectToAction(nameof(Add), new { brandId = viewModel.SelectedBrandId });
        }
        [HttpGet]
		public async Task<IActionResult> Edit(int id)
        {
            CarEditViewModel viewModel = await carService.InitializeCarEditViewModel(id);
            return View(viewModel);
		}
        [HttpPost]
        public async Task<IActionResult> Edit(CarEditViewModel viewModel)
        {
            await carService.InitializeCarEditViewModel(viewModel.Id);
            if (viewModel is null)
            {
                return View(viewModel);
            }
            await carService.EditCarAsync(viewModel);
            return RedirectToAction(nameof(Details), new{viewModel.Id});
        }
		[HttpGet]
        public async Task<IActionResult> Add(int brandId)
        {
            if (brandId <= 0 )
            {
                return RedirectToAction(nameof(SelectBrand));
            }
            CarAddViewModel viewModel = await carService.InitializeCarAddViewModelAsync(brandId,GetCurrentUserId());


            return View(nameof(Add), viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel = await carService.InitializeCarAddViewModelAsync(viewModel.BrandId,GetCurrentUserId());
                return View(viewModel);
            }
            await carService.AddCarAsync(viewModel);
            return RedirectToAction(nameof(Cars));
            //return View(viewModel);
        }
		public async Task<IActionResult> Delete(int id)
		{
            await carService.SoftDeleteAsync(id);
            return View(nameof(Cars));
		}
		private string? GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
