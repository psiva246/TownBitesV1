using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Category;

namespace TownBites.AdminWeb.Services;

public class CategoryApiService : BaseApiService, ICategoryApiService
{
    public CategoryApiService(HttpClient httpClient, ITokenProvider tokenProvider, IOptions<ApiSettings> apiOptions) : base(httpClient, tokenProvider)
    {
        httpClient.BaseAddress = new Uri(apiOptions.Value.BaseUrl);
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        return await GetAsync<List<CategoryDto>>("api/categories");
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        return await GetAsync<CategoryDto>($"api/categories/{id}");
    }

    public async Task CreateAsync(CreateCategoryRequest request)
    {
        await PostAsync<object>("api/categories", request);
    }

    public async Task UpdateAsync(UpdateCategoryRequest request)
    {
        await PutAsync($"api/categories/{request.Id}", request);
    }

    public async Task DeleteAsync(int id)
    {
        await DeleteRequestAsync($"api/categories/{id}");
    }
}