using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.ViewModels.Models.Car
{
    public class CarPreview
    {
        public int CarId { get; set; }
        public string BrandName { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public string? MainImage { get; set; }
        public int Year { get; set; }
        public int MileageInKm { get; set; }
        public decimal Price { get; set; }
    }
}
