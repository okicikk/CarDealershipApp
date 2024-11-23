using System.ComponentModel.DataAnnotations;
using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Data.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;  
        public IList<Model> Models { get; set; } = new List<Model>();

    }
}
