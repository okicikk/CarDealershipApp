using static CarDealershipApp.Constants.Constants;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Web.ViewModels.Models.Brand
{
    public class BrandIndexViewModel
    {
        public int Id { get; set; }
        [MinLength(BrandNameMinLength)]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
