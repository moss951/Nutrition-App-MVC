using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition_App.Entities
{
    public class DietGoal
    {
        public int Id { get; set; }
        public string NutrientName { get; set; }
        public double Goal {  get; set; }
        public string UserId { get; set; }
    }
}
