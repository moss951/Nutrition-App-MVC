using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition_App.Entities
{
    public class DietLog
    {
        public int Id { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime DateEaten { get; set; }
        public double WeightEaten { get; set; }
        public List<Nutrient> Nutrients { get; set; }
    }
}
