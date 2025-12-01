using Nutrition_App.Services.Utility;
using System.ComponentModel.DataAnnotations;

namespace Nutrition_App.Operations.Models.User
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Password]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please reconfirm password.")]
        [Password]
        public string PasswordConfirm { get; set; }

        public bool PasswordsMatch { get; set; } = false;
    }
}
