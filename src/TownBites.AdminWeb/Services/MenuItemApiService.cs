using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Menu;

namespace TownBites.AdminWeb.Services;

public class MenuItemApiService : BaseApiService, IMenuItemApiService
{
    private readonly IFileApiService _fileApiService;
    public MenuItemApiService(HttpClient httpClient, ITokenProvider tokenProvider
            , IOptions<ApiSettings> options, IFileApiService fileApiService) : base(httpClient, tokenProvider)
    {
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        _fileApiService = fileApiService;
    }

    public async Task<List<MenuItemDto>> GetAllAsync()
    {
        return await GetAsync<List<MenuItemDto>>("api/menuitems");
    }

    public async Task<MenuItemDto?> GetByIdAsync(int id)
    {
        return await GetAsync<MenuItemDto>($"api/menuitems/{id}");
    }

    public async Task<List<CategoryLookupDto>> GetCategoriesAsync()
    {
        return await GetAsync<List<CategoryLookupDto>>("api/categories");
    }

    //public async Task CreateAsync(CreateMenuItemRequest request)
    //{
    //    await PostAsync<object>("api/menuitems", request);
    //}
    public async Task CreateAsync(CreateMenuItemRequest request)
    {
        string? imageUrl = null;
        if (request.Image != null)
        {
            var upload = await _fileApiService.UploadImageAsync(request.Image);
            imageUrl = upload.FileUrl;
        }

        var apiRequest = new
        {
            request.Name,
            request.Description,
            request.Price,
            request.CategoryId,
            request.IsVeg,
            request.IsAvailable,
            ImageUrl = imageUrl
        };

        await PostAsync<object>("api/menuitems", apiRequest);
    }

    public async Task UpdateAsync(UpdateMenuItemRequest request)
    {
        await PutAsync($"api/menuitems/{request.Id}", request);
    }

    public async Task DeleteAsync(int id)
    {
        await DeleteRequestAsync($"api/menuitems/{id}");
    }
}