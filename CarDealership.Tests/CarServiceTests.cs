using Moq;
using NUnit.Framework;
using CarDealershipApp.Services;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.ViewModels.CarViewModels;
using CarDealershipApp.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MockQueryable;
using CarDealershipApp.Services.Interfaces;
using CarDealership.ViewModels.Models.Car;

namespace CarDealershipApp.Tests
{
	public class CarServiceTests
	{
		private Mock<IRepository<Car>> mockCarRepo;
		private Mock<IRepository<Brand>> mockBrandRepo;
		private Mock<IRepository<Category>> mockCategoryRepo;
		private Mock<IRepository<Feature>> mockFeatureRepo;
		private Mock<IRepository<Model>> mockModelRepo;
		private Mock<IRepository<UserCar>> mockUserCarRepo;
		private CarService carService;

		[SetUp]
		public void Setup()
		{
			mockCarRepo = new Mock<IRepository<Car>>();
			mockBrandRepo = new Mock<IRepository<Brand>>();
			mockCategoryRepo = new Mock<IRepository<Category>>();
			mockFeatureRepo = new Mock<IRepository<Feature>>();
			mockModelRepo = new Mock<IRepository<Model>>();
			mockUserCarRepo = new Mock<IRepository<UserCar>>();

			carService = new CarService(
				mockCarRepo.Object,
				mockBrandRepo.Object,
				mockCategoryRepo.Object,
				mockFeatureRepo.Object,
				mockModelRepo.Object,
				mockUserCarRepo.Object
			);
		}

		[Test]
		public async Task GetCarOwnerIdAsync_ReturnsCorrectSellerId()
		{
			// Arrange
			int carId = 1;
			var car = new Car { Id = carId, SellerId = "seller123" };
			mockCarRepo.Setup(repo => repo.GetByIdAsync(carId)).ReturnsAsync(car);

			// Act
			var result = await carService.GetCarOwnerIdAsync(carId);

			// Assert
			Assert.That(result, Is.EqualTo("seller123"));
		}

		[Test]
		public async Task CheckAllCarsAsync_ReturnsCorrectCars()
		{
			// Arrange
			var carList = new List<Car>
			{
				new Car { Id = 1, Brand = new Brand { Name = "Toyota" }, Model = new Model { Name = "Corolla" }, Price = 20000 },
				new Car { Id = 2, Brand = new Brand { Name = "Ford" }, Model = new Model { Name = "Focus" }, Price = 18000 }
			};

			mockCarRepo.Setup(repo => repo.GetAllQueryable()).Returns(carList.BuildMock());

			// Act
			var result = await carService.CheckAllCarsAsync();

			// Assert
			Assert.That(result.Cars.Count, Is.EqualTo(2));
			Assert.That(result.Cars[0].BrandName, Is.EqualTo("Ford"));
			Assert.That(result.Cars[1].ModelName, Is.EqualTo("Corolla"));
		}

		[Test]
		public async Task AddCarAsync_AddsCarSuccessfully()
		{
			// Arrange
			var carAddViewModel = new CarAddViewModel
			{
				BrandId = 1,
				ModelId = 1,
				CategoryId = 1,
				Price = 15000,
				Weight = 1200,
				Description = "Test car",
				Mileage = 10000,
				ReleaseYear = 2020,
				SellerId = "user123"
			};

			var brand = new Brand { Id = 1, Name = "Toyota" };
			mockBrandRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(brand);

			// Act
			await carService.AddCarAsync(carAddViewModel);

			// Assert
			mockCarRepo.Verify(repo => repo.AddAsync(It.IsAny<Car>()), Times.Once);
		}

		[Test]
		public async Task SoftDeleteAsync_DeletesCarSuccessfully()
		{
			// Arrange
			int carId = 1;
			var car = new Car { Id = carId, IsDeleted = false, SellerId = "user123" };
			mockCarRepo.Setup(repo => repo.GetByIdAsync(carId)).ReturnsAsync(car);
			mockUserCarRepo.Setup(repo => repo.GetAllQueryable()).Returns(new List<UserCar>().BuildMock());

			// Act
			var result = await carService.SoftDeleteAsync(carId);

			// Assert
			mockCarRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Car>()), Times.Once);
			Assert.IsTrue(car.IsDeleted);
		}
		[Test]
		public async Task GetAllBrandsAsync_ShouldReturnAllBrands_WhenBrandsExist()
		{
			// Arrange
			var brandsData = new List<Brand>
			{
				new Brand { Id = 1, Name = "Toyota" },
				new Brand { Id = 2, Name = "Ford" }
			};


			mockBrandRepo
				.Setup(repo => repo.GetAllQueryable())
				.Returns(brandsData.BuildMock());  

			// Act
			var result = await carService.GetAllBrandsAsync();

			// Assert
			Assert.IsNotNull(result);  
			Assert.That(result.Count, Is.EqualTo(2));  
			Assert.That(result.First().Name, Is.EqualTo("Toyota"));  
			Assert.That(result.Last().Name, Is.EqualTo("Ford"));  
		}
		[Test]
		public async Task EditCarAsync_ShouldEditCar_WhenCarExists()
		{
			// Arrange
			var viewModel = new CarEditViewModel
			{
				Id = 1,
				CategoryId = 2,
				Weight = 1500,
				Description = "Updated description",
				Mileage = 5000,
				ImageUrls = new List<string> { "image1.jpg", "image2.jpg" },
				SelectedFeaturesIds = new List<int> { 101, 102 }
			};

			var carToBeEdited = new Car
			{
				Id = 1,
				CategoryId = 1,
				Weight = 1400,
				Description = "Old description",
				Mileage = 3000,
				ImageUrls = new List<string> { "oldImage1.jpg" },
				CarsFeatures = new List<CarFeature>(),
				IsDeleted = false
			};

			mockCarRepo
				.Setup(repo => repo.GetByIdAsync(viewModel.Id))
				.ReturnsAsync(carToBeEdited);

			mockCarRepo
				.Setup(repo => repo.UpdateAsync(It.IsAny<Car>()))
				.ReturnsAsync(true);

			// Act
			await carService.EditCarAsync(viewModel);

			// Assert
			Assert.That(carToBeEdited.CategoryId, Is.EqualTo(viewModel.CategoryId));
			Assert.That(carToBeEdited.Weight, Is.EqualTo(viewModel.Weight));
			Assert.That(carToBeEdited.Description, Is.EqualTo(viewModel.Description));
			Assert.That(carToBeEdited.Mileage, Is.EqualTo(viewModel.Mileage));
			CollectionAssert.AreEqual(viewModel.ImageUrls, carToBeEdited.ImageUrls);

			Assert.That(carToBeEdited.CarsFeatures.Count, Is.EqualTo(viewModel.SelectedFeaturesIds.Count));
			Assert.IsTrue(carToBeEdited.CarsFeatures.All(cf =>
				viewModel.SelectedFeaturesIds.Contains(cf.FeatureId)));

			mockCarRepo.Verify(repo => repo.UpdateAsync(carToBeEdited), Times.Once);
		}

		[Test]
		public void EditCarAsync_ShouldThrowArgumentException_WhenCarDoesNotExist()
		{
			// Arrange
			var viewModel = new CarEditViewModel
			{
				Id = 99, // Non-existent car ID
				CategoryId = 2,
				Weight = 1500,
				Description = "Updated description",
				Mileage = 5000,
				ImageUrls = new List<string> { "image1.jpg", "image2.jpg" },
				SelectedFeaturesIds = new List<int> { 101, 102 }
			};

			mockCarRepo
				.Setup(repo => repo.GetByIdAsync(viewModel.Id))
				.ReturnsAsync((Car)null);

			// Act & Assert
			var exception = Assert.ThrowsAsync<ArgumentException>(
				async () => await carService.EditCarAsync(viewModel));

			Assert.That(exception.Message, Is.EqualTo("There is no such car."));
		}

		[Test]
		public async Task GetAllBrandsAsync_ShouldReturnEmptyList_WhenNoBrandsExist()
		{
			// Arrange
			var brandsData = new List<Brand>();  

			
			mockBrandRepo
				.Setup(repo => repo.GetAllQueryable())
				.Returns(brandsData.BuildMock());  

			// Act
			var result = await carService.GetAllBrandsAsync();

			// Assert
			Assert.IsNotNull(result); 
			Assert.That(result.Count, Is.EqualTo(0));  
		}
		[Test]
		public async Task GetCarsCountWithModelId_ShouldReturnCorrectCount_WhenCarsExistWithGivenModelId()
		{
			// Arrange
			int modelId = 1;
			var carsData = new List<Car>
			{
				new Car { Id = 1, ModelId = 1, IsDeleted = false },
				new Car { Id = 2, ModelId = 1, IsDeleted = false },
				new Car { Id = 3, ModelId = 2, IsDeleted = false }
			};

			// Mock the GetAllQueryable method to return the list of cars
			mockCarRepo
				.Setup(repo => repo.GetAllQueryable())
				.Returns(carsData.BuildMock());  // Use MockQueryable to simulate IQueryable

			// Act
			var result = await carService.GetCarsCountWithModelId(modelId);

			// Assert
			Assert.That(result, Is.EqualTo(2));  // Ensure the count is 2 (cars with ModelId = 1)
		}

		[Test]
		public async Task GetCarsCountWithModelId_ShouldReturnZero_WhenNoCarsExistWithGivenModelId()
		{
			// Arrange
			int modelId = 1;
			var carsData = new List<Car>
			{
				new Car { Id = 1, ModelId = 2, IsDeleted = false },
				new Car { Id = 2, ModelId = 2, IsDeleted = false }
			};

			mockCarRepo
				.Setup(repo => repo.GetAllQueryable())
				.Returns(carsData.BuildMock());  

			// Act
			var result = await carService.GetCarsCountWithModelId(modelId);

			// Assert
			Assert.That(result, Is.EqualTo(0));
		}

		[Test]
		public async Task GetCarsCountWithModelId_ShouldReturnZero_WhenCarsAreDeleted()
		{
			// Arrange
			int modelId = 1;
			var carsData = new List<Car>
			{
				new Car { Id = 1, ModelId = 1, IsDeleted = true },
				new Car { Id = 2, ModelId = 1, IsDeleted = false }
			};

			mockCarRepo
				.Setup(repo => repo.GetAllQueryable())
				.Returns(carsData.BuildMock());  

			// Act
			var result = await carService.GetCarsCountWithModelId(modelId);

			// Assert
			Assert.That(result, Is.EqualTo(1));  
		}

	}
}
