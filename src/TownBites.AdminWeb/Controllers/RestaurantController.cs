using Microsoft.AspNetCore.Mvc;

namespace TownBites.AdminWeb.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
