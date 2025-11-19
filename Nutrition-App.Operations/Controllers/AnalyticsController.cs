using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Entities;
using Nutrition_App.Services;

namespace Nutrition_App.Operations.Controllers
{
    public class AnalyticsController : Controller
    {
        IFoodServices _foodServices;
        IUserServices _userServices;

        public AnalyticsController(IFoodServices foodServices, IUserServices userServices)
        {
            _foodServices = foodServices;
            _userServices = userServices;
        }

        public IActionResult CalorieTracking()
        {
            return View();
        }
    }
}
