using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
//[Authorize]
[Route("api/customer/orders")]
public class CustomerOrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public CustomerOrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Temporary until JWT customer authentication is completed
    private int GetUserId()
    {
        return 1;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetCustomerOrdersAsync(GetUserId());

        return Ok(ApiResponse<List<OrderResponse>>.Ok(orders, "Orders retrieved successfully."));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order = await _orderService.GetCustomerOrderAsync(GetUserId(),id);

        if (order == null)
        {
            return NotFound(ApiResponse<object>.Fail("Order not found."));
        }

        return Ok(ApiResponse<OrderResponse>.Ok(order, "Order retrieved successfully."));
    }
}