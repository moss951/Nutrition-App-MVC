using Nutrition_App.Entities;
namespace Nutrition_App.Operations.Models.Foods
{
    public class FoodSearchViewModel
    {
        public string FoodName { get; set; }
        public List<Food> QueriedFood {  get; set; }
    }
}
