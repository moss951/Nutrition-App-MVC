using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nutrition_App.Entities;

namespace Nutrition_App.Services
{
    public interface IDietLogServices
    {
        public DietLog GetDietLog(int id);
        public List<DietLog> GetDietLogsByUser(string userId);
        public DietLog CreateDietLog(DietLog dietLog);
        public void DeleteDietLog(int id);
    }
}
