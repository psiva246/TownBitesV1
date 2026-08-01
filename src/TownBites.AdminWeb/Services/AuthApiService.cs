using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Auth;
using TownBites.AdminWeb.Models.Common;
using TownBites.AdminWeb.ViewModels;

namespace TownBites.AdminWeb.Services;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _apiSettings;

    public AuthApiService(HttpClient httpClient, IOptions<ApiSettings> apiOptions)
    {
        _httpClient = httpClient;
        _apiSettings = apiOptions.Value;
        _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
    }

    public async Task<AuthResponse> LoginAsync(LoginViewModel model)
    {
        var request = new LoginRequest
        {
            Email = model.Email,
            Password = model.Password
        };

        var json = JsonConvert.SerializeObject(request);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/auth/login", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(error))
            {
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(error);

                    throw new Exception(errorResponse?.Message ?? "Unable to login.");
                }
                catch
                {
                    throw new Exception("Unable to login.");
                }
            }

            throw new Exception("Unable to login.");
        }

        var responseJson = await response.Content.ReadAsStringAsync();

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<AuthResponse>>(responseJson);

        if (apiResponse == null)
            throw new Exception("Invalid response received.");

        if (!apiResponse.Success)
            throw new Exception(apiResponse.Message);

        if (apiResponse.Data == null)
            throw new Exception("Login response is empty.");

        return apiResponse.Data;
    }
}