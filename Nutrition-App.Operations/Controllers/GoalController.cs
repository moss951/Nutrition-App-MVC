using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nutrition_App.Operations.Controllers
{
    [Authorize]
    public class GoalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
