using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Common;
using TownBites.AdminWeb.Models.File;

namespace TownBites.AdminWeb.Services;

public class FileApiService : BaseApiService, IFileApiService
{
    public FileApiService(HttpClient httpClient, ITokenProvider tokenProvider, IOptions<ApiSettings> options) : base(httpClient, tokenProvider)
    {
        httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
    }

    public async Task<FileUploadResponse> UploadImageAsync(IFormFile file)
    {
        SetAuthorizationHeader();

        using var content = new MultipartFormDataContent();

        using var stream = file.OpenReadStream();

        var fileContent = new StreamContent(stream);

        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        content.Add(fileContent, "file", file.FileName);

        var response = await HttpClient.PostAsync("api/uploads", content);

        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception(json);

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<FileUploadResponse>>(json);

        if (apiResponse == null)
            throw new Exception("Invalid response.");

        if (!apiResponse.Success)
            throw new Exception(apiResponse.Message);

        return apiResponse.Data ?? throw new Exception("No upload result returned.");
    }

    public async Task DeleteAsync(string fileName)
    {
        await DeleteRequestAsync($"api/uploads/{fileName}");
    }
}