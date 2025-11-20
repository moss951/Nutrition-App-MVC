using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Services;
using Nutrition_App.Entities;
using Nutrition_App.Operations.Models.Log;
using Microsoft.EntityFrameworkCore;

namespace Nutrition_App.Operations.Controllers
{
    public class LogController : Controller
    {
        private IFoodServices _foodServices;
        private IUserServices _userServices;
        private IDietLogServices _dietLogServices;

        public LogController(IFoodServices foodServices, IUserServices userServices, IDietLogServices dietLogServices)
        {
            _foodServices = foodServices;
            _userServices = userServices;
            _dietLogServices = dietLogServices;
        }

        public IActionResult AddFoodToLog(int id)
        {
            AddFoodToLogViewModel model = new AddFoodToLogViewModel();

            Food food = _foodServices.GetFoodById(id);
            model.FoodId = id;
            model.Food = food;
            model.Description = food.Description;
            model.DateEaten = DateTime.Today;

            return View(model);
        }
        [HttpPost]
        public IActionResult AddFoodToLog(AddFoodToLogViewModel model)
        {
            model.Food = _foodServices.GetFoodById(model.FoodId);

            DietLog dietLog = new DietLog
            {
                FoodId = model.FoodId,
                UserId = _userServices.GetUserByUsername(User.Identity.Name).Result.Id,
                DateEaten = model.DateEaten,
                WeightEaten = model.WeightEaten
            };

            _dietLogServices.CreateDietLog(dietLog);

            return View(model);
        }
    }
}
