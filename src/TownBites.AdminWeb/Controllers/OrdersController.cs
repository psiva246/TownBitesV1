using Microsoft.AspNetCore.Mvc;

namespace TownBites.AdminWeb.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
