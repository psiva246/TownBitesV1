using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Interfaces;

public interface ICartService
{
    Task<CartResponse> GetCartAsync(int userId);

    Task<CartResponse> AddItemAsync(int userId, AddCartItemRequest request);

    Task<CartResponse> UpdateItemAsync(int userId, int cartItemId, UpdateCartItemRequest request);

    Task<bool> RemoveItemAsync(int userId, int cartItemId);
    Task<int> CheckoutAsync(int userId);
    Task<List<int>> CheckoutMultiAsync(int userId);
}
