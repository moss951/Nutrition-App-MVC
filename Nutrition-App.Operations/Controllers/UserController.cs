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
        public IActionResult Login(string username, string plainPassword)
        {
            bool takenUsername = _services.SearchForUser(username);
            bool foundPassword = _services.SearchForPassword(username, plainPassword);

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
        public IActionResult Registration(string userDetail, string plainPassword)
        {
            //
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string userDetail)
        {
            return View();
        }
        [HttpPost]
        public IActionResult PasswordReset(string userDetail)
        {
            return View();
        }
        [HttpPost]
        public IActionResult PasswordReset(string userDetail, string plainPassword)
        {
            return RedirectToAction("Login");
        }
    }
}
