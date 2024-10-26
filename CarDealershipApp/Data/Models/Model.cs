using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Data.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ModelNameMaxLength)]
        public string Name { get; set; }
        [Required]
        public Brand Brand { get; set; }
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
    }
}
