﻿using CarDealershipApp.ViewModels.CarViewModels;
using CarDealershipApp.Services;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarDealership.ViewModels.Models.Car;
using CarDealershipApp.Data.Models;
using System.Security.Claims;
using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Controllers
{
	[Authorize(Roles = "User, Admin")]
	public class CarController : Controller
	{
		private readonly ICarService carService;
		public CarController(ICarService carServ)
		{
			this.carService = carServ;
		}
		public IActionResult Index()
		{
			return RedirectToAction(nameof(Cars));
		}
		[HttpGet]
		public async Task<IActionResult> Add(int brandId)
		{
			if (brandId <= 0)
			{
				return RedirectToAction(nameof(SelectBrand));
			}
			CarAddViewModel viewModel = await carService.InitializeCarAddViewModelAsync(brandId, GetCurrentUserId());


			return View(nameof(Add), viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CarAddViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel = await carService.InitializeCarAddViewModelAsync(viewModel.BrandId, GetCurrentUserId());
				return View(viewModel);
			}
			await carService.AddCarAsync(viewModel);
			return RedirectToAction(nameof(Cars));
			//return View(viewModel);
		}


		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			CarDetailsViewModel details = await carService.LoadDetailsAsync(id);
			if (details is null)
			{
				return RedirectToAction(nameof(Cars));
			}
			return View(details);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var carOwnerId = await carService.GetCarOwnerIdAsync(id);
			var currentUserId = GetCurrentUserId();
			if (!User.IsInRole("Admin") && carOwnerId != currentUserId)
			{
				return StatusCode(403);
			}
			await carService.SoftDeleteAsync(id);
			return RedirectToAction(nameof(Cars));
		}
		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Cars(string? brandName = null,
												 string? modelName = null,
												 string? category = null,
												 int? minReleaseYear = null,
												 int? maxReleaseYear = null,
												 int pageNumber = 1,
												 int pageSize = 5)
		{
			
				(List<CarPreview> Cars, int TotalPages) carsToSee = 
				await carService.CheckAllCarsAsync(brandName,
				modelName,
				category,
				minReleaseYear,
				maxReleaseYear,
				pageNumber,
				pageSize);

			if (minReleaseYear < CarMinYear)
			{
				//TempData["YearErrorMessage"] = "Enter valid filter years.";
				minReleaseYear = CarMinYear;
			}
			if (maxReleaseYear > CarMaxYear)
			{
				maxReleaseYear = CarMaxYear;
			}
			if (pageNumber > carsToSee.TotalPages)
			{
				pageNumber = carsToSee.TotalPages;
			}

			ViewData["BrandName"] = brandName;
			ViewData["ModelName"] = modelName;
			ViewData["Category"] = category;
			ViewData["MinYear"] = minReleaseYear;
			ViewData["MaxYear"] = maxReleaseYear;
			ViewData["CurrentPage"] = pageNumber;
			ViewData["TotalPages"] = carsToSee.TotalPages;


			return View(carsToSee);
		}
		[HttpGet]
		public async Task<IActionResult> YourCars()
		{
			string? userId = GetCurrentUserId();
			if (userId is null)
			{
				return RedirectToAction(nameof(Cars));
			}

			return View(await carService.CheckYourCars(userId));
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

			if (!ModelState.IsValid)
			{
				viewModel.Brands = await carService.GetAllBrandsAsync();
				return View(viewModel);
			}

			return RedirectToAction(nameof(Add), new { brandId = viewModel.SelectedBrandId });
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var carOwnerId = await carService.GetCarOwnerIdAsync(id);
			var currentUserId = GetCurrentUserId();
			if (!User.IsInRole("Admin") && carOwnerId != currentUserId)
			{
                return StatusCode(403);
            }
            CarEditViewModel viewModel = await carService.InitializeCarEditViewModel(id);
			if (viewModel == null)
			{
				return RedirectToAction(nameof(Index));
			}
			return View(viewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(CarEditViewModel viewModel)
		{
			var carOwnerId = await carService.GetCarOwnerIdAsync(viewModel.Id);
			var currentUserId = GetCurrentUserId();
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}
			if (!User.IsInRole("Admin") && carOwnerId != currentUserId)
			{
				return StatusCode(403);
			}
			await carService.InitializeCarEditViewModel(viewModel.Id);
			if (viewModel is null)
			{
				return View(viewModel);
			}
			await carService.EditCarAsync(viewModel);
			return RedirectToAction(nameof(Details), new { viewModel.Id });
		}


		private string? GetCurrentUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}
