using Microsoft.AspNetCore.Mvc;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
using TownBites.Shared.Common;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IPasswordHasherService _passwordHasher;

    public HealthController(IPasswordHasherService passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var hash = _passwordHasher.HashPassword("Admin@123");

        var isValid = _passwordHasher.VerifyPassword(hash, "Admin@123");

        return Ok(new
        {
            hash,
            isValid
        });
    }
}