using CarDealership.ViewModels.Models.Car;
using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Services;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CarDealershipApp.Web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ModelController : Controller
	{
		private readonly IModelService modelService;
		public ModelController(IModelService modelServ)
		{
			this.modelService = modelServ;
		}

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            (List<ModelIndexViewModel> models, int totalPages) = await modelService.GetAllModelsAsync(pageNumber, pageSize);

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View((models, totalPages));
        }


        [HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			return View(await modelService.InitializeModelByIdAsync(id));
		}
		[HttpPost]
		public async Task<IActionResult> Edit(ModelEditViewModel viewModel)
		{
			await modelService.EditModel(viewModel);
			return RedirectToAction(nameof(Index));
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await modelService.SoftDeleteByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			ModelAddViewModel viewModel = new ModelAddViewModel();
			viewModel.Brands = await modelService.GetAllBrandsAsync();
			return View(viewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Add(ModelAddViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}
			try
			{
				await modelService.AddModelAsync(viewModel);
			}
			catch (ArgumentException e)
			{
				ModelState.AddModelError(nameof(viewModel.Name), e.Message);
				return View(viewModel);
			}

			return RedirectToAction(nameof(Index));
		}
		//public async Task<bool> Delete(Model);

	}
}
