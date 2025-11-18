using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutrition_App.Data;
using Nutrition_App.Operations.Models.Nutrients;
using Nutrition_App.Services;

namespace Nutrition_App.Operations.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly IFoodServices _foodServices;
        public NutrientsController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var food = _foodServices.GetFoodById(id);
            var portion = food.FoodPortions.FirstOrDefault();
            var portionId = portion.Id;

            var model = new NutrientViewModel
            {
                Food = food,
                FoodId = id,
                FoodDescription = food.Description,
                Portion = portion,
                PortionId = portionId
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("View")]
        public IActionResult ViewPost(NutrientViewModel model)
        { 
            model.Food = _foodServices.GetFoodById(model.FoodId);
            model.Portion = _foodServices.GetFoodPortionById(model.PortionId);

            return View(model);
        }
    }
}
