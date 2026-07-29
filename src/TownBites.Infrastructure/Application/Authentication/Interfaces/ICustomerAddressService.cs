using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Application.Authentication.Interfaces
{
    public interface ICustomerAddressService
    {
        Task<List<CustomerAddressResponse>> GetAllAsync(int userId);

        Task<CustomerAddressResponse> CreateAsync(int userId, CreateCustomerAddressRequest request);

        Task<CustomerAddressResponse?> UpdateAsync(int userId, int id, UpdateCustomerAddressRequest request);

        Task<bool> DeleteAsync(int userId, int id);

        Task<bool> SetDefaultAsync(int userId, int id);
    }
}
