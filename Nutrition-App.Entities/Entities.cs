// store SQL objects in C# classes
namespace Nutrition_App.Entities
{
    public class FoodAttribute
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public FoodAttributeType FoodAttributeType { get; set; }
        public string? Name { get; set; }
    }

    public class FoodAttributeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FoodNutrient
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public Nutrient Nutrient { get; set; }
        public double Amount { get; set; }
    }

    public class FoodPortion
    {
        public int Id { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public string Modifier { get; set; }
        public double GramWeight { get; set; }
        public string PortionDescription { get; set; }
        public int SequenceNumber { get; set; }
    }

    public class InputFood
    {
        public int Id { get; set; }
        public string Unit { get; set; }
        public string PortionDescription { get; set; }
        public string PortionCode { get; set; }
        public string FoodDescription { get; set; }
        public int RetentionCode { get; set; }
        public double IngredientWeight { get; set; }
        public int IngredientCode { get; set; }
        public string IngredientDescription { get; set; }
        public double Amount { get; set; }
        public int SequenceNumber { get; set; }
    }

    public class MeasureUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class Nutrient
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public string UnitName { get; set; }
    }

    public class Food
    {
        public int Id { get; set; }
        public string FoodClass { get; set; }
        public string Description { get; set; }
        public List<FoodNutrient> FoodNutrients { get; set; }
        public List<FoodAttribute> FoodAttributes { get; set; }
        public string FoodCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public WweiaFoodCategory WweiaFoodCategory { get; set; }
        public string DataType { get; set; }
        public int FdcId { get; set; }
        public List<FoodPortion> FoodPortions { get; set; }
        public string PublicationDate { get; set; }
        public List<InputFood> InputFoods { get; set; }
    }

    public class WweiaFoodCategory
    {
        public int Id { get; set; }
        public string WweiaFoodCategoryDescription { get; set; }
        public int WweiaFoodCategoryCode { get; set; }
    }

    public class FoodList
    {
        public List<Food> SurveyFoods { get; set; }
    }

    /*
     public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Sex { get; set; }
        public double BMI { get; set; }
    }

    public class DietGoals
    {
        public int Id { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbohydrate { get; set; }
        public double Fat { get; set; }
        public double Fiber { get; set; }
        public double Cholesterol { get; set; }
        public double VitaminA { get; set; }
        public double VitaminB2 { get; set; }
        public double VitaminB3 { get; set; }
        public double VitaminB6 { get; set; }
        public double VitaminB7 { get; set; }
        public double VitaminB9 { get; set; }
        public double VitaminB12 { get; set; }
        public double VitaminC { get; set; }
        public double VitaminD { get; set; }
        public double VitaminD { get; set; }
        public double VitaminK { get; set; }
        public double Sodium { get; set; }
        public double Calcium { get; set; }
        public double Iron { get; set; }
        public double Magnesium { get; set; }
        public double Potassium { get; set; }
    }
     */
}
