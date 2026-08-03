using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Interfaces;

public interface IAuthApiService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}