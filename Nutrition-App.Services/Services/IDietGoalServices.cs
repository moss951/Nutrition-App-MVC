using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nutrition_App.Entities;

namespace Nutrition_App.Services
{
    public interface IDietGoalServices
    {
        public DietGoal GetDietGoal(int id);
        public List<DietGoal> GetDietGoalByUser(string userId);
        public DietGoal CreateDietGoal(DietGoal dietGoal);
        public void DeleteDietGoal(int id);
        public DietGoal UpdateDietGoal(DietGoal dietGoal);
    }
}
