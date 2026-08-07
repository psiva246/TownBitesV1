using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
namespace TownBites.AdminWeb.Services
{
    public class CategoryApiService : BaseApiService, ICategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient, ITokenProvider tokenProvider,
            IOptions<ApiSettings> options) : base(httpClient, tokenProvider)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task<ApiResponse<List<CategoryResponse>>> GetAllAsync(int restaurantId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CategoryResponse>>>($"api/categories?restaurantId={restaurantId}")
                   ?? new ApiResponse<List<CategoryResponse>>
                   {
                       Success = false,
                       Message = "Unable to load categories."
                   };
            return response;
        }

        public async Task<ApiResponse<CategoryResponse>> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<CategoryResponse>>($"api/categories/{id}")
                   ?? new ApiResponse<CategoryResponse>
                   {
                       Success = false,
                       Message = "Category not found."
                   };
            return response;
        }

        public async Task<ApiResponse<int>> CreateAsync(CategoryRequest request)
        {
            TownBites.Shared.Contracts.Requests.CreateCategoryRequest createRequest = new CreateCategoryRequest()
            {
                RestaurantId = request.RestaurantId,
                Name = request.Name,
                Description = request.Description,
                DisplayOrder = request.DisplayOrder,
                IsActive = request.IsActive
            };
            var response = await _httpClient.PostAsJsonAsync("api/categories", request);

            return await response.Content.ReadFromJsonAsync<ApiResponse<int>>()
                   ?? new ApiResponse<int>
                   {
                       Success = false,
                       Message = "Failed to create category."
                   };
        }

        public async Task<ApiResponse<bool>> UpdateAsync(int id, CategoryRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", request);
            
            return await response.Content.ReadFromJsonAsync<ApiResponse<bool>>()
            ?? new ApiResponse<bool>
            {
                Success = false,
                Message = "Failed to update category."
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/categories/{id}");

            return await response.Content.ReadFromJsonAsync<ApiResponse<bool>>()
                   ?? new ApiResponse<bool>
                   {
                       Success = false,
                       Message = "Failed to delete category."
                   };
        }
    }
}