using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/cart")]
//[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    /// Temporary implementation.
    /// Replace this with JWT UserId after Customer module is completed.
    /// </summary>
    private int GetUserId()
    {
        // Temporary
        return 1;

        // Future implementation
        /*
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null)
            throw new UnauthorizedAccessException();

        return int.Parse(claim.Value);
        */
    }

    /// <summary>
    /// Returns current user's cart.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var cart = await _cartService.GetCartAsync(GetUserId());

        return Ok(ApiResponse<CartResponse>.Ok(cart));
    }

    /// <summary>
    /// Add item into cart.
    /// </summary>
    [HttpPost("items")]
    public async Task<IActionResult> AddItem(
        [FromBody] AddCartItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));

        var cart = await _cartService.AddItemAsync(
            GetUserId(),
            request);

        return Ok(ApiResponse<CartResponse>.Ok(
            cart,
            "Item added to cart."));
    }

    /// <summary>
    /// Update cart item quantity.
    /// </summary>
    [HttpPut("items/{cartItemId:int}")]
    public async Task<IActionResult> UpdateItem(
        int cartItemId,
        [FromBody] UpdateCartItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));

        var cart = await _cartService.UpdateItemAsync(
            GetUserId(),
            cartItemId,
            request);

        return Ok(ApiResponse<CartResponse>.Ok(
            cart,
            "Cart updated."));
    }

    /// <summary>
    /// Remove an item from cart.
    /// </summary>
    [HttpDelete("items/{cartItemId:int}")]
    public async Task<IActionResult> RemoveItem(int cartItemId)
    {
        var success = await _cartService.RemoveItemAsync(
            GetUserId(),
            cartItemId);

        if (!success)
        {
            return NotFound(
                ApiResponse<object>.Fail("Cart item not found."));
        }

        return Ok(ApiResponse<object>.Ok(
            null,
            "Item removed from cart."));
    }

    /// <summary>
    /// Checkout current cart.
    /// Returns generated Order Id.
    /// </summary>
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout()
    {
        var orderId = await _cartService.CheckoutAsync(GetUserId());
        var mulOrderId = await _cartService.CheckoutMultiAsync(GetUserId());
        return Ok(ApiResponse<object>.Ok(
            new
            {
                OrderId = orderId
            },
            "Order placed successfully."));
    }
}