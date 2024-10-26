using System.ComponentModel.DataAnnotations;
using static CarDealershipApp.Constants.Constants;
namespace CarDealershipApp.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
