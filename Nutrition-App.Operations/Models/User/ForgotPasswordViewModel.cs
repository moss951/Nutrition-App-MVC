namespace Nutrition_App.Operations.Models.User
{
    public class ForgotPasswordViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public bool Succeeded { get; set; }
    }
}
