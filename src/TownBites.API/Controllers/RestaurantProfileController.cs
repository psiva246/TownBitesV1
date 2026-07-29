using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Restaurant")]
[Route("api/restaurant/profile")]
public class RestaurantProfileController : ControllerBase
{
    private readonly IRestaurantProfileService _restaurantProfileService;

    public RestaurantProfileController(IRestaurantProfileService restaurantProfileService)
    {
        _restaurantProfileService = restaurantProfileService;
    }

    /// <summary>
    /// Temporary implementation.
    /// Replace with RestaurantId from JWT after authentication is completed.
    /// </summary>
    private int GetRestaurantId()
    {
        // Temporary
        var restaurantId = User.FindFirst("RestaurantId")?.Value;

        if (string.IsNullOrEmpty(restaurantId))
            throw new UnauthorizedAccessException("Restaurant not authenticated.");

        return int.Parse(restaurantId);

        // Future implementation
        /*
        var claim = User.FindFirst("RestaurantId");

        if (claim == null)
            throw new UnauthorizedAccessException();

        return int.Parse(claim.Value);
        */
    }

    /// <summary>
    /// Get restaurant profile.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var profile = await _restaurantProfileService.GetAsync(GetRestaurantId());

        if (profile == null)
        {
            return NotFound(ApiResponse<object>.Fail("Restaurant not found."));
        }

        return Ok(ApiResponse<RestaurantProfileResponse>.Ok(profile, "Restaurant profile retrieved successfully."));
    }

    /// <summary>
    /// Update restaurant profile.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateRestaurantProfileRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        var profile = await _restaurantProfileService.UpdateAsync(GetRestaurantId(), request);

        if (profile == null)
        {
            return NotFound(ApiResponse<object>.Fail("Restaurant not found."));
        }

        return Ok(ApiResponse<RestaurantProfileResponse>.Ok(profile, "Restaurant profile updated successfully."));
    }
}