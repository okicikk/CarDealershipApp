using Microsoft.AspNetCore.Mvc;
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
        public IList<Model> Models { get; set; } = new List<Model>();

    }
}
