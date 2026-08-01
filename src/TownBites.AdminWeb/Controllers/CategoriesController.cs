using Microsoft.AspNetCore.Mvc;

namespace TownBites.AdminWeb.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
