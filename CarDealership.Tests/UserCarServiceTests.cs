//using CarDealershipApp.Data.Models;
//using CarDealershipApp.Infrastructure.Repositories.Interfaces;
//using CarDealershipApp.Services;
//using CarDealershipApp.Services.Interfaces;
//using CarDealership.ViewModels.Models.UserCarViewModels;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MockQueryable;
//using Microsoft.EntityFrameworkCore;

//namespace CarDealershipApp.Tests.Services
//{
//	[TestFixture]
//	public class UserCarServiceTests
//	{
//		private Mock<IRepository<UserCar>> mockUserCarRepository;
//		private Mock<IRepository<Car>> mockCarRepository;
//		private IUserCarService _userCarService;

//		[SetUp]
//		public void Setup()
//		{
//			mockUserCarRepository = new Mock<IRepository<UserCar>>();
//			mockCarRepository = new Mock<IRepository<Car>>();
//			_userCarService = new UserCarService(mockUserCarRepository.Object, mockCarRepository.Object);
//		}

//		[Test]
//		public async Task AddAsync_ShouldThrowInvalidOperationException_WhenCarIsDeleted()
//		{
//			// Arrange
//			var car = new Car { Id = 1, IsDeleted = true };
//			var userCar = new UserCar { CarId = 1, UserId = "user1" };
//			mockCarRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(car);
//			mockUserCarRepository.Setup(repo => repo.GetAllQueryable()).Returns(new List<UserCar>().BuildMock());

//			// Act & Assert
//			var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
//				await _userCarService.AddAsync(1, "user1"));

//			Assert.AreEqual("Invalid car!", exception.Message);
//		}

//		[Test]
//		public async Task AddAsync_ShouldAddUserCar_WhenCarIsNotDeleted()
//		{
//			// Arrange
//			var car = new Car { Id = 1, IsDeleted = false };
//			var userCar = new UserCar { CarId = 1, UserId = "user1" };
//			mockCarRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(car);
//			mockUserCarRepository.Setup(repo => repo.GetAllQueryable()).Returns(new List<UserCar>().AsQueryable());
//			mockUserCarRepository.Setup(repo => repo.AddAsync(It.IsAny<UserCar>())).Returns(Task.CompletedTask);

//			// Act
//			await _userCarService.AddAsync(1, "user1");

//			// Assert
//			mockUserCarRepository.Verify(repo => repo.AddAsync(It.Is<UserCar>(uc => uc.CarId == userCar.CarId && uc.UserId == userCar.UserId)), Times.Once);
//		}

//		[Test]
//		public async Task DeleteAsync_ShouldReturnFalse_WhenUserCarDoesNotExist()
//		{
//			// Arrange
//			mockUserCarRepository.Setup(repo => repo.GetAllQueryable())
//				.Returns(new List<UserCar>().BuildMock());

//			// Act
//			var result = await _userCarService.DeleteAsync(1, "user1");

//			// Assert
//			Assert.IsFalse(result);
//			mockUserCarRepository.Verify(repo => repo.DeleteAsync(It.IsAny<UserCar>()), Times.Never);
//		}

//		[Test]
//		public async Task DeleteAsync_ShouldReturnTrue_WhenUserCarExists()
//		{
//			// Arrange
//			var userCar = new UserCar { CarId = 1, UserId = "user1" };
//			mockUserCarRepository.Setup(repo => repo.GetAllQueryable())
//				.Returns(new List<UserCar> { userCar }.BuildMock());
//			mockUserCarRepository.Setup(repo => repo.DeleteAsync(It.IsAny<UserCar>())).ReturnsAsync(true);

//			// Act
//			var result = await _userCarService.DeleteAsync(1, "user1");

//			// Assert
//			Assert.IsTrue(result);
//			mockUserCarRepository.Verify(repo => repo.DeleteAsync(It.Is<UserCar>(uc => uc.CarId == 1 && uc.UserId == "user1")), Times.Once);
//		}

//		[Test]
//		public async Task DeleteByCarAsync_ShouldDeleteUserCarsAssociatedWithCar()
//		{
//			// Arrange
//			var userCars = new List<UserCar>
//			{
//				new UserCar { CarId = 1, UserId = "user1" },
//				new UserCar { CarId = 1, UserId = "user2" }
//			};

//			mockUserCarRepository.Setup(repo => repo.GetAllQueryable())
//				.Returns(userCars.BuildMock());
//			mockUserCarRepository.Setup(repo => repo.DeleteAsync(It.IsAny<UserCar>())).ReturnsAsync(true);

//			// Act
//			var result = await _userCarService.DeleteByCarAsync(1);

//			// Assert
//			Assert.IsTrue(result);
//			mockUserCarRepository.Verify(repo => repo.DeleteAsync(It.Is<UserCar>(uc => uc.CarId == 1)), Times.Exactly(userCars.Count));
//		}

//		[Test]
//		public async Task DeleteByUserAsync_ShouldDeleteUserCarsAssociatedWithUser()
//		{
//			// Arrange
//			var userCars = new List<UserCar>
//			{
//				new UserCar { CarId = 1, UserId = "user1" },
//				new UserCar { CarId = 2, UserId = "user1" }
//			};

//			mockUserCarRepository.Setup(repo => repo.GetAllQueryable())
//				.Returns(userCars.BuildMock());
//			mockUserCarRepository.Setup(repo => repo.DeleteAsync(It.IsAny<UserCar>())).ReturnsAsync(true);

//			// Act
//			var result = await _userCarService.DeleteByUserAsync("user1");

//			// Assert
//			Assert.IsTrue(result);
//			mockUserCarRepository.Verify(repo => repo.DeleteAsync(It.Is<UserCar>(uc => uc.UserId == "user1")), Times.Exactly(userCars.Count));
//		}
//	}
//}
