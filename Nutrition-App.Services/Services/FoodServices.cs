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
    public class FoodServices : IFoodServices
    {
        private readonly FoodDbContext _context;

        public FoodServices (FoodDbContext context)
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
            Food? food = _context.Foods
                        .Include(fn => fn.FoodNutrients)
                        .ThenInclude(n => n.Nutrient)
                        .Include(fp => fp.FoodPortions)
                        .FirstOrDefault(f => f.Id == id);

            return food;
        }

        public FoodPortion? GetFoodPortionById(int id)
        {
            FoodPortion? foodPortion = _context.FoodPortions.FirstOrDefault(f => f.Id == id);

            return foodPortion;
        }

        public double GetCaloriesById(int id)
        {
            Food? food = _context.Foods
                         .Include(fn => fn.FoodNutrients)
                         .ThenInclude(n => n.Nutrient)
                         .FirstOrDefault(f => f.Id == id);

            int calorieId = 1008; // Id for "Energy" in Nutrient Table

            var nutrientEntry = food.FoodNutrients.FirstOrDefault(fn => fn.Nutrient.Id == calorieId);

            return nutrientEntry.Amount;
        }

        public List<Food>? GetFoodsByDietLogs(List<DietLog> dietLogs)
        {
            if (dietLogs == null || dietLogs.Count == 0) return new List<Food>();

            // remove duplicate ids
            var foodIds = dietLogs
                .Select(d => d.FoodId)
                .Distinct()
                .ToList();

            var foods = _context.Foods
                .Where(f => foodIds.Contains(f.Id))
                .ToList();

            return foods;
        }

        public List<Nutrient> GetNutrients()
        {
            return _context.Nutrients.ToList();
        }
    }
}
