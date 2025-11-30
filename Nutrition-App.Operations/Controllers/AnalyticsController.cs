using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Entities;
using Nutrition_App.Services;
using System.Security.Claims;

namespace Nutrition_App.Operations.Controllers
{
    public class AnalyticsController : Controller
    {
        IFoodServices _foodServices;
        IUserServices _userServices;
        IDietLogServices _dietLogServices;
        IDietGoalServices _dietGoalServices;

        public AnalyticsController(IFoodServices foodServices, IUserServices userServices, IDietLogServices dietLogServices, IDietGoalServices dietGoalServices)
        {
            _foodServices = foodServices;
            _userServices = userServices;
            _dietLogServices = dietLogServices;
            _dietGoalServices = dietGoalServices;
        }

        [HttpGet]
        [Authorize]
        public IActionResult NutrientTracker()
        {
            var model = new AnalysisModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.Nutrients = _foodServices.GetNutrients()
                .Where(n => n.Id < 1258 || n.Id > 1293) // ignore fatty acid category
                .ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult NutrientTracker(AnalysisModel model)
        {
            //repopulate drop down list
            model.Nutrients = _foodServices.GetNutrients()
                .Where(n => n.Id < 1258 || n.Id > 1293) // ignore fatty acid category
                .ToList();

            if (model.NutrientPicked == null)
            {
                return RedirectToAction("NutrientTracker");
            }

            model.NutrientName = _foodServices.GetNutrientById((int)model.NutrientPicked)?.Name;
            model.NutrientUnit = _foodServices.GetNutrientById((int)model.NutrientPicked)?.UnitName;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.Target = _dietGoalServices.GetDietGoalByUser(userId)?
                .Where(dg => dg.NutrientId == model.NutrientPicked)
                .Select(dg => dg.Goal)
                .FirstOrDefault() ?? 0;

            List<DietLog> logs = _dietLogServices
                                .GetDietLogsByUser(userId)
                                .Where(log => log.DateEaten >= DateTime.Today.AddDays(-30))
                                .ToList();

            model.DailyIntake = new Dictionary<string, double>();
            for (int i = -30; i <= 0; i++)
            {
                model.DailyIntake[DateTime.Today.AddDays(i).ToString("MMM dd")] = 0;
            }

            foreach (DietLog log in logs)
            {
                model.DailyIntake[log.DateEaten.ToString("MMM dd")] 
                    += log.WeightEaten * _foodServices.GetNutrientAmountInFood(log.FoodId, (int)model.NutrientPicked) / 100;

            }

            return View(model);
        }

        
    }
}
