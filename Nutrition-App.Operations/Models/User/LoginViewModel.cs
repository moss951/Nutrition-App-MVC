using System.ComponentModel.DataAnnotations;

namespace Nutrition_App.Operations.Models.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool Succeeded { get; set; } = true;
    }
}
