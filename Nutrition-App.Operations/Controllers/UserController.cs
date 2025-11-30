using Microsoft.AspNetCore.Authorization;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _services.Login(model.Username, model.Password);
            if (result.Result == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model); // If not failed to login
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _services.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(new RegistrationViewModel() { PasswordsMatch = true });
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.Password.Equals(model.PasswordConfirm))
            {
                model.PasswordsMatch = false;
                return View(model);
            }

            var user = new User
            {
                UserName = model.Username,
                Height = model.Height,
                Weight = model.Weight,
                Sex = model.Sex
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Username))
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
            if (!ModelState.IsValid)
            {
                return View("PasswordReset", model);
            }
            
            var result = _services.ResetPassword(model.Username, model.Password);
            if (result.Result.Succeeded)
            {
                var loginViewModel = new LoginViewModel();
                loginViewModel.Succeeded = true;
                return View("Login", loginViewModel);
            }
            else
            { 
                // theoretically should never need to run
                var pwvm = new PasswordResetViewModel();
                pwvm.Username = model.Username;
                return View("Index");
            }
        }
    }
}
