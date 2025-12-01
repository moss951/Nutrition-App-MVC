using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Services;
using Nutrition_App.Entities;
using Nutrition_App.Operations.Models.Log;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Nutrition_App.Operations.Models.Nutrients;

namespace Nutrition_App.Operations.Controllers
{
    [Authorize]
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
            model.DateEaten = DateTime.Now;

            return View(model);
        }
        [HttpPost]
        public IActionResult AddFoodToLog(AddFoodToLogViewModel model)
        {
            model.Food = _foodServices.GetFoodById(model.FoodId);
            if (model.Food == null) return View(model);

            DietLog dietLog = new DietLog
            {
                FoodId = model.FoodId,
                UserId = _userServices.GetUserByUsername(User.Identity.Name).Result.Id,
                DateEaten = model.DateEaten,
                WeightEaten = model.WeightEaten
            };

            _dietLogServices.CreateDietLog(dietLog);

            return RedirectToAction("View");
        }

        [Authorize]
        [HttpGet]
        public IActionResult View()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dietLogs = _dietLogServices.GetDietLogsByUser(userId);
            var foods = _foodServices.GetFoodsByDietLogs(dietLogs);
            var foodMap = foods.ToDictionary(f => f.Id, f => f); // faster than searching through foods table for every diet log

            var rows = dietLogs.Select(l => new DietLogRow
            {
                DietLog = l,
                Food = foodMap[l.FoodId]
            }).ToList();

            var model = new ViewDietLogViewModel
            {
                Rows = rows
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditLogViewModel model = new EditLogViewModel();

            DietLog dietLog = _dietLogServices.GetDietLog(id);
            Food food = _foodServices.GetFoodById(dietLog.FoodId);
            model.FoodId = food.Id;
            model.Food = food;
            model.Description = food.Description;
            model.DateEaten = DateTime.Now;
            model.WeightEaten = dietLog.WeightEaten;
            model.DietLogId = id;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditLogViewModel model)
        {
            model.Food = _foodServices.GetFoodById(model.FoodId);

            DietLog existing = _dietLogServices.GetDietLog(model.DietLogId);
            existing.WeightEaten = model.WeightEaten;
            existing.DateEaten = model.DateEaten;
            _dietLogServices.UpdateDietLog(existing);

            return RedirectToAction("View");
        }
    }
}
