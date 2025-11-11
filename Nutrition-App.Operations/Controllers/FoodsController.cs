using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Data;
using Nutrition_App.Operations.Models;
using Nutrition_App.Operations.Models.Foods;
using Nutrition_App.Services;

namespace Nutrition_App.Operations.Controllers
{
    public class FoodsController : Controller
    {
        private IFoodServices _foodServices;

        public FoodsController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View(new FoodSearchViewModel());
        }

        [HttpPost]
        public IActionResult Search(FoodSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.QueriedFood = _foodServices.GetFoodsByString(model.FoodName);
            }
            return View(model);
        }
    }
}
