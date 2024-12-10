using CarDealership.ViewModels.Models.CategoryViewModels;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;

namespace CarDealership.Tests
{
	[TestFixture]
	public class CategoryServiceTests
	{
		private Mock<IRepository<Category>> mockCategoryRepository;
		private Mock<IRepository<Car>> mockCarRepository;
		private CategoryService categoryService;

		private readonly List<Category> categoriesData = new List<Category>
		{
			new Category { Id = 1, Name = "SUV" },
			new Category { Id = 2, Name = "Sedan" }
		};

		[SetUp]
		public void Setup()
		{
			mockCategoryRepository = new Mock<IRepository<Category>>();
			mockCarRepository = new Mock<IRepository<Car>>();
			categoryService = new CategoryService(mockCategoryRepository.Object, mockCarRepository.Object);
		}

		[Test]
		public async Task AddCategoryAsync_ShouldAddCategory_WhenCategoryIsValid()
		{
			// Arrange
			var categoryViewModel = new CategoryAddViewModel { Name = "Truck" };

			mockCategoryRepository
				.Setup(repo => repo.AddAsync(It.IsAny<Category>()))
				.Returns(Task.CompletedTask);

			// Act
			await categoryService.AddCategoryAsync(categoryViewModel);

			// Assert
			mockCategoryRepository.Verify(repo => repo.AddAsync(It.IsAny<Category>()), Times.Once);
		}

		[Test]
		public void AddCategoryAsync_ShouldThrowArgumentNullException_WhenCategoryIsNull()
		{
			// Act & Assert
			var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
				await categoryService.AddCategoryAsync(null));
		}

		[Test]
		public async Task DeleteCategoryByIdAsync_ShouldDeleteCategory_WhenCategoryExists()
		{
			// Arrange
			var category = new Category { Id = 1, Name = "SUV" };
			var defaultCategory = new Category { Id = 3, Name = "No Category" };
			var car = new Car { Id = 1, CategoryId = 1, Category = category };

			mockCategoryRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(category);

			mockCategoryRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(new List<Category> { defaultCategory }.BuildMock());

			mockCarRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(new List<Car> { car }.BuildMock());

			mockCarRepository
				.Setup(repo => repo.GetAll())
				.Returns(new List<Car> { car });

			mockCategoryRepository
				.Setup(repo => repo.DeleteByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(true);

			// Act
			var result = await categoryService.DeleteCategoryByIdAsync(1);

			// Assert
			Assert.IsTrue(result);
			mockCategoryRepository.Verify(repo => repo.DeleteByIdAsync(1), Times.Once);
			mockCarRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Car>()), Times.Once);
		}

		[Test]
		public async Task DeleteCategoryByIdAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
		{
			// Arrange
			mockCategoryRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Category)null);

			// Act
			var result = await categoryService.DeleteCategoryByIdAsync(1);

			// Assert
			Assert.IsFalse(result);
			mockCategoryRepository.Verify(repo => repo.DeleteByIdAsync(It.IsAny<int>()), Times.Never);
		}

		[Test]
		public async Task DeleteCategoryByIdAsync_ShouldCreateDefaultCategory_WhenDefaultCategoryDoesNotExist()
		{
			// Arrange
			var category = new Category { Id = 1, Name = "SUV" };
			var defaultCategory = new Category { Id = 2, Name = "No Category" };
			var car = new Car { Id = 1, CategoryId = 1, Category = category };

			mockCategoryRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(category);

			mockCategoryRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(new List<Category>().BuildMock());  

			mockCarRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(new List<Car> { car }.BuildMock());

			mockCarRepository
				.Setup(repo => repo.GetAll())
				.Returns(new List<Car> { car });

			mockCategoryRepository
				.Setup(repo => repo.AddAsync(It.IsAny<Category>()))
				.Returns(Task.CompletedTask);

			mockCategoryRepository
				.Setup(repo => repo.DeleteByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(true);

			// Act
			var result = await categoryService.DeleteCategoryByIdAsync(1);

			// Assert
			Assert.IsTrue(result);

			mockCategoryRepository.Verify(repo => repo.AddAsync(It.Is<Category>(c => c.Name == "No Category")), Times.Once);
		}


		[Test]
		public async Task DoesCategoryExistAsync_ShouldReturnTrue_WhenCategoryExists()
		{
			// Arrange
			mockCategoryRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(categoriesData.BuildMock());

			// Act
			var result = await categoryService.DoesCategoryExistAsync("SUV");

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public async Task DoesCategoryExistAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
		{
			// Arrange
			mockCategoryRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(categoriesData.BuildMock());

			// Act
			var result = await categoryService.DoesCategoryExistAsync("Truck");

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task EditCategoryAsync_ShouldEditCategory_WhenCategoryExists()
		{
			// Arrange
			var categoryEditViewModel = new CategoryEditViewModel { Id = 1, Name = "Luxury SUV" };
			var category = new Category { Id = 1, Name = "SUV" };

			mockCategoryRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(category);

			mockCategoryRepository
				.Setup(repo => repo.UpdateAsync(It.IsAny<Category>()))
				.ReturnsAsync(true);

			// Act
			await categoryService.EditCategoryAsync(categoryEditViewModel);

			// Assert
			Assert.That(category.Name, Is.EqualTo("Luxury SUV"));
			mockCategoryRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Category>()), Times.Once);
		}

		[Test]
		public void EditCategoryAsync_ShouldThrowArgumentNullException_WhenCategoryDoesNotExist()
		{
			// Arrange
			var categoryEditViewModel = new CategoryEditViewModel { Id = 99, Name = "Luxury SUV" };

			mockCategoryRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync((Category)null);

			// Act & Assert
			var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
				await categoryService.EditCategoryAsync(categoryEditViewModel));
		}

		[Test]
		public async Task GetCategoryByIdAsync_ShouldReturnCategory_WhenCategoryExists()
		{
			// Arrange
			var category = new Category { Id = 1, Name = "SUV" };

			mockCategoryRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(category);

			// Act
			var result = await categoryService.GetCategoryByIdAsync(1);

			// Assert
			Assert.That(result.Name, Is.EqualTo("SUV"));
		}

		[Test]
		public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
		{
			// Arrange
			mockCategoryRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(categoriesData.BuildMock());

			// Act
			var result = await categoryService.GetAllCategoriesAsync();

			// Assert
			Assert.That(result.Count(), Is.EqualTo(2));
			Assert.That(result.First().Name, Is.EqualTo("SUV"));
		}
	}
}
