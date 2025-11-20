using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nutrition_App.Entities;
using Nutrition_App.Operations.Models.User;
using Nutrition_App.Services;

namespace Nutrition_App.Operations.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _services;

        public UserController(IUserServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel
            {
                IsLoggedIn = false
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            /*            bool foundUsername = await _services.SearchForUser(username);
                        //bool foundPassword = _services.SearchForPassword(username, password);

                        if(foundUsername *//*&& foundPassword*//*)
                         {
                              //redirect to proper page
                              // placeholder id until user authentication is taught
                              return RedirectToAction("Index", "Home", new { id = 0 } );
                         }
                         else
                         {
                              return View();
                         }*/

            var result = _services.Login(model.Username, model.Password).Result;
            if (result) model.IsLoggedIn = true;
            return View(model);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel
            {
                IsCreated = false
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Height = model.Height,
                Weight = model.Weight,
                Sex = model.Sex,
                BMI = model.BMI
            };

            var result = await _services.CreateUser(user, model.Password);
            if (result.Succeeded) model.IsCreated = true;
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

/*        [HttpPost]
        public IActionResult Registration(string username, string password1, string password2)
        {
            // The entered username and passwords must be valid character-wise, the username must not be taken, and the passwords must match.
            if (_services.RegisterResetValidation(username, password1, password2, true))
            {
                // If successful, create user, and return user to login page.
                _services.RegisterUser(username, password1);
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }*/

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
/*        [HttpPost]
        public IActionResult ForgotPassword(string username)
        {
            bool validUsername = _services.ValidateLoginString(username, false);
            bool foundUsername = _services.SearchForUser(username);
            // If the username entered is valid character-wise, and the username exists in the database, redirect to password reset process.
            // Realistically, there would be an intermediate step of verifying an included email field, but this is not included in this implementation.
            if (validUsername && foundUsername)
            {
                //redirect to proper page
                return View("PasswordReset", username);
            }
            else
            {
                return View();
            }
        }*/
        [HttpPost]
        public IActionResult PasswordReset(string username)
        {
            return View(username);
        }
/*        [HttpPost]
        public IActionResult PasswordReset(string username, string password1, string password2)
        {
            // All 3 entries must be valid, and the two passwords must be the same.
            if (_services.RegisterResetValidation(username, password1, password2, false))
            {
                // If successful, return user to login page.
                _services.UpdatePassword(username, password1);
                return RedirectToAction("Login");
            }
            else
            {
                // If any of the passwords are not valid, or the passwords do not match, it will "reload" the page.
                return View(username);
            }
        }*/
    }
}
