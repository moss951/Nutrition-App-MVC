using Nutrition_App.Entities;

namespace Nutrition_App.Operations.Models.Goal
{
    public class InsertGoalViewModel
    {
        public int NutrientId { get; set; }
        public double Goal {  get; set; }
        public List<Nutrient> Nutrients { get; set; } = new List<Nutrient>();
        public bool Inserted { get; set; }
    }
}
