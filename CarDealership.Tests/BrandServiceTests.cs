using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealership.Web.ViewModels.Models.Brand;
using CarDealershipApp.Services.Interfaces;
using static CarDealershipApp.Constants.Constants;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipApp.Services;
using MockQueryable;

namespace CarDealershipApp.Tests.Services
{
	[TestFixture]
	public class BrandServiceTests
	{
		private Mock<IRepository<Brand>> mockBrandRepository;
		private Mock<IRepository<Model>> mockModelRepository;
		private Mock<IRepository<Car>> mockCarRepository;
		private Mock<IRepository<UserCar>> mockUsersCarsRepository;
		private IBrandService _brandService;

		[SetUp]
		public void Setup()
		{
			mockBrandRepository = new Mock<IRepository<Brand>>();
			mockModelRepository = new Mock<IRepository<Model>>();
			mockCarRepository = new Mock<IRepository<Car>>();
			mockUsersCarsRepository = new Mock<IRepository<UserCar>>();
			_brandService = new BrandService(mockBrandRepository.Object, mockModelRepository.Object, mockCarRepository.Object, mockUsersCarsRepository.Object);
		}

		[Test]
		public async Task AddBrandAsync_ShouldThrowArgumentException_WhenBrandNameAlreadyExists()
		{
			// Arrange
			var model = new BrandAddViewModel { Name = "Existing Brand", ImageUrl = "image.jpg" };

			mockBrandRepository.Setup(repo => repo.GetAllQueryable())
				.Returns(new List<Brand>
				{
					new Brand { Name = "Existing Brand" }
				}.BuildMock());

			// Act & Assert
			var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _brandService.AddBrandAsync(model));

            Assert.That(exception.Message, Is.EqualTo("Brand with that name already exists!"));
		}

		[Test]
		public async Task AddBrandAsync_ShouldAddBrand_WhenBrandNameIsUnique()
		{
			// Arrange
			var model = new BrandAddViewModel { Name = "New Brand", ImageUrl = "image.jpg" };

			mockBrandRepository.Setup(repo => repo.GetAllQueryable())
				.Returns(new List<Brand>().BuildMock());

			mockBrandRepository.Setup(repo => repo.AddAsync(It.IsAny<Brand>())).Returns(Task.CompletedTask);

			// Act
			await _brandService.AddBrandAsync(model);

			// Assert
			mockBrandRepository.Verify(repo => repo.AddAsync(It.Is<Brand>(b => b.Name == model.Name && b.ImageUrl == model.ImageUrl)), Times.Once);
		}

		[Test]
		public async Task EditAsync_ShouldUpdateBrand_WhenBrandExists()
		{
			// Arrange
			var viewModel = new BrandIndexViewModel { Id = 1, Name = "Updated Brand", ImageUrl = "newImage.jpg" };

			var existingBrand = new Brand { Id = 1, Name = "Old Brand", ImageUrl = "oldImage.jpg" };

			mockBrandRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingBrand);
			mockBrandRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Brand>())).ReturnsAsync(true);

			// Act
			await _brandService.EditAsync(viewModel);

			// Assert
			mockBrandRepository.Verify(repo => repo.UpdateAsync(It.Is<Brand>(b => b.Name == viewModel.Name && b.ImageUrl == viewModel.ImageUrl)), Times.Once);
		}

		
		[Test]
		public async Task SoftDeleteByIdAsync_ShouldReturnFalse_WhenBrandAlreadyDeleted()
		{
			// Arrange
			var brand = new Brand { Id = 1, Name = "Deleted Brand", IsDeleted = true };

			mockBrandRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(brand);

			// Act
			var result = await _brandService.SoftDeleteByIdAsync(1);

			// Assert
			Assert.IsFalse(result);
			mockBrandRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Brand>()), Times.Never);
		}

	}
}
