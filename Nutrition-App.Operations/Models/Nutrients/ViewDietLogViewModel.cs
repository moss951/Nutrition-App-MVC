using Nutrition_App.Entities;
using System.Diagnostics.Contracts;

namespace Nutrition_App.Operations.Models.Nutrients
{
    public class ViewDietLogViewModel
    {
        public double WeightEaten { get; set; }
        public Food Food { get; set; }

        public List<DietLogRow> Rows {get; set; }
}
}
