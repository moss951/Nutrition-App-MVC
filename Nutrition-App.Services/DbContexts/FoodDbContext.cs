using Microsoft.EntityFrameworkCore;
using Nutrition_App.Entities;

// model sql database in C# classes
namespace Nutrition_App.Data
{
    public class FoodDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodAttribute> FoodAttributes { get; set; }
        public DbSet<FoodAttributeType> FoodAttributeTypes { get; set; }
        public DbSet<FoodNutrient> FoodNutrients { get; set; }
        public DbSet<Nutrient> Nutrients { get; set; }
        public DbSet<FoodPortion> FoodPortions { get; set; }
        public DbSet<MeasureUnit> MeasureUnits { get; set; }
        public DbSet<WweiaFoodCategory> WweiaFoodCategories { get; set; }
        public DbSet<InputFood> InputFoods { get; set; }
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
        {
        }
    }
}
