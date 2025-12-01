using Microsoft.AspNetCore.Mvc.Rendering;
using Nutrition_App.Services.Utility;
using System.ComponentModel.DataAnnotations;

namespace Nutrition_App.Operations.Models.User
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Password]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please reconfirm password.")]
        [Password]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a minimum of 0.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Height must be a minimum of 0.1 meters tall.")]
        public double Height { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be a minimum of 0.1 meters tall.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public string Sex { get; set; }

        public List<SelectListItem> BinarySexes = new List<SelectListItem>
        {
            new SelectListItem { Value = "M", Text = "Male" },
            new SelectListItem { Value = "F", Text = "Female" }
        };

        public bool UsernameFound { get; set; } = false;
        public bool PasswordsMatch { get; set; } = false;
    }
}
