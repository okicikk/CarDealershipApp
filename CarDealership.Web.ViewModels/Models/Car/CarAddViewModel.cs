using CarDealershipApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Models.CarViewModels
{
	public class CarAddViewModel
	{
		[Required]
		public List<Brand> Brands { get; set; } = new();
		[Required]
		public int BrandId { get; set; }

		[Required]
		public List<Model> Models { get; set; } = new List<Model>();

		[Required]
		public int ModelId { get; set; }

		[Required]
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
		public string Description { get; set; }
		[Required]
		[Range(CarMileageMinValue, CarMileageMaxValue)]
		public int Mileage { get; set; }
		public List<string>? ImageUrls { get; set; } = new List<string>();
		[Required]
		public int ReleaseYear { get; set; }
		[Required]
		public DateTime ListedOn { get; set; } = DateTime.Today;

		public IList<CarFeature> CarsFeatures { get; set; } = new List<CarFeature>();
	}
}
