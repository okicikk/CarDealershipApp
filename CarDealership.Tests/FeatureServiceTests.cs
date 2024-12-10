using Moq;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Infrastructure.Repositories.Interfaces;
using CarDealershipApp.Services;
using CarDealership.ViewModels.Models.FeatureViewModels;
using MockQueryable.Moq;
using MockQueryable;

namespace CarDealership.Tests
{
	[TestFixture]
	public class FeatureServiceTests
	{
		private Mock<IRepository<Feature>> mockFeatureRepository;
		private FeatureService _featureService;

		private readonly List<Feature> featuresData = new List<Feature>
		{
			new Feature { Id = 1, Name = "Sunroof" },
			new Feature { Id = 2, Name = "Leather Seats" },
			new Feature { Id = 3, Name = "Bluetooth" }
		};

		[SetUp]
		public void Setup()
		{
			mockFeatureRepository = new Mock<IRepository<Feature>>();
			_featureService = new FeatureService(mockFeatureRepository.Object);
		}

		[Test]
		public async Task DeleteByIdAsync_ShouldReturnTrue_WhenFeatureExists()
		{
			// Arrange
			mockFeatureRepository
				.Setup(repo => repo.DeleteByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(true);

			// Act
			var result = await _featureService.DeleteByIdAsync(1);

			// Assert
			Assert.IsTrue(result);
			mockFeatureRepository.Verify(repo => repo.DeleteByIdAsync(1), Times.Once);
		}

		[Test]
		public async Task DeleteByIdAsync_ShouldReturnFalse_WhenFeatureDoesNotExist()
		{
			// Arrange
			mockFeatureRepository
				.Setup(repo => repo.DeleteByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(false);

			// Act
			var result = await _featureService.DeleteByIdAsync(999);

			// Assert
			Assert.IsFalse(result);
			mockFeatureRepository.Verify(repo => repo.DeleteByIdAsync(999), Times.Once);
		}

		[Test]
		public async Task GetAllFeaturesAsync_ShouldReturnAllFeatures()
		{
			// Arrange
			mockFeatureRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(featuresData.BuildMock());

			// Act
			var result = await _featureService.GetAllFeaturesAsync();

			// Assert
			Assert.That(result.Count(), Is.EqualTo(3));
			Assert.That(result.First().Name, Is.EqualTo("Bluetooth"));
		}

		[Test]
		public async Task EditFeatureAsync_ShouldUpdateFeature_WhenFeatureExists()
		{
			// Arrange
			var feature = new Feature { Id = 1, Name = "Sunroof" };
			var viewModel = new FeatureEditViewModel { Id = 1, Name = "Panoramic Sunroof" };

			mockFeatureRepository
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
				.ReturnsAsync(feature);

			mockFeatureRepository
				.Setup(repo => repo.Update(It.IsAny<Feature>()))
				.Verifiable();

			// Act
			await _featureService.EditFeatureAsync(viewModel);

			// Assert
			Assert.AreEqual("Panoramic Sunroof", feature.Name);
			mockFeatureRepository.Verify(repo => repo.Update(It.IsAny<Feature>()), Times.Once);
		}

		[Test]
		public async Task GetFeatureByIdAsync_ShouldReturnFeature_WhenFeatureExists()
		{
			// Arrange
			var feature = new Feature { Id = 1, Name = "Sunroof" };

			mockFeatureRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(featuresData.BuildMock());

			// Act
			var result = await _featureService.GetFeatureByIdAsync(1);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Sunroof", result.Name);
		}

		[Test]
		public async Task GetFeatureByIdAsync_ShouldReturnNull_WhenFeatureDoesNotExist()
		{
			// Arrange
			mockFeatureRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(featuresData.BuildMock());

			// Act
			var result = await _featureService.GetFeatureByIdAsync(999);

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public void AddAsync_ShouldThrowArgumentException_WhenFeatureAlreadyExists()
		{
			// Arrange
			var viewModel = new FeatureAddViewModel { Name = "Sunroof" };

			mockFeatureRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(featuresData.BuildMock());

			// Act & Assert
			var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
				await _featureService.AddAsync(viewModel));

			Assert.That(ex.Message, Is.EqualTo("Feature already exists."));
		}

		[Test]
		public async Task AddAsync_ShouldAddFeature_WhenFeatureDoesNotExist()
		{
			// Arrange
			var viewModel = new FeatureAddViewModel { Name = "Heated Seats" };

			mockFeatureRepository
				.Setup(repo => repo.GetAllQueryable())
				.Returns(featuresData.BuildMock());

			mockFeatureRepository
				.Setup(repo => repo.AddAsync(It.IsAny<Feature>()))
				.Returns(Task.CompletedTask);

			// Act
			await _featureService.AddAsync(viewModel);

			// Assert
			mockFeatureRepository.Verify(repo => repo.AddAsync(It.IsAny<Feature>()), Times.Once);
		}
	}
}
