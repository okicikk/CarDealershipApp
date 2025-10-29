using CarDealershipApp.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static CarDealershipApp.Constants.Constants;

namespace CarDealership.ViewModels.Models.Car
{
    public class CarEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public IEnumerable<Category> AvailableCategories { get; set; } = new List<Category>();

        [Required]
        public IEnumerable<Feature> AvailableFeatures { get; set; } = new List<Feature>();

        public List<int> SelectedFeaturesIds { get; set; } = new();

        [Required(ErrorMessage = "Select Category")]
        public int CategoryId { get; set; }

        [Required]
        [Range(CarMinWeight, CarMaxWeight)]
        public double Weight { get; set; }

        [Required]
        [MinLength(CarDescriptionMinLenght)]
        [MaxLength(CarDescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        [Range(CarMileageMinValue, CarMileageMaxValue)]
        public int Mileage { get; set; }

        // For new uploads
        [MaxLength(CarImagesMaxCount)]
        public List<IFormFile>? Images { get; set; } = new List<IFormFile>();

        // Existing images shown in the view
        public List<CarImage> ExistingImages { get; set; } = new List<CarImage>();

        public List<int> ImageIdsToRemove { get; set; } = new List<int>();
    }
}
