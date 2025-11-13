using Microsoft.EntityFrameworkCore;
using Nutrition_App.Data;
using Nutrition_App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition_App.Services
{
    public class FoodRepository : IFoodServices
    {
        private readonly FoodDbContext _context;

        public FoodRepository (FoodDbContext context)
        {
            _context = context;
        }

        public List<Food>? GetFoodsByString(string query)
        {
            List<Food> foodList = _context.Foods
                                            .Where(f => f.Description.ToLower().Contains(query.ToLower()))
                                            .ToList();
            return foodList;
        }

        public Food? GetFoodById(int id)
        {
            Food food = _context.Foods
                        .Include(f => f.FoodNutrients)
                        .ThenInclude(fn => fn.Nutrient)
                        .Include(f => f.FoodPortions)
                        .FirstOrDefault(f => f.Id == id);

            return food;
        }
    }
}
