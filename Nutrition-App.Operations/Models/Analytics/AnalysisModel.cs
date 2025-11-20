using Nutrition_App.Entities;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Nutrition_App.Operations
{
    public class AnalysisModel
    {
        public int Target { get; set; }
        public Dictionary<DateTime, double> DailyIntake { get; set; }
    }
}
