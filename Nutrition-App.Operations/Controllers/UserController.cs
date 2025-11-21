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
            List<string> errorMessages = new List<string>();
            errorMessages = errorMessages.Concat(_services.ValidateUserEntryField(model.Username)).ToList();
            errorMessages = errorMessages.Concat(_services.ValidatePasswordEntryFields(model.Password, model.PasswordConfirm)).ToList();

            if (errorMessages.Count() > 0) // Will run if entry fields are empty and would probably throw an exception.
            {
                model.ErrorMessages = errorMessages;
                return View(model);
            }

            bool secondPasswordValidation = _services.ValidatePasswordRequirements(model.Password).Result; // Will run if entry fields are not erroneous, then check if passwords meet requirements.
            if (!secondPasswordValidation)
            {
                model.ErrorMessages = _services.ValidatePasswordRequirementsErrorMessages(model.Password);
                return View(model);
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
            var loginViewModel = new LoginViewModel();
            loginViewModel.Succeeded = true;
            return View("Login", loginViewModel);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            model.Succeeded = true;
            return View(model);
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
        public IActionResult PasswordResetVerification(PasswordResetViewModel model)
        {
            List<string> errorMessages = new List<string>();
            errorMessages = _services.ValidatePasswordEntryFields(model.Password, model.PasswordConfirm);
            errorMessages = errorMessages.Concat(_services.ValidatePasswordRequirementsErrorMessages(model.Password)).ToList();

            if (errorMessages.Count > 0)
            {
                var pwvm = new PasswordResetViewModel();
                pwvm.Username = model.Username;
                pwvm.ErrorMessages = errorMessages;
                return View("PasswordReset", pwvm);
            }
            
            var result = _services.ResetPassword(model.Username, model.Password);
            if (result.Result.Succeeded)
            {
                var loginViewModel = new LoginViewModel();
                loginViewModel.Succeeded = true;
                return View("Login", loginViewModel);
            }
            else
            { // theoretically should never need to run
                
                var pwvm = new PasswordResetViewModel();
                pwvm.Username = model.Username;
                return View("Test");
            }
            
            
        }
    }
}
