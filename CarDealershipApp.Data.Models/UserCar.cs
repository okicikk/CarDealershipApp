using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Data.Models
{
	public class UserCar
	{
        [Required]
        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        [Required]
        public Car Car { get; set; } = null!;


        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User{ get; set; }= null!;
    }
}
