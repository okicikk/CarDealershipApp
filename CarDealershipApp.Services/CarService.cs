using CarDealershipApp.ViewModels.CarViewModels;
using CarDealershipApp.Services.Interfaces;
using CarDealershipApp.Infrastructure.Repositories;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Data.Models;

using Microsoft.EntityFrameworkCore;

using CarDealership.ViewModels.Models.Car;
using static CarDealershipApp.Constants.Constants;
//using Nest;



namespace CarDealershipApp.Services
{
	public class CarService : ICarService
	{
		private readonly IRepository<Car> carRepository;
		private readonly IRepository<Brand> brandRepository;
		private readonly IRepository<Category> categoryRepository;
		private readonly IRepository<Feature> featureRepository;
		private readonly IRepository<Model> modelsRepository;
		private readonly IRepository<UserCar> usersCarsRepository;





		public CarService
			(IRepository<Car> repository,
			IRepository<Brand> brandRepository,
			IRepository<Category> categoryRepository,
			IRepository<Feature> featureRepository,
			IRepository<Model> modelsRepository,
			IRepository<UserCar> usersCarsRepository)
		{
			this.carRepository = repository;
			this.brandRepository = brandRepository;
			this.categoryRepository = categoryRepository;
			this.featureRepository = featureRepository;
			this.modelsRepository = modelsRepository;
			this.usersCarsRepository = usersCarsRepository;
		}

		public async Task<string> GetCarOwnerIdAsync(int carId)
		{
			Car car = await carRepository.GetByIdAsync(carId);

			return car.SellerId;
		}

		public async Task<(List<CarPreview> Cars, int TotalPages)> CheckAllCarsAsync(string? brandName = null,
												 string? modelName = null,
												 string? category = null,
												 int? minReleaseYear = null,
												 int? maxReleaseYear = null,
												 int pageNumber = 1,
												 int pageSize = 5)
		{


			IQueryable<Car> cars = carRepository
				.GetAllQueryable()
				.Where(x => x.IsDeleted == false)
				.Include(x => x.Brand)
				.Include(x => x.Model)
				.OrderBy(x => x.Brand.Name)
				.ThenBy(x => x.Model.Name)
				.ThenByDescending(x => x.Price);



			if (!string.IsNullOrWhiteSpace(brandName))
			{
				brandName = brandName.ToLower().Trim();
				cars = cars.Where(x => x.Brand.Name == brandName.ToLower());
			}
			if (!string.IsNullOrWhiteSpace(modelName))
			{
				modelName = modelName.ToLower().Trim();
				cars = cars.Where(x => x.Model.Name == modelName.ToLower());
			}
			if (!string.IsNullOrWhiteSpace(category))
			{
				category = category.ToLower().Trim();
				cars = cars.Where(x => x.Category.Name == category.ToLower());
			}
			if (minReleaseYear.HasValue)
			{
				cars = cars.Where(x => x.ReleaseYear >= minReleaseYear);
			}
			if (maxReleaseYear.HasValue)
			{
				cars = cars.Where(x => x.ReleaseYear <= maxReleaseYear);
			}
			if (minReleaseYear < CarMinYear)
			{
				//TempData["YearErrorMessage"] = "Enter valid filter years.";
				minReleaseYear = CarMinYear;
			}
			if (maxReleaseYear > CarMaxYear)
			{
				maxReleaseYear = CarMaxYear;
			}

			int totalCars = cars.Count();
			int totalPages = (int)Math.Ceiling(totalCars / (double)pageSize);


			cars = cars
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize);


			List<CarPreview> carPreviews = new List<CarPreview>();

			foreach (var car in await cars.ToListAsync() ?? new List<Car>())

			{
				CarPreview carPreview = new CarPreview()
				{
					SellerId = car.SellerId,
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
			return (carPreviews
				.ToList(), totalPages);
		}
		public async Task<List<CarPreview>> CheckYourCars(string userId)
		{
			List<CarPreview> carPreviews = new List<CarPreview>();
			foreach (var car in await carRepository
				.GetAllQueryable()
				.Include(x => x.Brand)
				.Include(x => x.Model)
				.Where(x => x.IsDeleted == false && x.SellerId == userId)
				.ToListAsync())
			{
				CarPreview carPreview = new CarPreview()
				{
					SellerId = car.SellerId,
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
				.Where(x => x.Id == carId && x.IsDeleted == false)
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
				.Where(x => x.BrandId == brandId && x.IsDeleted == false)
				.ToListAsync();
			viewModel.Categories = await categoryRepository.GetAllAsync() ?? new List<Category>();

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
				.Where(x => x.IsDeleted == false)
				.Include(x => x.CarsFeatures)
				.FirstOrDefaultAsync(x => x.Id == id);
			if (selectedCar is null)
			{
				return null;
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
			if (carToBeEdited == null || (carToBeEdited.IsDeleted == true))
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
			Car? car = await carRepository
				.GetAllQueryable()
				.Include(x => x.UsersCars)
				.FirstOrDefaultAsync(x => x.Id == carId);
			List<UserCar> usersCarsToBeDeleted = await usersCarsRepository
				.GetAllQueryable()
				.Where(x=>x.CarId == carId)
				.ToListAsync();

			if (car is null)
			{
				return false;
			}

			if (car.IsDeleted == false)
			{
				car.IsDeleted = true;
				
				await carRepository.UpdateAsync(car);
			}
			foreach (var userCar in usersCarsToBeDeleted)
			{
				await usersCarsRepository.DeleteAsync(userCar);
			}
			return false;
		}

		public async Task SoftDeleteCarsByModelId(int modelId)
		{
			List<UserCar> usersCarsToBeDeleted = await usersCarsRepository
				.GetAllQueryable()
				.Where(x => x.Car.ModelId == modelId)
				.ToListAsync();
			foreach (var userCar in usersCarsToBeDeleted)
			{
				await usersCarsRepository.DeleteAsync(userCar);
			}
			foreach (var car in carRepository
				.GetAllQueryable()
				.Where(x => x.ModelId == modelId))
			{
				if (car.IsDeleted == false)
				{
					car.IsDeleted = true;
				}
				await carRepository.UpdateAsync(car);
			}

		}

		public async Task<int> GetCarsCountWithModelId(int modelId)
		{
			return await carRepository
				.GetAllQueryable()
				.Where(x => x.ModelId == modelId && x.IsDeleted == false)
				.CountAsync();
		}
	}
}
