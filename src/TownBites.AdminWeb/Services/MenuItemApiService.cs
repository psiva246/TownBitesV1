using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Menu;
using TownBites.Domain.Entities;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.AdminWeb.Services;

public class MenuItemApiService : BaseApiService, IMenuItemApiService
{
    private readonly IFileApiService _fileApiService;
    private readonly HttpClient _httpClient;
    public MenuItemApiService(HttpClient httpClient, ITokenProvider tokenProvider
            , IOptions<ApiSettings> options, IFileApiService fileApiService) : base(httpClient, tokenProvider)
    {
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        _httpClient = httpClient; 
        _fileApiService = fileApiService;
    }

    public async Task<TownBites.Application.DTOs.ApiResponse<List<MenuItemDto>>> GetAllAsync()
    {
        //var responce = await GetAsync<TownBites.Application.DTOs.ApiResponse<List<MenuItemDto>>>("api/menuitems");
        var responce = await _httpClient.GetFromJsonAsync<TownBites.Application.DTOs.ApiResponse<List<MenuItemDto>>>($"api/menuitems");
        return responce;
    }

    public async Task<TownBites.Application.DTOs.ApiResponse<MenuItemDto>> GetByIdAsync(int id)
    {
        var responce = await _httpClient.GetFromJsonAsync<TownBites.Application.DTOs.ApiResponse<MenuItemDto?>>($"api/menuitems/{id}");
        return responce;
    }

    public async Task<List<CategoryLookupDto>> GetCategoriesAsync()
    {
        var responce = await GetAsync<List<CategoryLookupDto>>("api/categories");
        return responce;
    }

    //public async Task CreateAsync(CreateMenuItemRequest request)
    //{
    //    await PostAsync<object>("api/menuitems", request);
    //}
    public async Task<TownBites.Application.DTOs.ApiResponse<MenuItemDto>> CreateAsync(MenuItemRequest request)
    {
        string? imageUrl = null;
        if (request.ImageUrl != null)
        {
            var upload = await _fileApiService.UploadImageAsync(request.ImageUrl);
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

        var response = await PostAsync<TownBites.Application.DTOs.ApiResponse<MenuItemDto>>("api/menuitems", apiRequest);
        return response;
    }

    public async Task<TownBites.Application.DTOs.ApiResponse<MenuItemDto>> UpdateAsync(int id, MenuItemRequest request)
    {
        //var response = await PutAsync<Task<TownBites.Application.DTOs.ApiResponse<bool>>>($"api/menuitems/{request.Id}", request);
        UpdateMenuItemRequest updateReq = new UpdateMenuItemRequest()
        {
            Id = request.Id,
            CategoryId = request.CategoryId,
            Name = request.Name,
            Description = request.Description,
            RestaurantId = request.RestaurantId,
            IsAvailable = request.IsAvailable,
            IsVeg = request.IsVeg,
            Price = request.Price,
            DiscountPrice = request.DiscountPrice,
            ImageUrl = request.uploadedImageUrl
        };
        var response = await _httpClient.PutAsJsonAsync($"api/menuitems/{request.Id}", updateReq);

        return await response.Content.ReadFromJsonAsync<TownBites.Application.DTOs.ApiResponse<MenuItemDto>>()
        ?? new TownBites.Application.DTOs.ApiResponse<MenuItemDto>
        {
            Success = false,
            Message = "Failed to update category."
        };
    }

    public async Task<TownBites.Application.DTOs.ApiResponse<bool>> DeleteAsync(int id)
    {
        //await DeleteRequestAsync($"api/menuitems/{id}");
        var response = await _httpClient.DeleteAsync($"api/menuitems/{id}");

        return await response.Content.ReadFromJsonAsync<TownBites.Application.DTOs.ApiResponse<bool>>()
               ?? new TownBites.Application.DTOs.ApiResponse<bool>
               {
                   Success = false,
                   Message = "Failed to delete category."
               };
    }
}