using Nutrition_App.Entities;
using System.ComponentModel.DataAnnotations;
namespace Nutrition_App.Operations.Models.Foods
{
    public class FoodSearchViewModel
    {
        [Required(ErrorMessage ="Enter a valid search")]
        public string FoodName { get; set; }
        public List<Food>? QueriedFood {  get; set; }
    }
}
