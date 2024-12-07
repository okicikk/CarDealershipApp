using CarDealership.ViewModels.Models.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UsersAllViewModel>> GetAllUsersAsync();
        Task<bool> AssignRoleToUserAsync(string id, string role);
        Task<bool> RemoveRoleFromUserAsync(string id,string role);
        Task<bool> DeleteUserByIdAsync(string id);
    }
}
