using TownBites.AdminWeb.Models.Orders;

namespace TownBites.AdminWeb.Interfaces
{
    public interface IOrderApiService
    {
        Task<List<OrderListDto>> GetAllAsync();

        Task<OrderDetailsDto?> GetByIdAsync(int id);

        Task UpdateStatusAsync(UpdateOrderStatusRequest request);
    }
}
