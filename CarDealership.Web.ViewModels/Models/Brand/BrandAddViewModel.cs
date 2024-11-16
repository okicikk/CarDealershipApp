using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Models.Brand
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
    }
}
