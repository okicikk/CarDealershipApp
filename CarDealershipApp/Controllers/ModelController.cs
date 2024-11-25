using CarDealership.ViewModels.Models.Model;
using CarDealershipApp.Services;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipApp.Web.Controllers
{
    [Authorize]
    public class ModelController : Controller
    {
        private readonly IModelService modelService;
        public ModelController(IModelService modelServ)
        {
            this.modelService = modelServ;
        }

		public IActionResult Index()
		{
			return View();
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
                ModelState.AddModelError(nameof(viewModel.Name),e.Message);
                return View(viewModel);
            }
            
            return RedirectToAction(nameof(Index));
        }
        //public async Task<bool> Delete(Model);
        
    }
}
