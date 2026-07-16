using Microsoft.AspNetCore.Mvc;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail(
                "Validation failed."));
        }

        if (await _userService.PhoneNumberExistsAsync(request.PhoneNumber))
        {
            return BadRequest(ApiResponse<object>.Fail(
                "Phone number already registered."));
        }

        var user = await _userService.RegisterCustomerAsync(
            request.Name,
            request.PhoneNumber,
            request.Password);

        return Ok(ApiResponse<object>.Ok(
            new
            {
                user.Id,
                user.Name,
                user.PhoneNumber
            },
            "Customer registered successfully."));
    }
}