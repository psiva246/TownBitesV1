using TownBites.Domain.Entities;

namespace TownBites.Infrastructure.Application.Authentication.Interfaces;

public interface IUserService
{
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);

    Task<User> RegisterCustomerAsync(
        string name,
        string phoneNumber,
        string password);

    Task<bool> PhoneNumberExistsAsync(string phoneNumber);
}