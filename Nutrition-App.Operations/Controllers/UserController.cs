using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Services;

namespace Nutrition_App.Operations.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices _services;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string plaintextPassword)
        {
            bool takenUsername = _services.SearchForUser(username);
            bool foundPassword = _services.SearchForPassword(username, plaintextPassword);

            if(!takenUsername && foundPassword)
             {
                  //redirect to proper page
                  // placeholder id until user authentication is taught
                  return RedirectToAction("Index", "Home", new { id = 0 } );
             }
             else
             {
                  return View();
             }
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(string username, string plaintextPassword)
        {
            bool validUsername = _services.ValidateLoginString(username, false);
            bool takenUsername = _services.SearchForUser(username);
            bool validPassword = _services.ValidateLoginString(plaintextPassword, true);

            // The entered username and password must be valid character-wise, and the username must not be taken.
            if (validUsername && !takenUsername && validPassword)
            {
                // If successful, create user, and return user to login page.
                _services.RegisterUser(username, plaintextPassword);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string username)
        {
            bool validUsername = _services.ValidateLoginString(username, false);
            bool foundUsername = _services.SearchForUser(username);
            if (validUsername && foundUsername)
            {
                //redirect to proper page
                return RedirectToAction("PasswordReset", "User", new { username });
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult PasswordReset(string username)
        {
            return View();
        }
        [HttpPost]
        public IActionResult PasswordReset(string username, string plaintextPassword, string plaintextPasswordConfirmed)
        {
            bool validUsername = _services.ValidateLoginString(username, false); // Should be an immutable control in the view, and should not throw any errors here.
            bool validPassword1 = _services.ValidateLoginString(plaintextPassword, true);
            bool validPassword2 = _services.ValidateLoginString(plaintextPasswordConfirmed, true);
            // All 3 entries must be valid, and the two passwords must be the same.
            if (validUsername && validPassword1 && validPassword2 && (validPassword1.Equals(validPassword2)) )
            {
                // If successful, return user to login page.
                _services.UpdatePassword(username, plaintextPassword);
                return RedirectToAction("Login");
            }
            else
            {
                // If any of the passwords are not valid, or the passwords do not match, it will "reload" the page.
                return View(username);
            }
        }
    }
}
