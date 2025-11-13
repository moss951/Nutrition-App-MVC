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
                FoodDescription = food.Description,
                Food = food
            };

            return View(model);
        }
    }
}
