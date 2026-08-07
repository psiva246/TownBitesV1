using TownBites.Application.DTOs;
using TownBites.Application.Requests;
//using TownBites.Shared.Contracts.Requests;

namespace TownBites.Application.Interfaces;

public interface IOrderService
{
    Task<ApiResponse<IEnumerable<OrderDto>>> GetAllAsync();

    Task<ApiResponse<OrderDto>> GetByIdAsync(int id);

    Task<ApiResponse<IEnumerable<OrderDto>>> GetByRestaurantAsync(int restaurantId);

    Task<ApiResponse<OrderDto>> CreateAsync( CreateOrderRequest request);

    Task<ApiResponse<bool>> UpdateStatusAsync(UpdateOrderStatusRequest request);

    Task<ApiResponse<bool>> DeleteAsync(int id);
}