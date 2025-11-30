using Microsoft.EntityFrameworkCore;
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
        IDietGoalServices _dietGoalServices;
        public DietLogSeeder(IUserServices userServices, IDietLogServices dietLogServices, IFoodServices foodServices, IDietGoalServices dietgoalServices)
        {
            _userServices = userServices;
            _dietLogServices = dietLogServices;
            _foodServices = foodServices;
            _dietGoalServices = dietgoalServices;
        }

        public void SeedTestUser()
        {
            Random random = new Random();
            string id = "b2b52787-d2cd-4234-847d-7d71fe672bea"; // SeededTest user id
            
            // Seed Diet Logs
            List<DietLog> logs = _dietLogServices.GetDietLogsByUser(id);
            int i = !logs.Any() ? 31 : DateTime.Today.Subtract(logs[0].DateEaten).Days;
            int calorieId = 1008; // Calories nutrient id

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
                        foodCaloriesPer100g = _foodServices.GetNutrientAmountInFood(foodId, calorieId);
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

            // Seed Diet Goals
            var existingUserGoals = _dietGoalServices.GetDietGoalByUser(id)
                                         .Select(g => g.NutrientId)
                                         .ToHashSet();

            var newGoals = new List<DietGoal>();

            foreach (var nutrient in RecommendedDailyIntake)
            {
                if (!existingUserGoals.Contains(nutrient.Key))
                {
                    newGoals.Add(new DietGoal
                    {
                        UserId = id,
                        NutrientId = nutrient.Key,
                        Goal = nutrient.Value
                    });
                }
            }

            if (newGoals.Any())
            {
                foreach (var goal in newGoals)
                    _dietGoalServices.CreateDietGoal(goal);
            }
        }


        private readonly Dictionary<int, double> RecommendedDailyIntake = new()
        {
            { 1003, 56 },        // Protein (g)
            { 1004, 70 },        // Total lipid (fat) (g)
            { 1005, 275 },       // Carbohydrate, by difference (g)
            { 1008, 2000 },      // Energy (kcal)
            { 1018, 0 },         // Alcohol, ethyl (g)
            { 1051, 3700 },      // Water (g)
            { 1057, 400 },       // Caffeine (mg)
            { 1058, 200 },       // Theobromine (mg)
            { 1079, 28 },        // Fiber, total dietary (g)
            { 1087, 1300 },      // Calcium, Ca (mg)
            { 1089, 18 },        // Iron, Fe (mg)
            { 1090, 400 },       // Magnesium, Mg (mg)
            { 1091, 700 },       // Phosphorus, P (mg)
            { 1092, 4700 },      // Potassium, K (mg)
            { 1093, 2300 },      // Sodium, Na (mg)
            { 1095, 11 },        // Zinc, Zn (mg)
            { 1098, 0.9 },       // Copper, Cu (mg)
            { 1103, 55 },        // Selenium, Se (µg)
            { 1105, 900 },       // Retinol (µg)
            { 1106, 900 },       // Vitamin A, RAE (µg)
            { 1107, 0 },         // Carotene, beta (µg)
            { 1108, 0 },         // Carotene, alpha (µg)
            { 1109, 15 },        // Vitamin E (alpha-tocopherol) (mg)
            { 1114, 15 },        // Vitamin D (D2 + D3) (µg)
            { 1120, 0 },         // Cryptoxanthin, beta (µg)
            { 1122, 0 },         // Lycopene (µg)
            { 1123, 0 },         // Lutein + zeaxanthin (µg)
            { 1162, 90 },        // Vitamin C, total ascorbic acid (mg)
            { 1166, 1.3 },       // Riboflavin (mg)
            { 1167, 16 },        // Niacin (mg)
            { 1175, 1.3 },       // Vitamin B-6 (mg)
            { 1177, 400 },       // Folate, total (µg)
            { 1178, 2.4 },       // Vitamin B-12 (µg)
            { 1180, 550 },       // Choline, total (mg)
            { 1185, 120 },       // Vitamin K (phylloquinone) (µg)
            { 1186, 0 },         // Folic acid (µg)
            { 1187, 0 },         // Folate, food (µg)
            { 1190, 0 },         // Folate, DFE (µg)
            { 1242, 0 },         // Vitamin E, added (mg)
            { 1246, 0 },         // Vitamin B-12, added (µg)
            { 1253, 300 },       // Cholesterol (mg)
            { 2000, 50 }         // Total Sugars (g)
        };




    }
}
