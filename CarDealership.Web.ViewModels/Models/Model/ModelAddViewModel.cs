using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using CarDealershipApp.Data.Models;
using static CarDealershipApp.Constants.Constants;

namespace CarDealership.ViewModels.Models.Model
{
    public class ModelAddViewModel
    {
        public int Id { get; set; }
        [MinLength(ModelNameMinLength)]
        [MaxLength(ModelNameMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<Brand> Brands { get; set; } = new List<Brand>();
        [Required(ErrorMessage = "You need to select one of the available brands!")]
        public int BrandId { get; set; }

    }
}
