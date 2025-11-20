using Nutrition_App.Entities;
using System.ComponentModel.DataAnnotations;

namespace Nutrition_App.Operations.Models.Log
{
    public class AddFoodToLogViewModel
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public string Description { get; set; }


        [Range(0, int.MaxValue)]
        public double WeightEaten { get; set; }

        public DateTime DateEaten {  get; set; }
    }

}
