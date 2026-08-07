using TownBites.AdminWeb.Models.Customer;

namespace TownBites.AdminWeb.Interfaces
{
    public interface ICustomerApiService
    {
        Task<List<CustomerDto>> GetCustomersAsync();
        Task<CustomerDetailsDto?> GetCustomerAsync(int id);
    }
}
