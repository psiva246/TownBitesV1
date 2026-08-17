using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Common;
using TownBites.AdminWeb.Models.File;

namespace TownBites.AdminWeb.Services;

public class UploadApiService : BaseApiService, IUploadApiService
{
    public UploadApiService( HttpClient httpClient, ITokenProvider tokenProvider,
        IOptions<ApiSettings> options) : base(httpClient, tokenProvider)
    {
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
    }

    public async Task<string?> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new Exception("Please select an image.");

        SetAuthorizationHeader();

        using var formData = new MultipartFormDataContent();

        using var stream = file.OpenReadStream();

        var streamContent = new StreamContent(stream);

        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        formData.Add(streamContent, "file", file.FileName);

        var response = await HttpClient.PostAsync("api/uploads/menu-item", formData);

        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(json);

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(json);

        if (apiResponse == null)
            throw new Exception("Invalid server response.");

        if (!apiResponse.Success)
            throw new Exception(apiResponse.Message);

        return apiResponse.Data;//?.FileUrl;
    }
}