using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Data;
using Nutrition_App.Operations.Models.Foods;

namespace Nutrition_App.Operations.Controllers
{
    public class FoodsController : Controller
    {
        private readonly FoodDbContext _context;
        public FoodsController(FoodDbContext context)
        {
            _context = context;
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
            model.QueriedFood = _context.Foods
                .Where(f => f.Description.ToLower().Contains(model.FoodName.ToLower()))
                .ToList();

            return View(model);
        }
    }
}
