using CarDealership.ViewModels.Models.FeatureViewModels;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using static CarDealershipApp.Constants.Constants;

namespace CarDealershipApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService featureService;
        public FeatureController(IFeatureService featureService)
        {
            this.featureService = featureService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await featureService.GetAllFeaturesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Feature? feature = await featureService.GetFeatureByIdAsync(id);

            if (feature is null)
            {
                return RedirectToAction(nameof(Index));
            }

            FeatureEditViewModel viewModel = new FeatureEditViewModel()
            {
                Name = feature.Name,
                CarsWithFeatureCount = feature.CarsFeatures
                    .Where(x => x.FeatureId == feature.Id)
                    .Count()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FeatureEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await featureService.EditFeatureAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await featureService.DeleteByIdAsync(id);
            TempData["SuccessMessage"] = FeatureDeletionSuccessMessage;
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(FeatureAddViewModel viewModel)
        {
            try
            {
                await featureService.AddAsync(viewModel);
            }
            catch (ArgumentException e)
            {
                TempData["ErrorMessage"] = e.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
