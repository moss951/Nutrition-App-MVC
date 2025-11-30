using Nutrition_App.Entities;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Nutrition_App.Operations
{
    public class AnalysisModel
    {
        public double Target { get; set; }
        public Dictionary<string, double> DailyIntake { get; set; }
        public int? NutrientPicked { get; set; }
        public string NutrientName { get; set; }
        public string NutrientUnit { get; set; }
        public List<Nutrient>? Nutrients { get; set; }
    }
}
