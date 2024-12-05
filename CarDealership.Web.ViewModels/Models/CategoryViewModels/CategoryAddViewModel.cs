using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarDealershipApp.Constants.Constants;
namespace CarDealership.ViewModels.Models.CategoryViewModels
{
    public class CategoryAddViewModel
    {
        [MinLength(CategoryNameMinLength)]
        [MaxLength(CategoryNameMaxLength)]
        [Required(ErrorMessage = "Enter a name for the new category!")]
        public string Name { get; set; } = null!;
    }
}
