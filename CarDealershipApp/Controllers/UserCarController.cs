using CarDealershipApp.Services;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace CarDealershipApp.Web.Controllers
{
	[Authorize(Roles = "User")]
	public class UserCarController : Controller
	{
		private readonly IUserCarService userCarService;
		public UserCarController(IUserCarService userCarService)
		{
			this.userCarService = userCarService;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var viewModel = await userCarService.LoadUserCarsAsync(CurrentUserId());

			return View(viewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Add(int id)
		{
			try
			{
				await userCarService.AddAsync(id, CurrentUserId());
			}
			catch (InvalidOperationException e)
			{
				return StatusCode(403);
			}

			return RedirectToAction(nameof(Index));
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int carId)
		{
			bool result = await userCarService.DeleteAsync(carId, CurrentUserId());

			if (result == false)
			{
				TempData["ErrorMessage"] = "Saved car could not be deleted!";
				return RedirectToAction(nameof(Index));
			}
			TempData["SuccessMessage"] = "Saved car deleted successfully!";

			return RedirectToAction(nameof(Index));
		}
		private string CurrentUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
