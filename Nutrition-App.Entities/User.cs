using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Nutrition_App.Entities
{
    public class User : IdentityUser
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Sex { get; set; }
        public double BMI { get; set; }
    }
}
