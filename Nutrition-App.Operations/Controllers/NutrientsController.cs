using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutrition_App.Data;
using Nutrition_App.Operations.Models.Nutrients;

namespace Nutrition_App.Operations.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly FoodDbContext _context;
        public NutrientsController(FoodDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult View(string foodDescription)
        {
            var food = _context.Foods
                .Include(f => f.FoodNutrients)
                .ThenInclude(fn => fn.Nutrient)
                .Include(f => f.FoodPortions)
                .FirstOrDefault(f => f.Description.ToLower().Contains(foodDescription.ToLower()));
                

            var model = new NutrientViewModel
            {
                FoodDescription = foodDescription,
                Food = food
            };

            return View(model);
        }
    }
}
