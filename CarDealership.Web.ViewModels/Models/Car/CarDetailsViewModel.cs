

using CarDealershipApp.Data.Models;

namespace CarDealership.ViewModels.Models.Car
{
	public class CarDetailsViewModel
	{
        public int Id { get; set; }
        public string SellerId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SellerEmail { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Weight { get; set; }
        public string Description { get; set; }
        public int Mileage { get; set; }
        public List<CarImage> Images { get; set; } = new List<CarImage>();
        public int ReleaseYear { get; set; }
        public string ListedOn { get; set; } = null!;
        public List<string> CarFeatures { get; set; } = new();
    }
}
