using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nutrition_App.Entities;

namespace Nutrition_App.Services
{
    public interface IFoodServices
    {
        List<Food>? GetFoodsByString(string query);

        /// <summary>
        /// Retrieves a Food item by its unique ID, including its nutrients and portion information.
        /// </summary>
        /// <param name="id">The unique identifier of the food item to retrieve.</param>
        /// <returns>
        /// A <see cref="Food"/> object if found; otherwise <c>null</c>.
        /// The returned object includes related FoodNutrients and FoodPortions collections.
        /// </returns>
        /// <remarks>
        /// This method performs an EF Core query with eager loading using Include/ThenInclude
        /// to ensure all related data is retrieved in a single database call.
        /// </remarks>
        Food? GetFoodById(int id);
        FoodPortion? GetFoodPortionById(int id);

        double GetCaloriesById(int id);
        List<Food>? GetFoodsByDietLogs(List<DietLog> dietLog);
        List<Nutrient> GetNutrients();
        List<Nutrient> GetNutrientsByDietGoals(List<DietGoal> dietGoals);
        Nutrient GetNutrientById(int id);
    }
}
