using Microsoft.AspNetCore.Mvc;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
using TownBites.Infrastructure.Application.Authentication.Services;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IUserService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                ApiResponse<object>.Fail("Invalid request."));
        }

        var user = await _userService.ValidateUserAsync(
            request.PhoneNumber,
            request.Password);

        if (user == null)
        {
            return Unauthorized(
                ApiResponse<object>.Fail("Invalid phone number or password."));
        }

        var jwt = _jwtTokenService.GenerateToken(user);

        var response = new LoginResponse
        {
            Token = jwt.Token,
            ExpiresAt = jwt.ExpiresAt,
            User = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString()
            }
        };

        return Ok(ApiResponse<LoginResponse>.Ok(
            response,
            "Login successful."));
    }
}