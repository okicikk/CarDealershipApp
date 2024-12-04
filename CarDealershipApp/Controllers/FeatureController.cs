using CarDealership.ViewModels.Models.FeatureViewModels;
using CarDealershipApp.Data.Models;
using CarDealershipApp.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using NuGet.Protocol;

namespace CarDealershipApp.Web.Controllers
{
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
			Feature feature = await featureService.GetFeatureByIdAsync(id);

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

	}
}
