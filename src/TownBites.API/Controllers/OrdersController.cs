using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Restaurant")]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Get all pending orders.
    /// </summary>
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingOrders()
    {
        var orders = await _orderService.GetPendingOrdersAsync();

        return Ok(ApiResponse<List<OrderResponse>>.Ok(orders, "Pending orders retrieved successfully."));
    }

    /// <summary>
    /// Get order by Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order = await _orderService.GetOrderAsync(id);

        if (order == null)
        {
            return NotFound(ApiResponse<object>.Fail("Order not found."));
        }

        return Ok(ApiResponse<OrderResponse>.Ok(order, "Order retrieved successfully."));
    }

    /// <summary>
    /// Update order status.
    /// </summary>
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        var success = await _orderService.UpdateStatusAsync(id, request.Status);

        if (!success)
        {
            return NotFound(ApiResponse<object>.Fail("Order not found."));
        }

        return Ok(ApiResponse<object>.Ok(null, "Order status updated successfully."));
    }
}