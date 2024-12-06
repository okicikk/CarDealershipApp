using System.ComponentModel.DataAnnotations;
using static CarDealershipApp.Constants.Constants;

namespace CarDealership.ViewModels.Models.CategoryViewModels
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [MinLength(CategoryNameMinLength,ErrorMessage = "Enter a name with minimum length of {1}")]
        [MaxLength(CategoryNameMaxLength, ErrorMessage = "Enter a name with maximum length of {1}")]
        [Required(ErrorMessage = "Enter the name of the category.")]
        public string Name { get; set; } = null!;
    }
}
