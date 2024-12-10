using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services;
using CarDealership.ViewModels.Models.Admin;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using MockQueryable;

namespace CarDealership.Tests
{
	[TestFixture]
	public class UserServiceTests
	{
		private Mock<UserManager<IdentityUser>> mockUserManager;
		private Mock<RoleManager<IdentityRole>> mockRoleManager;
		private Mock<IRepository<Car>> mockCarRepository;
		private UserService _userService;

		private readonly List<IdentityUser> usersData = new List<IdentityUser>
		{
			new IdentityUser { Id = "1", Email = "user1@example.com" },
			new IdentityUser { Id = "2", Email = "user2@example.com" }
		};

		private readonly List<Car> carsData = new List<Car>
		{
			new Car { Id = 1, SellerId = "1", IsDeleted = false },
			new Car { Id = 2, SellerId = "1", IsDeleted = false }
		};

		private readonly List<IdentityRole> rolesData = new List<IdentityRole>
		{
			new IdentityRole { Name = "Admin" },
			new IdentityRole { Name = "User" }
		};

		[SetUp]
		public void Setup()
		{
			// Mock IUserStore and IRoleStore
			var mockUserStore = new Mock<IUserStore<IdentityUser>>();
			var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();

			// Mock UserManager and RoleManager using the mocked stores
			mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
			mockRoleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore.Object, null, null, null, null);

			mockCarRepository = new Mock<IRepository<Car>>();

			// Initialize UserService with the mocked dependencies
			_userService = new UserService(mockUserManager.Object, mockRoleManager.Object, mockCarRepository.Object);
		}

		[Test]
		public async Task AssignRoleToUserAsync_ShouldReturnTrue_WhenRoleIsAssigned()
		{
			// Arrange
			var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
			mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
			mockRoleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
			mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
			mockUserManager.Setup(m => m.IsInRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(false);

			// Act
			var result = await _userService.AssignRoleToUserAsync("1", "Admin");

			// Assert
			Assert.IsTrue(result);
			mockUserManager.Verify(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public async Task AssignRoleToUserAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
		{
			// Arrange
			var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
			mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
			mockRoleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

			// Act
			var result = await _userService.AssignRoleToUserAsync("1", "Admin");

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task AssignRoleToUserAsync_ShouldReturnFalse_WhenUserAlreadyHasRole()
		{
			// Arrange
			var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
			mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
			mockRoleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
			mockUserManager.Setup(m => m.IsInRoleAsync(It.IsAny<IdentityUser>(), "Admin")).ReturnsAsync(true);

			// Act
			var result = await _userService.AssignRoleToUserAsync("1", "Admin");

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task RemoveRoleFromUserAsync_ShouldReturnTrue_WhenRoleIsRemoved()
		{
			// Arrange
			var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
			mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
			mockRoleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
			mockUserManager.Setup(m => m.IsInRoleAsync(It.IsAny<IdentityUser>(), "Admin")).ReturnsAsync(true);
			mockUserManager.Setup(m => m.RemoveFromRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _userService.RemoveRoleFromUserAsync("1", "Admin");

			// Assert
			Assert.IsTrue(result);
			mockUserManager.Verify(m => m.RemoveFromRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public async Task RemoveRoleFromUserAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
		{
			// Arrange
			var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
			mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
			mockRoleManager.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

			// Act
			var result = await _userService.RemoveRoleFromUserAsync("1", "Admin");

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task GetAllUsersAsync_ShouldReturnAllUsers()
		{
			// Arrange
			mockUserManager.Setup(m => m.Users).Returns(usersData.BuildMock());
			mockUserManager.Setup(m => m.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "User" });

			// Act
			var result = await _userService.GetAllUsersAsync();

			// Assert
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual("user1@example.com", result.First().Email);
		}

		//[Test]
		//public async Task DeleteUserByIdAsync_ShouldReturnTrue_WhenUserDeletedSuccessfully()
		//{
		//	// Arrange
		//	var user = new IdentityUser { Id = "1", Email = "user1@example.com" };
		//	mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
		//	mockUserManager.Setup(m => m.DeleteAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);

		//	var carsData = new List<Car>
		//		{
		//			new Car { SellerId = "1", IsDeleted = false }
		//		};
		//	mockCarRepository.Setup(repo => repo.GetAllQueryable()).Returns(carsData.BuildMock());
		//	mockCarRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Car>())).ReturnsAsync(true);

		//	// Act
		//	var result = await _userService.DeleteUserByIdAsync("1");

		//	// Assert
		//	Assert.IsTrue(result);  
		//	mockUserManager.Verify(m => m.DeleteAsync(It.IsAny<IdentityUser>()), Times.Exactly(2));  
		//	mockCarRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Car>()), Times.Once);  
		//}

		[Test]
		public async Task DeleteUserByIdAsync_ShouldReturnFalse_WhenUserDoesNotExist()
		{
			// Arrange
			mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

			// Act
			var result = await _userService.DeleteUserByIdAsync("999");

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task DeleteUserByIdAsync_ShouldReturnFalse_WhenUserIsDefaultUser()
		{
			// Arrange
			var user = new IdentityUser { Id = "1", Email = "admin@example.com" };

			mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

			// Mock the car repository to return an empty list for cars (this avoids the query issue)
			mockCarRepository.Setup(repo => repo.GetAllQueryable())
							  .Returns(new List<Car>().AsQueryable().BuildMock());

			// Act
			var result = await _userService.DeleteUserByIdAsync("1");

			// Assert
			Assert.IsFalse(result);
		}
	}
}
