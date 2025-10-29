using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarDealershipApp.Constants.Constants;


namespace CarDealershipApp.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Brand Brand { get; set; }
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        [Required]
        public int ModelId { get; set; }
        [ForeignKey(nameof(ModelId))]
        public Model Model { get; set; }
        [Required]
        public string SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public IdentityUser Seller { get; set; }
        [Required]
        public Category Category { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [Required]
        [Range((Double)CarPriceMinValue,(Double)CarPriceMaxValue)]
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
        public List<CarImage> Images { get; set; } = new List<CarImage>();
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public DateTime ListedOn { get; set; } = DateTime.Today;
        public bool IsDeleted { get; set; } = false;
        public IList<CarFeature> CarsFeatures { get; set; } = new List<CarFeature>();
        public IList<UserCar>? UsersCars { get; set; } = new List<UserCar>();
    }
}
