using CarDealership.ViewModels.Models.Car;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services.Interfaces;
using CarDealershipApp.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICarImageService carImageService;
        private readonly IWebHostEnvironment _env;





        public CarService
            (IRepository<Car> repository,
            IRepository<Brand> brandRepository,
            IRepository<Category> categoryRepository,
            IRepository<Feature> featureRepository,
            IRepository<Model> modelsRepository,
            IRepository<UserCar> usersCarsRepository,
            ICarImageService carImageService,
            IWebHostEnvironment env)
        {
            this.carRepository = repository;
            this.brandRepository = brandRepository;
            this.categoryRepository = categoryRepository;
            this.featureRepository = featureRepository;
            this.modelsRepository = modelsRepository;
            this.usersCarsRepository = usersCarsRepository;
            this.carImageService = carImageService;
            this._env = env;
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
                                                 int pageSize = 5,
                                                 string? userId = null)
        {


            IQueryable<Car> cars = carRepository
                .GetAllQueryable()
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Brand)
                .Include(x => x.Model)
                .Include(x => x.Images)
                .Include(x => x.UsersCars)
                .OrderBy(x => x.Brand.Name)
                .ThenBy(x => x.Model.Name)
                .ThenByDescending(x => x.Price);



            if (!string.IsNullOrWhiteSpace(brandName))
            {
                brandName = brandName.ToLower().Trim();
                cars = cars.Where(x => x.Brand.Name.ToLower() == brandName.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(modelName))
            {
                modelName = modelName.ToLower().Trim();
                cars = cars.Where(x => x.Model.Name.ToLower() == modelName.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                category = category.ToLower().Trim();
                cars = cars.Where(x => x.Category.Name.ToLower() == category.ToLower());
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
                if (car.Images.Count() == 0)
                {
                    car.Images = new List<CarImage>() { };
                }
                string? currentUserId = userId;
                CarPreview carPreview = new CarPreview()
                {
                    SellerId = car.SellerId,
                    CarId = car.Id,
                    BrandName = car.Brand.Name,
                    ModelName = car.Model.Name,
                    Price = car.Price,
                    MainImage = car.Images.FirstOrDefault(),
                    Year = car.ReleaseYear,
                    MileageInKm = car.Mileage,
                };
                carPreview.IsSavedByCurrentUser = currentUserId != null &&
                    car.UsersCars.Any(uc => uc.UserId == currentUserId);
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
                .Include(x => x.Images)
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
                    MainImage = car.Images.FirstOrDefault(),
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
                    Id = x.Id,
                    SellerId = x.SellerId,
                    BrandName = x.Brand.Name,
                    ModelName = x.Model.Name,
                    SellerEmail = x.Seller.NormalizedEmail ?? "No Email",
                    CategoryName = x.Category.Name,
                    Price = x.Price,
                    Weight = x.Weight.ToString(),
                    Description = x.Description,
                    Mileage = x.Mileage,
                    Images = x.Images ?? new List<CarImage> { new CarImage { FilePath = "No Image", CarId = carId } },
                    ReleaseYear = x.ReleaseYear,
                    ListedOn = x.ListedOn.ToString("yyyy-MM-dd"),
                    CarFeatures = x.CarsFeatures
                        .Select(cf => cf.Feature.Name)
                        .OrderBy(featureName => featureName)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return carDetails;
        }

        public async Task AddCarAsync(CarAddViewModel viewModel)
        {
            Brand? brand = await brandRepository.GetByIdAsync(viewModel.BrandId);
            if (brand is null) throw new ArgumentException("Select one of the available brands!");

            string? sellerId = viewModel.SellerId;
            if (sellerId is null) throw new ArgumentException("No user!");

            var selectedFeatures = viewModel.SelectedFeaturesIds
                .Select(id => new CarFeature { FeatureId = id })
                .ToList();

            var carToAdd = new Car
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
                ReleaseYear = viewModel.ReleaseYear,
                ListedOn = viewModel.ListedOn,
                CarsFeatures = selectedFeatures
            };

            // Add car first to obtain Id
            await carRepository.AddAsync(carToAdd);

            // handle images via service - this will store DB CarImage rows and disk files
            if (viewModel.Images != null && viewModel.Images.Any(f => f?.Length > 0))
            {
                await carImageService.AddRangeAsync(carToAdd.Id, viewModel.Images.Where(f => f != null && f.Length > 0), _env.WebRootPath);
            }
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
                .Include(x => x.Images)
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
                Weight = selectedCar.Weight,
                ExistingImages = selectedCar.Images // only for display
            };
            //while (viewModel.Images.Count < 4)
            //{
            //    viewModel.Images.Add(null);
            //}
            viewModel.AvailableFeatures = await featureRepository.GetAllAsync();
            viewModel.AvailableCategories = await categoryRepository.GetAllAsync();
            return viewModel;
        }

        public async Task EditCarAsync(CarEditViewModel viewModel)
        {
            // Load the car with collections
            var carToBeEdited = await carRepository
                .GetAllQueryable()
                .Include(c => c.CarsFeatures)
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == viewModel.Id);

            if (carToBeEdited == null || carToBeEdited.IsDeleted)
            {
                throw new ArgumentException("There is no such car.");
            }

            // Update scalar properties 
            carToBeEdited.CategoryId = viewModel.CategoryId;
            carToBeEdited.Weight = viewModel.Weight;
            carToBeEdited.Description = viewModel.Description;
            carToBeEdited.Mileage = viewModel.Mileage;

            // Update features safely 
            var selectedFeatureIds = viewModel.SelectedFeaturesIds ?? new List<int>();
            var existingFeatureIds = carToBeEdited.CarsFeatures
                .Select(cf => cf.FeatureId)
                .ToHashSet();

            // Features to add
            var toAdd = selectedFeatureIds.Except(existingFeatureIds).ToList();

            // Feature link entities to remove
            var toRemove = carToBeEdited.CarsFeatures
                .Where(cf => !selectedFeatureIds.Contains(cf.FeatureId))
                .ToList();

            // Remove unselected links (from the collection => EF will mark them Deleted)
            foreach (var rem in toRemove)
            {
                carToBeEdited.CarsFeatures.Remove(rem);
            }

            // Add newly selected links
            foreach (var fid in toAdd)
            {
                // create new link (EF will Insert this)
                carToBeEdited.CarsFeatures.Add(new CarFeature
                {
                    CarId = carToBeEdited.Id,
                    FeatureId = fid
                });
            }

            // Prepare image removals 
            var imagePathsToDelete = new List<string>();
            var removeIds = viewModel.ImageIdsToRemove ?? new List<int>();

            if (removeIds.Any())
            {
                foreach (var remId in removeIds)
                {
                    // Delete by image id via service (will delete DB record and file)
                    await carImageService.DeleteByCarImageAsync(remId, _env.WebRootPath);
                    // Also remove from EF tracked collection so the in-memory entity is in sync
                    var imgEntity = carToBeEdited.Images.FirstOrDefault(i => i.Id == remId);
                    if (imgEntity != null)
                        carToBeEdited.Images.Remove(imgEntity);
                }
            }

            // Adding new uploads: delegate to service (service will create CarImage entries)
            if (viewModel.Images != null && viewModel.Images.Any(f => f != null && f.Length > 0))
            {
                // Note: if car is already tracked, AddRangeAsync will insert new rows referencing car.Id
                await carImageService.AddRangeAsync(carToBeEdited.Id, viewModel.Images.Where(f => f != null && f.Length > 0), _env.WebRootPath);
            }

            // Persist other changes
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
                .Where(x => x.CarId == carId)
                .ToListAsync();

            if (car is null)
            {
                return false;
            }

            if (car.IsDeleted == false)
            {
                car.IsDeleted = true;

                await carImageService.DeleteByCarIdAsync(carId, _env.WebRootPath);

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

        public Task<bool> IsCarSavedByUserAsync(int carId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
