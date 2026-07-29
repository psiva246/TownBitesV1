using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Restaurant")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    private static RestaurantResponse ToResponse(Restaurant restaurant)
    {
        return new RestaurantResponse
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            OwnerName = restaurant.OwnerName,
            PhoneNumber = restaurant.PhoneNumber,
            Address = restaurant.Address,
            IsOpen = restaurant.IsOpen,
            IsActive = restaurant.IsActive
        };
    }

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _restaurantService.GetAllAsync();

        var result = restaurants.Select(r => new RestaurantResponse
        {
            Id = r.Id,
            Name = r.Name,
            OwnerName = r.OwnerName,
            PhoneNumber = r.PhoneNumber,
            Address = r.Address,
            IsOpen = r.IsOpen,
            IsActive = r.IsActive
        });

        return Ok(ApiResponse<IEnumerable<RestaurantResponse>>.Ok(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await _restaurantService.GetByIdAsync(id);

        if (restaurant == null)
            return NotFound(ApiResponse<object>.Fail("Restaurant not found."));

        //return Ok(ApiResponse<RestaurantResponse>.Ok(new RestaurantResponse
        //{
        //    Id = restaurant.Id,
        //    Name = restaurant.Name,
        //    OwnerName = restaurant.OwnerName,
        //    PhoneNumber = restaurant.PhoneNumber,
        //    Address = restaurant.Address,
        //    IsOpen = restaurant.IsOpen,
        //    IsActive = restaurant.IsActive
        //}));
        return Ok(ApiResponse<RestaurantResponse>.Ok(ToResponse(restaurant)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRestaurantRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));

        var restaurant = await _restaurantService.CreateAsync(request);

        //return Ok(ApiResponse<RestaurantResponse>.Ok(new RestaurantResponse
        //{
        //    Id = restaurant.Id,
        //    Name = restaurant.Name,
        //    OwnerName = restaurant.OwnerName,
        //    PhoneNumber = restaurant.PhoneNumber,
        //    Address = restaurant.Address,
        //    IsOpen = restaurant.IsOpen,
        //    IsActive = restaurant.IsActive
        //}, "Restaurant created successfully."));

        return Ok(ApiResponse<RestaurantResponse>.Ok(ToResponse(restaurant)));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateRestaurantRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));

        var restaurant = await _restaurantService.UpdateAsync(id, request);

        if (restaurant == null)
            return NotFound(ApiResponse<object>.Fail("Restaurant not found."));

        return Ok(ApiResponse<RestaurantResponse>.Ok(
            ToResponse(restaurant),
            "Restaurant updated successfully."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _restaurantService.DeactivateAsync(id);

        if (!success)
            return NotFound(ApiResponse<object>.Fail("Restaurant not found."));

        return Ok(ApiResponse<object>.Ok(
            null,
            "Restaurant deactivated successfully."));
    }
}