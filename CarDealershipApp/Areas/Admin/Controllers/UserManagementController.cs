using CarDealership.ViewModels.Models.Admin;
using CarDealershipApp.Services.Interfaces;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data;

namespace CarDealershipApp.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class UserManagementController : Controller
	{
		private readonly IUserService userService;

		public UserManagementController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<UsersAllViewModel> usersViewModel = await userService.GetAllUsersAsync();

			return View(usersViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string role)
		{

			if (await userService.AssignRoleToUserAsync(userId, role))
			{
				TempData["SuccessMessage"] = "Role assigned successfully!";
			}
			else
			{
				TempData["ErrorMessage"] = "Role could not be assigned!";
			}
			
			return RedirectToAction(nameof(Index));


		}
		[HttpPost]
		public async Task<IActionResult> RemoveRole(string userId, string role)
		{
            if (await userService.RemoveRoleFromUserAsync(userId, role))
            {
                TempData["SuccessMessage"] = "Role removed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Role could not be removed!";
            }

            return RedirectToAction(nameof(Index));
        }
		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
            if (await userService.DeleteUserByIdAsync(userId))
            {
                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "User could not be deleted!";
            }

            return RedirectToAction(nameof(Index));
        }
	}
}
