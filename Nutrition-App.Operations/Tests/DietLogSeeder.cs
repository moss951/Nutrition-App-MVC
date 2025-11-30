using Nutrition_App.Entities;
using Nutrition_App.Services;
using System;

namespace Nutrition_App.Operations
{
    public class DietLogSeeder
    {
        IUserServices _userServices;
        IDietLogServices _dietLogServices;
        IFoodServices _foodServices;

        public DietLogSeeder(IUserServices userServices, IDietLogServices dietLogServices, IFoodServices foodServices)
        {
            _userServices = userServices;
            _dietLogServices = dietLogServices;
            _foodServices = foodServices;
        }

        public void SeedTestUser()
        {
            Random random = new Random();
            string id = "b2b52787-d2cd-4234-847d-7d71fe672bea"; // SeededTest user id
            
            List<DietLog> logs = _dietLogServices.GetDietLogsByUser(id);
            int i = !logs.Any() ? 31 : DateTime.Today.Subtract(logs[0].DateEaten).Days;

            for (int day = i; day > 0; day--)
            {
                for (int j = 0; j < 3; j++)
                {
                    int mealCalories = random.Next(500, 900);
                    double foodCaloriesPer100g = 0;
                    int foodId = 0;

                    while (foodCaloriesPer100g < 50)
                    {
                        foodId = random.Next(1, 5432);
                        foodCaloriesPer100g = _foodServices.GetCaloriesById(foodId);
                    }

                    DietLog log = new DietLog
                    {
                        UserId = id,
                        DateEaten = DateTime.Today.AddDays(-day).AddMinutes(random.Next(360, 1440)),
                        FoodId = foodId,
                        WeightEaten = Math.Round(mealCalories / (foodCaloriesPer100g / 100.0))
                    };

                    _dietLogServices.CreateDietLog(log);
                }
            }
            
        }

    }
}
