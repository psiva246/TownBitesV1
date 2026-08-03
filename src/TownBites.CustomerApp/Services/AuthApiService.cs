using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Services;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _httpClient;

    public AuthApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        try
        {
            var httpResponse = await _httpClient.PostAsJsonAsync(
                "api/Auth/login",
                request);

            if (!httpResponse.IsSuccessStatusCode)
                return null;

            var apiResponse =
                await httpResponse.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();

            if (apiResponse == null)
                return null;

            if (!apiResponse.Success)
                throw new Exception(apiResponse.Message);

            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Login failed: {ex.Message}");
        }
    }
}