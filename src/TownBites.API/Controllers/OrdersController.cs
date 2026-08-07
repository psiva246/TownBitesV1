using Microsoft.AspNetCore.Mvc;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Application.Requests;
//using TownBites.Shared.Contracts.Requests;
//using TownBites.Shared.Models;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetAll()
    {
        var response = await _orderService.GetAllAsync();
        return Ok(response);
    }

    /// <summary>
    /// Get order by Id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<OrderDto>>> GetById(int id)
    {
        var response = await _orderService.GetByIdAsync(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    /// <summary>
    /// Create new order
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<OrderDto>>> Create(CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _orderService.CreateAsync(request);

        if (!response.Success)
            return BadRequest(response);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Data!.Id },
            response);
    }

    /// <summary>
    /// Update order status
    /// </summary>
    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus(
        int id,
        UpdateOrderStatusRequest request)
    {
        if (id != request.OrderId)
        {
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = "Invalid Order Id"
            });
        }

        var response = await _orderService.UpdateStatusAsync(request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Delete order
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var response = await _orderService.DeleteAsync(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}