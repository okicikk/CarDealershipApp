using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.ViewModels.Models.Admin
{
    public class UsersAllViewModel
    {
        public string Id { get; set; } = null!;
        public string? Email { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
