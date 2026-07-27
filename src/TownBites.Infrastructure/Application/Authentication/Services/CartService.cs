using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using TownBites.Shared.Enums;

namespace TownBites.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _dbContext;
    private static CartResponse MapCart(Cart cart)
    {
        return new CartResponse
        {
            Items = cart.Items
                .Select(x => new CartItemResponse
                {
                    Id = x.Id,
                    MenuItemId = x.MenuItemId,
                    MenuItemName = x.MenuItem.Name,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity
                })
                .ToList()
        };
    }

    public CartService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<CartResponse> GetCartAsync(int userId)
    {
        var cart = await _dbContext.Carts
            .Include(x => x.Items)
                .ThenInclude(x => x.MenuItem)
            .AsSplitQuery().FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                !x.IsCheckedOut);

        if (cart == null)
        {
            return new CartResponse();
        }

        return MapCart(cart);
    }

    public async Task<CartResponse> AddItemAsync(int userId, AddCartItemRequest request)
    {
        if (request.Quantity <= 0)
            throw new Exception("Quantity must be greater than zero.");

        var menuItem = await _dbContext.MenuItems
            .FirstOrDefaultAsync(x =>
                x.Id == request.MenuItemId &&
                x.IsActive &&
                x.IsAvailable);

        if (menuItem == null)
            throw new Exception("Menu item not found.");

        var cart = await _dbContext.Carts
            .Include(x => x.Items)
                .ThenInclude(x => x.MenuItem)
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                !x.IsCheckedOut);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                IsCheckedOut = false
            };

            _dbContext.Carts.Add(cart);

            await _dbContext.SaveChangesAsync();
        }

        var existingItem = cart.Items
            .FirstOrDefault(x => x.MenuItemId == request.MenuItemId);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                MenuItemId = menuItem.Id,
                Quantity = request.Quantity,
                UnitPrice = menuItem.DiscountPrice ?? menuItem.Price
            });
        }

        await _dbContext.SaveChangesAsync();

        cart = await _dbContext.Carts
            .Include(x => x.Items)
                .ThenInclude(x => x.MenuItem)
            .FirstAsync(x => x.Id == cart.Id);

        return MapCart(cart);
    }

    public async Task<CartResponse> UpdateItemAsync(int userId, int cartItemId, UpdateCartItemRequest request)
    {
        if (request.Quantity <= 0)
            throw new Exception("Quantity must be greater than zero.");

        var cart = await _dbContext.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.MenuItem)
            .FirstOrDefaultAsync(c =>
                c.UserId == userId &&
                !c.IsCheckedOut);

        if (cart == null)
            throw new Exception("Cart not found.");

        var item = cart.Items
            .FirstOrDefault(x => x.Id == cartItemId);

        if (item == null)
            throw new Exception("Cart item not found.");

        item.Quantity = request.Quantity;

        await _dbContext.SaveChangesAsync();

        return MapCart(cart);
    }

    public async Task<bool> RemoveItemAsync(int userId, int cartItemId)
    {
        var cart = await _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c =>
                c.UserId == userId &&
                !c.IsCheckedOut);

        if (cart == null)
            return false;

        var item = cart.Items
            .FirstOrDefault(x => x.Id == cartItemId);

        if (item == null)
            return false;

        _dbContext.CartItems.Remove(item);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<int> CheckoutAsync(int userId)
    {
        await using var transaction =
            await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var cart = await _dbContext.Carts
                .Include(x => x.Items)
                    .ThenInclude(x => x.MenuItem)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    !x.IsCheckedOut);

            if (cart == null)
                throw new Exception("Cart not found.");

            if (!cart.Items.Any())
                throw new Exception("Cart is empty.");

            var order = new Order
            {
                UserId = userId,
                OrderedOn = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = 0
            };

            decimal grandTotal = 0;

            foreach (var cartItem in cart.Items)
            {
                var total = cartItem.UnitPrice * cartItem.Quantity;

                grandTotal += total;

                order.Items.Add(new OrderItem
                {
                    MenuItemId = cartItem.MenuItemId,
                    ItemName = cartItem.MenuItem.Name,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice,
                    TotalPrice = total
                });
            }

            order.TotalAmount = grandTotal;

            _dbContext.Orders.Add(order);

            cart.IsCheckedOut = true;

            await _dbContext.SaveChangesAsync();

            var newCart = new Cart
            {
                UserId = userId,
                IsCheckedOut = false
            };

            _dbContext.Carts.Add(newCart);

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return order.Id;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}