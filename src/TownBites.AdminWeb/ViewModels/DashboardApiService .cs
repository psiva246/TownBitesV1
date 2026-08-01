using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Dashboard;
using TownBites.AdminWeb.ViewModels.Dashboard;

namespace TownBites.AdminWeb.Services;

public class DashboardApiService : BaseApiService, IDashboardApiService
{
    public DashboardApiService(HttpClient httpClient, ITokenProvider tokenProvider,
        IOptions<ApiSettings> options) : base(httpClient, tokenProvider)
    {
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
    }

    //public async Task<DashboardDto> GetDashboardAsync()
    //{
    //    return await GetAsync<DashboardDto>("api/dashboard");
    //}
    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var response = await GetAsync<DashboardViewModel>("api/dashboard"); 

        return response ?? new DashboardViewModel();
    }
}