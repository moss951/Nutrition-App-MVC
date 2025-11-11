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
        Food? GetFoodById(int id);
    }
}
