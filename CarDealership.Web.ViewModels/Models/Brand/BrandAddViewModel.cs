using System.ComponentModel.DataAnnotations;
using static CarDealershipApp.Constants.Constants;

namespace CarDealership.Web.ViewModels.Models.Brand
{ 
    public class BrandAddViewModel
    {
        [Required]
        [MinLength(BrandNameMinLength)]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; }
        [MinLength(ModelNameMinLength)]
        [MaxLength(ModelNameMaxLength)]
        public string? Model { get; set; }

        public string? ImageUrl { get; set; }
    }
}
