using Nutrition_App.Entities;
using Nutrition_App.Services;
using System;

namespace Nutrition_App.Operations
{
    public static class DietLogSeeder
    {
        public static void Seed(DietLogDbContext context)
        {
            var random = new Random();
            var logs = new List<DietLog>();

            for (int i = 0; i < 90; i++)
            {
                logs.Add(new DietLog
                {
                    UserId = "some-user-id",
                    FoodId = random.Next(1, 20),
                    WeightEaten = random.Next(50, 300),
                    DateEaten = DateTime.Today.AddDays(-i)
                });
            }

            context.DietLogs.AddRange(logs);
            context.SaveChanges();
        }
    }

}
