﻿using CarDealershipApp.ViewModels.CarViewModels;
using CarDealershipApp.Services.Interfaces;
using CarDealershipApp.Infrastructure.Repositories;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Data.Models;

using Microsoft.EntityFrameworkCore;

using CarDealership.ViewModels.Models.Car;
using System.Net;
using Microsoft.AspNetCore.Mvc;



namespace CarDealershipApp.Services
{
	public class CarService : ICarService
	{
		private readonly IRepository<Car> carRepository;
		private readonly IRepository<Brand> brandRepository;
		private readonly IRepository<Category> categoryRepository;
		private readonly IRepository<Feature> featureRepository;
		private readonly IRepository<Model> modelsRepository;



		public CarService
			(IRepository<Car> repository,
			IRepository<Brand> brandRepository,
			IRepository<Category> categoryRepository,
			IRepository<Feature> featureRepository,
			IRepository<Model> modelsRepository)
		{
			this.carRepository = repository;
			this.brandRepository = brandRepository;
			this.categoryRepository = categoryRepository;
			this.featureRepository = featureRepository;
			this.modelsRepository = modelsRepository;
		}

		public async Task<List<CarPreview>> CheckAllCarsAsync()
		{
			List<CarPreview> carPreviews = new List<CarPreview>();
			foreach (var car in await carRepository
				.GetAllQueryable()
				.Include(x => x.Brand)
				.Include(x => x.Model)
				.ToListAsync())
			{
				CarPreview carPreview = new CarPreview()
				{
					CarId = car.Id,
					BrandName = car.Brand.Name,
					ModelName = car.Model.Name,
					Price = car.Price,
					MainImage = car.ImageUrls?[0],
					Year = car.ReleaseYear,
					MileageInKm = car.Mileage
				};
				carPreviews.Add(carPreview);
			}
			return carPreviews
				.OrderBy(x => x.BrandName)
				.ThenBy(x => x.ModelName)
				.ThenByDescending(x => x.Price)
				.ToList();
		}
		public async Task<CarDetailsViewModel> LoadDetailsAsync(int carId)
		{
			CarDetailsViewModel? carDetails = await carRepository
				.GetAllQueryable()
				.Where(x => x.Id == carId)
				.Select(x => new CarDetailsViewModel
				{
					BrandName = x.Brand.Name,
					ModelName = x.Model.Name,
					SellerEmail = x.Seller.NormalizedEmail ?? "No Email",
					CategoryName = x.Category.Name,
					Price = x.Price,
					Weight = x.Weight.ToString(),
					Description = x.Description,
					Mileage = x.Mileage,
					ImageUrls = x.ImageUrls ?? new List<string> { "No Image" },
					ReleaseYear = x.ReleaseYear,
					ListedOn = x.ListedOn.ToString("yyyy-MM-dd"),
					CarFeatures = x.CarsFeatures.Select(cf => cf.Feature.Name).ToList()
				})
				.FirstOrDefaultAsync();

			if (carDetails is null)
			{
				throw new ArgumentException("No car was found!");
			}

			return carDetails;
		}

		public async Task AddCarAsync(CarAddViewModel viewModel)
		{

			Brand? brand = await brandRepository.GetByIdAsync(viewModel.BrandId);

			if (brand is null)
			{
				throw new ArgumentException("Select one of the available brands!");
			}


			string? sellerId = viewModel.SellerId;

			if (sellerId is null)
			{
				throw new ArgumentException("No user!");
			}

			var selectedFeatures = viewModel.SelectedFeaturesIds
				.Select(id => new CarFeature
				{
					FeatureId = id
				})
				.ToList();

			Car carToAdd = new Car()
			{
				SellerId = sellerId,
				Brand = brand,
				BrandId = viewModel.BrandId,
				ModelId = viewModel.ModelId,
				CategoryId = viewModel.CategoryId,
				Price = viewModel.Price,
				Weight = viewModel.Weight,
				Description = viewModel.Description ?? "No description!",
				Mileage = viewModel.Mileage,
				ImageUrls = viewModel.ImageUrls,
				ReleaseYear = viewModel.ReleaseYear,
				ListedOn = viewModel.ListedOn,
				CarsFeatures = selectedFeatures
			};
			await carRepository.AddAsync(carToAdd);
		}
		public void AddCar(CarAddViewModel viewModel)
		{

			Brand? brand = brandRepository.GetById(viewModel.BrandId);

			if (brand is null)
			{
				throw new ArgumentException("Select one of the available brands!");
			}


			var selectedFeatures = viewModel.SelectedFeaturesIds
				.Select(id => new CarFeature
				{
					FeatureId = id
				})
				.ToList();

			Car carToAdd = new Car()
			{
				Brand = brand,
				BrandId = viewModel.BrandId,
				ModelId = viewModel.ModelId,
				CategoryId = viewModel.CategoryId,
				Price = viewModel.Price,
				Weight = viewModel.Weight,
				Description = viewModel.Description ?? "No description!",
				Mileage = viewModel.Mileage,
				ImageUrls = viewModel.ImageUrls,
				ReleaseYear = viewModel.ReleaseYear,
				ListedOn = viewModel.ListedOn,
				CarsFeatures = selectedFeatures
			};
			carRepository.Add(carToAdd);
		}

		public async Task<CarAddViewModel> InitializeCarAddViewModelAsync(int brandId, string userId)
		{
			CarAddViewModel viewModel = new CarAddViewModel();
			if (userId is null)
			{
				throw new ArgumentException($"There is no user");
			}

			viewModel.SellerId = userId;

			viewModel.BrandId = brandId;

			viewModel.Models = await modelsRepository
				.GetAllQueryable()
				.Where(x => x.BrandId == brandId)
				.ToListAsync();
			// Load categories (categories don't depend on the brand)
			viewModel.Categories = await categoryRepository.GetAllAsync() ?? new List<Category>();

			// Load available features
			viewModel.AvailableFeatures = await featureRepository.GetAllAsync() ?? new List<Feature>();
			return viewModel;
		}
		public async Task<List<Brand>> GetAllBrandsAsync()
		{
			return await brandRepository
				.GetAllQueryable()
				.ToListAsync();
		}

		public async Task<CarEditViewModel> InitializeCarEditViewModel(int id)
		{
			Car? selectedCar = await carRepository
				.GetAllQueryable()
				.Include(x=>x.CarsFeatures)
				.FirstOrDefaultAsync(x=>x.Id == id);
			if (selectedCar is null)
			{
				throw new ArgumentException("No such car!");
			}

			List<int> featureIds = selectedCar.CarsFeatures.Select(x => x.FeatureId).ToList();

			

            CarEditViewModel viewModel = new CarEditViewModel()
			{
				SelectedFeaturesIds = featureIds,
				CategoryId = selectedCar.CategoryId,
				Description = selectedCar.Description,
				Mileage = selectedCar.Mileage,
				ImageUrls = selectedCar.ImageUrls,
				Weight = selectedCar.Weight,
			};
			while (viewModel.ImageUrls?.Count < 4)
			{
				viewModel.ImageUrls.Add("");
			}
			viewModel.AvailableFeatures = await featureRepository.GetAllAsync();
			viewModel.AvailableCategories = await categoryRepository.GetAllAsync();
			return viewModel;
		}

		public async Task EditCarAsync(CarEditViewModel viewModel)
		{
			Car carToBeEdited = await carRepository.GetByIdAsync(viewModel.Id);
			if (carToBeEdited == null)
			{
				throw new ArgumentException("There is no such car.");
			}

			List<CarFeature> carFeatures = viewModel.SelectedFeaturesIds
				.Select(featureId => new CarFeature
				{
					CarId = carToBeEdited.Id,  
					FeatureId = featureId      
				})
				.ToList();

			carToBeEdited.ImageUrls.Clear();

			carToBeEdited.CategoryId = viewModel.CategoryId;
			carToBeEdited.CarsFeatures = carFeatures;
			carToBeEdited.Weight = viewModel.Weight;
			carToBeEdited.Description = viewModel.Description;
			carToBeEdited.Mileage = viewModel.Mileage;
			carToBeEdited.ImageUrls = viewModel.ImageUrls;

			await carRepository.UpdateAsync(carToBeEdited);
			 
		}
		public async Task<bool> SoftDeleteAsync(int carId)
		{
			Car car = await carRepository.GetByIdAsync(carId);

			if (car.IsDeleted == false)
			{
				car.IsDeleted = true;
				return true;
			}
			return false;
		}
	}
}
