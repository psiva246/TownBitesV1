using Microsoft.AspNetCore.Mvc;
using TownBites.Shared.Common;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = ApiResponse<object>.Ok(
            new
            {
                Version = "1.0.0",
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                ServerTime = DateTime.UtcNow
            },
            "TownBites API is running."
        );

        return Ok(response);
    }
}