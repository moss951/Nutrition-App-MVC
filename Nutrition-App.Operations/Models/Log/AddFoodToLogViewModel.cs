using Nutrition_App.Entities;
using System.ComponentModel.DataAnnotations;

namespace Nutrition_App.Operations.Models.Log
{
    public class AddFoodToLogViewModel
    {
        public int FoodId { get; set; }

        public string Description { get; set; }


        [Range(0, int.MaxValue)]
        public decimal WeightEaten { get; set; }

        public DateOnly DateEaten {  get; set; }
    }

}
