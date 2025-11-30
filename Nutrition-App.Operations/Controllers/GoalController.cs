using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Entities;
using Nutrition_App.Operations.Models.Goal;
using Nutrition_App.Operations.Models.Log;
using Nutrition_App.Operations.Models.Nutrients;
using Nutrition_App.Services;

namespace Nutrition_App.Operations.Controllers
{
    [Authorize]
    public class GoalController : Controller
    {
        private IDietGoalServices _goalServices;
        private IUserServices _userServices;
        private IFoodServices _foodServices;

        public GoalController(IDietGoalServices goalServices, IUserServices userServices, IFoodServices nutientServices)
        {
            _goalServices = goalServices;
            _userServices = userServices;
            _foodServices = nutientServices;
        }

        [HttpGet]
        public IActionResult Insert()
        {
            InsertGoalViewModel model = new InsertGoalViewModel();
            model.Nutrients = _foodServices.GetNutrients();

            return View(model);
        }

        [HttpPost]
        public IActionResult Insert(InsertGoalViewModel model)
        {
            DietGoal dietGoal = new DietGoal
            {
                NutrientId = model.NutrientId,
                UserId = _userServices.GetUserByUsername(User.Identity.Name).Result.Id,
                Goal = model.Goal
            };

            _goalServices.CreateDietGoal(dietGoal);

            return View(model);
        }

        [HttpGet]
        public IActionResult View()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dietGoals = _goalServices.GetDietGoalByUser(userId);
            var nutrients = _foodServices.GetNutrientsByDietGoals(dietGoals);
            var goalMap = nutrients.ToDictionary(f => f.Id, f => f); // faster than searching through foods table for every diet goal

            var rows = dietGoals.Select(g => new DietGoalRow
            {
                DietGoal = g,
                Nutrient = goalMap[g.NutrientId]
            }).ToList();

            var model = new ViewGoalViewModel()
            {
                Rows = rows
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DietGoal goal = _goalServices.GetDietGoal(id);
            EditGoalViewModel model = new EditGoalViewModel();
            model.Goal = goal.Goal;
            model.GoalId = id;

            Nutrient nutrient = _foodServices.GetNutrientById(goal.NutrientId);
            model.GoalName = nutrient.Name;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditGoalViewModel model)
        {
            model.GoalName = model.GoalName;

            DietGoal existing = _goalServices.GetDietGoal(model.GoalId);
            existing.Goal = model.Goal;
            _goalServices.UpdateDietGoal(existing);

            return View(model);
        }
    }
}
