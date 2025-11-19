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

        public IActionResult AddFoodToLog(int id)
        {
            AddFoodToLogViewModel model = new AddFoodToLogViewModel();
            Food food = _foodServices.GetFoodById(id);
            model.FoodId = id;
            model.Description = food.Description;
            model.DateEaten = DateOnly.FromDateTime(DateTime.Today);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddFoodToLog(AddFoodToLogViewModel model)
        {
            // Log entry logic goes here
            // model captures food id, amount eaten, date eaten
            // user data should be pulled from session data
            // -shaun
            return Ok();
        }


    }
}
