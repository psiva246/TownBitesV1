using TownBites.AdminWeb.Models.Auth;
using TownBites.AdminWeb.ViewModels;

namespace TownBites.AdminWeb.Interfaces;

public interface IAuthApiService
{
    Task<AuthResponse> LoginAsync(LoginViewModel model);
}