namespace Nutrition_App.Operations.Models.User
{
    public class LoginViewModel
    {
        public bool IsLoggedIn { get; set; } = false;
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
