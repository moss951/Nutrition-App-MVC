using Microsoft.AspNetCore.Mvc;
using Nutrition_App.Services;
using Nutrition_App.Entities;

namespace Nutrition_App.Operations.Controllers
{
    public class LogController : Controller
    {
        private IFoodServices _foodServices;
        private IUserServices _userServices;

        public LogController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }


        // Incomplete 
        public IActionResult AddFoodToLog(int id)
        {
            Food food = _foodServices.GetFoodById(id);
            return Ok(); 
        }
    }
}
