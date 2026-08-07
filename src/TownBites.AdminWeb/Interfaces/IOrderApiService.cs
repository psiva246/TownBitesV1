using TownBites.AdminWeb.Models.Orders;
using TownBites.Shared.Contracts.Responses;
using TownBites.AdminWeb.Models.Common;

namespace TownBites.AdminWeb.Interfaces
{
    public interface IOrderApiService
    {
        Task<List<OrderListDto>> GetAllAsync();

        Task<OrderDetailsDto?> GetByIdAsync(int id);

        Task UpdateStatusAsync(UpdateOrderStatusRequest request);
        Task<ApiResponse<List<OrderResponse>>> GetPendingOrdersAsync();
    }

    public interface IOrderAdminService
    {
        Task<List<OrderSummaryDto>> GetPendingOrdersAsync();

        Task<bool> AcceptOrderAsync(int orderId);

        Task<bool> RejectOrderAsync(int orderId);
    }
}
