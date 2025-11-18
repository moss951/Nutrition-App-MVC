using Nutrition_App.Entities;

namespace Nutrition_App.Operations.Models.Nutrients
{
    public class NutrientViewModel
    {
        public int FoodId { get; set; }
        public string FoodDescription { get; set; }
        public Food Food { get; set; }
        public int PortionId { get; set; }
        public int Quantity { get; set; }
        public FoodPortion Portion { get; set; }
    }
}
