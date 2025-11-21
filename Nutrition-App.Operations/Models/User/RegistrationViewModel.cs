using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nutrition_App.Operations.Models.User
{
    public class RegistrationViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Sex { get; set; }
        public double BMI { get; set; }

        public List<SelectListItem> BinarySexes = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Male" },
                new SelectListItem { Value = "F", Text = "Female" }
            };

        public List<string> ErrorMessages {  get; set; }
    }
}
