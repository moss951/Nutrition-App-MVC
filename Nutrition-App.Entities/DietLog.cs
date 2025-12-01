using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition_App.Entities
{
    public class DietLog
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string UserId { get; set; }
        public DateTime DateEaten { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Weight Eaten cannot be negative")]
        public double WeightEaten { get; set; }
    }
}
