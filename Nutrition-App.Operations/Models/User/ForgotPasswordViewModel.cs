using System.ComponentModel.DataAnnotations;

namespace Nutrition_App.Operations.Models.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        public bool Succeeded { get; set; }
    }
}
