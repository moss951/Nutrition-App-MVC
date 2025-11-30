using Nutrition_App.Entities;

namespace Nutrition_App.Operations.Models.Log
{
    public class EditLogViewModel
    {
        public int DietLogId { get; set; }
        public string Description { get; set; }
        public Food Food { get; set; }
        public int FoodId { get; set; }
        public DateTime DateEaten { get; set; }
        public double WeightEaten { get; set; }
    }
}
