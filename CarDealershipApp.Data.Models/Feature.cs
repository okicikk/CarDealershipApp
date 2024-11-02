using System.ComponentModel.DataAnnotations;
using static CarDealershipApp.Constants.Constants;
namespace CarDealershipApp.Data.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(FeatureNameMaxLength)]
        public string Name { get; set; }
        public IList<CarFeature> CarsFeatures { get; set; } = new List<CarFeature>();
    }
}
