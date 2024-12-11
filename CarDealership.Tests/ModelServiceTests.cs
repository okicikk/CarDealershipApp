using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services;
using CarDealership.ViewModels.Models.Model;
using MockQueryable.Moq;
using CarDealershipApp.Services.Interfaces;
using MockQueryable;

namespace CarDealership.Tests
{
	[TestFixture]
	public class ModelServiceTests
	{
		private Mock<IRepository<Model>> mockModelRepository;
		private Mock<IRepository<Brand>> mockBrandRepository;
		private Mock<ICarService> mockCarService;
		private ModelService _modelService;

		private readonly List<Model> modelsData = new List<Model>
		{
			new Model { Id = 1, Name = "Model X", BrandId = 1, Brand = new Brand { Id = 1, Name = "Tesla" }, IsDeleted = false },
			new Model { Id = 2, Name = "Model S", BrandId = 1, Brand = new Brand { Id = 1, Name = "Tesla" }, IsDeleted = false },
			new Model { Id = 3, Name = "Civic", BrandId = 2, Brand = new Brand { Id = 2, Name = "Honda" }, IsDeleted = false }
		};

		private readonly List<Brand> brandsData = new List<Brand>
		{
			new Brand { Id = 1, Name = "Tesla" },
			new Brand { Id = 2, Name = "Honda" }
		};

		[SetUp]
		public void Setup()
		{
			mockModelRepository = new Mock<IRepository<Model>>();
			mockBrandRepository = new Mock<IRepository<Brand>>();
			mockCarService = new Mock<ICarService>();
			_modelService = new ModelService(mockModelRepository.Object, mockBrandRepository.Object, mockCarService.Object);
		}

        [Test]
        public async Task GetAllModelsAsync_ShouldReturnPaginatedModels()
        {
            // Arrange
            var modelsData = new List<Model>
			{
			    new Model { Id = 1, Name = "Civic", BrandId = 1, Brand = new Brand { Name = "Honda" }, IsDeleted = false },
			    new Model { Id = 2, Name = "Accord", BrandId = 1, Brand = new Brand { Name = "Honda" }, IsDeleted = false },
			    new Model { Id = 3, Name = "Corolla", BrandId = 2, Brand = new Brand { Name = "Toyota" }, IsDeleted = false }
			};

            // Mock the repository
            mockModelRepository
                .Setup(repo => repo.GetAllQueryable())
                .Returns(modelsData.BuildMock());

            mockCarService
                .Setup(service => service.GetCarsCountWithModelId(It.IsAny<int>()))
                .ReturnsAsync(10);

            var pageNumber = 1;
            var pageSize = 2;

            // Act
            var result = await _modelService.GetAllModelsAsync(pageNumber, pageSize);

            // Assert
            Assert.That(result.Item1.Count, Is.EqualTo(2)); 
            Assert.That(result.Item1.First().BrandName, Is.EqualTo("Honda"));
            Assert.That(result.Item1.First().CarsCount, Is.EqualTo(10));
            Assert.That(result.Item2, Is.EqualTo(2));
        }


        [Test]
		public async Task EditModel_ShouldUpdateModel_WhenModelExists()
		{
			// Arrange
			var viewModel = new ModelEditViewModel { Id = 1, Name = "Model 3" };
			var existingModel = new Model { Id = 1, Name = "Model X", BrandId = 1, Brand = brandsData[0], IsDeleted = false };

			mockModelRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(existingModel);

			mockModelRepository
				.Setup(repo => repo.UpdateAsync(It.IsAny<Model>()))
				.Verifiable();

			// Act
			await _modelService.EditModel(viewModel);

			// Assert
			Assert.AreEqual("Model 3", existingModel.Name);
			mockModelRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Model>()), Times.Once);
		}

		[Test]
		public void EditModel_ShouldThrowArgumentException_WhenModelDoesNotExist()
		{
			// Arrange
			var viewModel = new ModelEditViewModel { Id = 999, Name = "Model Y" };

			mockModelRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Model)null);

			// Act & Assert
			var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _modelService.EditModel(viewModel));

			Assert.That(ex.Message, Is.EqualTo("No such model"));
		}

		[Test]
		public async Task AddModelAsync_ShouldAddModel_WhenModelDoesNotExist()
		{
			// Arrange
			var viewModel = new ModelAddViewModel { Name = "Model Z", BrandId = 1 };
			mockBrandRepository
				.Setup(repo => repo.GetAllAsync())
				.ReturnsAsync(brandsData);

			mockModelRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(modelsData.BuildMock());

			mockModelRepository
				.Setup(repo => repo.AddAsync(It.IsAny<Model>()))
				.Returns(Task.CompletedTask);

			// Act
			await _modelService.AddModelAsync(viewModel);

			// Assert
			mockModelRepository.Verify(repo => repo.AddAsync(It.IsAny<Model>()), Times.Once);
		}

		[Test]
		public void AddModelAsync_ShouldThrowArgumentException_WhenModelAlreadyExists()
		{
			// Arrange
			var viewModel = new ModelAddViewModel { Name = "Model X", BrandId = 1 };

			mockBrandRepository
				.Setup(repo => repo.GetAllAsync())
				.ReturnsAsync(brandsData);

			mockModelRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(modelsData.BuildMock());

			// Act & Assert
			var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _modelService.AddModelAsync(viewModel));

			Assert.That(ex.Message, Is.EqualTo("Model with that name already exists!"));
		}

		[Test]
		public async Task GetAllBrandsAsync_ShouldReturnAllBrands()
		{
			// Arrange
			mockBrandRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(brandsData.BuildMock());

			// Act
			var result = await _modelService.GetAllBrandsAsync();

			// Assert
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual("Tesla", result.First().Name);
		}

		[Test]
		public async Task SoftDeleteByIdAsync_ShouldReturnTrue_WhenModelExists()
		{
			// Arrange
			var model = new Model { Id = 1, Name = "Model X", BrandId = 1, Brand = brandsData[0], IsDeleted = false };
			mockModelRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(model);

			mockCarService
				.Setup(service => service.SoftDeleteCarsByModelId(It.IsAny<int>()))
				.Returns(Task.CompletedTask);

			mockModelRepository
				.Setup(repo => repo.Update(It.IsAny<Model>()))
				.Verifiable();

			// Act
			var result = await _modelService.SoftDeleteByIdAsync(1);

			// Assert
			Assert.IsTrue(result);
			Assert.IsTrue(model.IsDeleted);
			mockModelRepository.Verify(repo => repo.Update(It.IsAny<Model>()), Times.Once);
		}

		[Test]
		public async Task SoftDeleteByIdAsync_ShouldReturnFalse_WhenModelAlreadyDeleted()
		{
			// Arrange
			var model = new Model { Id = 1, Name = "Model X", BrandId = 1, Brand = brandsData[0], IsDeleted = true };
			mockModelRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(model);

			// Act
			var result = await _modelService.SoftDeleteByIdAsync(1);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task InitializeModelByIdAsync_ShouldReturnViewModel_WhenModelExists()
		{
			// Arrange
			var model = new Model { Id = 1, Name = "Model X", BrandId = 1, Brand = brandsData[0] };
			mockModelRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(model);

			// Act
			var result = await _modelService.InitializeModelByIdAsync(1);

			// Assert
			Assert.AreEqual("Model X", result.Name);
		}

		[Test]
		public void InitializeModelByIdAsync_ShouldThrowArgumentException_WhenModelDoesNotExist()
		{
			// Arrange
			mockModelRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Model)null);

			// Act & Assert
			var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _modelService.InitializeModelByIdAsync(999));

			Assert.That(ex.Message, Is.EqualTo("No such model"));
		}
	}
}
