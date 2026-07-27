using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok("Public endpoint");
    }

    [Authorize]
    [HttpGet("private")]
    public IActionResult Private()
    {
        return Ok("Authenticated successfully");
    }
}