using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Entities;
using Nutrition_App.Services;

namespace Nutrition_App.Operations.Controllers
{
    public class AnalyticsController : Controller
    {
        IFoodServices _foodServices;
        IUserServices _userServices;
        IDietLogServices _dietLogServices;

        public AnalyticsController(IFoodServices foodServices, IUserServices userServices, IDietLogServices dietLogServices)
        {
            _foodServices = foodServices;
            _userServices = userServices;
            _dietLogServices = dietLogServices;
        }

        public IActionResult CalorieTracking(AnalysisModel model)
        {
            int id = 0; // change this to get user id
            model.Target = 2000; // change this to get user's target calorie intake

            List<DietLog> logs = _dietLogServices
                                .GetDietLogsByUser(id)
                                .Where(log => log.DateEaten >= DateTime.Today.AddDays(-30))
                                .ToList();

            model.DailyIntake = new Dictionary<DateTime, double>();

            foreach (DietLog log in logs) 
            {
                model.DailyIntake[log.DateEaten] += log.WeightEaten * _foodServices.GetCaloriesById(log.FoodId) / 100; // GetCaloriesById returns calories per 100g
            }

            return View(model);
        }
    }
}
