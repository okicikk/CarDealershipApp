using CarDealershipApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static CarDealershipApp.Constants.Constants;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

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

		[Required(ErrorMessage = "Select an available model.")]
		public int ModelId { get; set; }

		//[Required]
		public IEnumerable<Category> Categories { get; set; } = new List<Category>();

		[Required(ErrorMessage ="Select an available category.")]
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
        public List<IFormFile>? Images { get; set; } = new List<IFormFile> ();
        [Required]
		[Range(CarMinYear, CarMaxYear,ErrorMessage = "Enter a year between 1930 and 2025")]
		public int ReleaseYear { get; set; }
		//[Required]
		public DateTime ListedOn { get; set; } = DateTime.Today;

		public IEnumerable<Feature> AvailableFeatures { get; set; } = new List<Feature>();
        public IList<int> SelectedFeaturesIds { get; set; } = new List<int>();
    }
}
