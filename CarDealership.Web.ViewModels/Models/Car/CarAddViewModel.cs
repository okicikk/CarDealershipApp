using CarDealershipApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static CarDealershipApp.Constants.Constants;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarDealershipApp.ViewModels.CarViewModels
{
	public class CarAddViewModel
	{
		[Required]
		public string SellerId { get; set; } = null!;
        [Required]
		public int BrandId { get; set; }

		//[Required]
		public IList<Model> Models { get; set; } = new List<Model>();

		[Required]
		public int ModelId { get; set; }

		//[Required]
		public IEnumerable<Category> Categories { get; set; } = new List<Category>();

		[Required]
		public int CategoryId { get; set; }

		[Required]
		[Range((Double)CarPriceMinValue, (Double)CarPriceMaxValue)]
		public decimal Price { get; set; }
		[Required]
		[Range(CarMinWeight, CarMaxWeight)]
		public double Weight { get; set; }
		[Required]
		[MaxLength(CarDescriptionMaxLenght)]
		public string Description { get; set; } = "No Description.";
		[Required]
		[Range(CarMileageMinValue, CarMileageMaxValue)]
		public int Mileage { get; set; }
		public List<string>? ImageUrls { get; set; } = new List<string>(4);
		[Required]
		public int ReleaseYear { get; set; }
		//[Required]
		public DateTime ListedOn { get; set; } = DateTime.Today;

		public IEnumerable<Feature> AvailableFeatures { get; set; } = new List<Feature>();
        public IList<int> SelectedFeaturesIds { get; set; } = new List<int>();
    }
}
