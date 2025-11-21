using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nutrition_App.Entities;
using Nutrition_App.Operations.Models.User;
using Nutrition_App.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var result = _services.Login(model.Username, model.Password);
            if(result.Result == true)
            {
                User user = _services.GetUserByUsername(model.Username).Result;
                return RedirectToAction("Index", "Home", new { user = user });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            bool allValid = true;
            bool firstPasswordValidation = false;
            List<string> errorMessages = new List<string>();

            if (String.IsNullOrEmpty(model.Username))
            {
                allValid = false;
                errorMessages.Add("Username field must not be empty.");
            }
            else
            {
                var userExists = _services.SearchForUser(model.Username);
                if(userExists.Result)
                {
                    allValid = false;
                    errorMessages.Add("Username is already taken.");
                }
            }

            if (String.IsNullOrEmpty(model.Password) || String.IsNullOrEmpty(model.PasswordConfirm))
            {
                allValid = false;
                errorMessages.Add("Password fields must not be empty.");
            }
            else
            {
                if (!model.Password.Equals(model.PasswordConfirm))
                {
                    allValid = false;
                    errorMessages.Add("Passwords must match.");
                }
                else
                {
                    firstPasswordValidation = true;
                }
            }

            if (!allValid) // Will run if entry fields are empty and would probably throw an exception.
            {
                model.ErrorMessages = errorMessages;
                return View(model);
            }

            if (firstPasswordValidation) // Will run if entry fields are not empty, then checks if password requirements are met.
            {
                bool secondPasswordValidation = _services.ValidatePasswordRequirements(model.Password).Result;
                if (!secondPasswordValidation)
                {
                    model.ErrorMessages = _services.ValidatePasswordRequirementsErrorMessages(model.Password);
                    return View(model);
                }
            }

            var user = new User
            {
                UserName = model.Username,
                Height = model.Height,
                Weight = model.Weight,
                Sex = model.Sex,
                BMI = _services.CalculateBMI(model.Weight, model.Height)
            };

            var result = await _services.CreateUser(user, model.Password);
            return View("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (model.Username == null)
            {
                model = new ForgotPasswordViewModel();
                model.Succeeded = false;
                return View(model);
            }
            else 
            {
                var found = _services.SearchForUser(model.Username);
                if (found.Result == false)
                {
                    model = new ForgotPasswordViewModel();
                    model.Succeeded = false;
                    return View(model);
                }
                else
                {
                    var pwvm = new PasswordResetViewModel();
                    pwvm.Username = model.Username;
                    pwvm.Succeeded = true;
                    return View("PasswordReset", pwvm);
                }
            }
        }

        [HttpPost]
        public IActionResult PasswordReset(PasswordResetViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult PasswordResetVerification(string username, string password1, string password2)
        {
            if (!String.IsNullOrEmpty(password1) && !String.IsNullOrEmpty(password2))
            {
                if (password1.Equals(password2))
                {
                    var result = _services.ResetPassword(username, password1);
                    if (result.Result.Succeeded)
                    {
                        return View("Login");
                    }
                }
            }

            var pwvm = new PasswordResetViewModel();
            pwvm.Username = username;
            return View("PasswordReset", pwvm);
        }
    }
}
