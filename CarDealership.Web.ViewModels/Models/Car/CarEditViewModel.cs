using CarDealershipApp.Data.Models;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Mileage { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>(4) { "", "", "", "" };
    }
}
