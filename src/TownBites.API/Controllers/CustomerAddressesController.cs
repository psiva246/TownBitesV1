using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TownBites.Infrastructure.Interfaces;
using TownBites.Infrastructure.Services;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
namespace TownBites.API.Controllers;

[ApiController]
[Authorize(Roles = "Customer")]
[Route("api/customer/addresses")]
public class CustomerAddressesController : ControllerBase
{
    private readonly ICustomerAddressService _customerAddressService;

    public CustomerAddressesController(ICustomerAddressService customerAddressService)
    {
        _customerAddressService = customerAddressService;
    }

    /// <summary>
    /// Temporary implementation.
    /// Replace with JWT UserId after authentication is completed.
    /// </summary>
    private int GetUserId()
    {
        // Temporary
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Future implementation
        /*
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null)
            throw new UnauthorizedAccessException();

        return int.Parse(claim.Value);
        */
    }

    /// <summary>
    /// Get all customer addresses.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAddresses()
    {
        var addresses = await _customerAddressService.GetAllAsync(GetUserId());

        return Ok(ApiResponse<List<CustomerAddressResponse>>.Ok(addresses, "Addresses retrieved successfully."));
    }

    /// <summary>
    /// Create a new address.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerAddressRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        var address = await _customerAddressService.CreateAsync(GetUserId(), request);

        return Ok(ApiResponse<CustomerAddressResponse>.Ok(
            address,
            "Address created successfully."));
    }

    /// <summary>
    /// Update an existing address.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerAddressRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        var address = await _customerAddressService.UpdateAsync(GetUserId(), id, request);

        if (address == null)
        {
            return NotFound(ApiResponse<object>.Fail("Address not found."));
        }

        return Ok(ApiResponse<CustomerAddressResponse>.Ok( address, "Address updated successfully."));
    }

    /// <summary>
    /// Delete an address.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _customerAddressService.DeleteAsync(GetUserId(), id);

        if (!success)
        {
            return NotFound(ApiResponse<object>.Fail("Address not found."));
        }

        return Ok(ApiResponse<object>.Ok(null, "Address deleted successfully."));
    }

    /// <summary>
    /// Set an address as the default address.
    /// </summary>
    [HttpPut("{id:int}/default")]
    public async Task<IActionResult> SetDefault(int id)
    {
        var success = await _customerAddressService.SetDefaultAsync(GetUserId(), id);

        if (!success)
        {
            return NotFound(ApiResponse<object>.Fail("Address not found."));
        }

        return Ok(ApiResponse<object>.Ok(null, "Default address updated successfully."));
    }
}