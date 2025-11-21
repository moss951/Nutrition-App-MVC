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

        public AnalyticsController(IFoodServices foodServices, IUserServices userServices, IDietLogServices dietLogServices)
        {
            _foodServices = foodServices;
            _userServices = userServices;
            _dietLogServices = dietLogServices;
        }

        public IActionResult CalorieTracking(AnalysisModel model)
        {

            //model.Username = char.ToUpper(User.Identity.Name[0]) + User.Identity.Name.Substring(1);
            model.Username = "Bob";


            //int userId = int.Parse(_userServices.GetUserByUsername(User.Identity.Name).Result.Id);
            string userId = _userServices.GetUserByUsername("bob").Result.Id; // test

            model.Target = 2000; // change this to get user's target calorie intake

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
                 model.DailyIntake[log.DateEaten.ToString("MMM dd")] += log.WeightEaten * _foodServices.GetCaloriesById(log.FoodId) / 100; // GetCaloriesById returns calories per 100g
            }
         
            return View(model);
        }
    }
}
