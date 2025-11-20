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

            var model = new NutrientViewModel
            {
                Food = food,
                FoodId = id,
                FoodDescription = food.Description,
                WeightEaten = 100
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("View")]
        public IActionResult ViewPost(NutrientViewModel model)
        { 
            model.Food = _foodServices.GetFoodById(model.FoodId);

            return View(model);
        }
    }
}
