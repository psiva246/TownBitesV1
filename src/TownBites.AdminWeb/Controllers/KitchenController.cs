using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TownBites.AdminWeb.Controllers;

[Authorize]
public class KitchenController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}