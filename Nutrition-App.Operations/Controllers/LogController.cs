using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Services;
using Nutrition_App.Entities;
using Nutrition_App.Operations.Models.Log;

namespace Nutrition_App.Operations.Controllers
{
    public class LogController : Controller
    {
        private IFoodServices _foodServices;
        private IUserServices _userServices;

        public LogController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }


        // Incomplete 
        public IActionResult AddFoodToLog(int id)
        {
            AddFoodToLogViewModel model = new AddFoodToLogViewModel();
            Food food = _foodServices.GetFoodById(id);
            model.FoodId = id;
            model.FoodPortions = food.FoodPortions;
            model.Description = food.Description;
            model.PortionDescription = food.FoodPortions[0].PortionDescription;
            return View(model);
        }

        public IActionResult CalorieTracking()
        {
            return View();
        }
    }
}
