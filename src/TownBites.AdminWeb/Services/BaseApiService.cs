using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models.Common;

namespace TownBites.AdminWeb.Services;

public abstract class BaseApiService
{
    protected readonly HttpClient HttpClient;
    private readonly ITokenProvider _tokenProvider;

    protected BaseApiService(HttpClient httpClient, ITokenProvider tokenProvider)
    {
        HttpClient = httpClient;
        _tokenProvider = tokenProvider;
    }

    protected void SetAuthorizationHeader()
    {
        var token = _tokenProvider.GetToken();

        HttpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrWhiteSpace(token))
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    protected async Task<T> GetAsync<T>(string url)
    {
        SetAuthorizationHeader();

        var response = await HttpClient.GetAsync(url);
        return await HandleResponse<T>(response);
    }

    protected async Task<T> PostAsync<T>(string url, object request)
    {
        SetAuthorizationHeader();

        var json = JsonConvert.SerializeObject(request);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync( url, content);

        return await HandleResponse<T>(response);
    }

    protected async Task PutAsync(string url, object request)
    {
        SetAuthorizationHeader();

        var json = JsonConvert.SerializeObject(request);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PutAsync(url, content);

        await EnsureSuccess(response);
    }

    protected async Task DeleteRequestAsync(string url)
    {
        SetAuthorizationHeader();

        var response = await HttpClient.DeleteAsync(url);

        await EnsureSuccess(response);
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            await EnsureSuccess(response);
        }

        var json = await response.Content.ReadAsStringAsync();

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(json);

        if (apiResponse == null)
            throw new Exception("Invalid API response.");

        if (!apiResponse.Success)
            throw new Exception(apiResponse.Message);

        if (apiResponse.Data == null)
            throw new Exception("No data returned.");

        return apiResponse.Data;
    }

    private static async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var body = await response.Content.ReadAsStringAsync();

        string message = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => "You are not authorized.",
            HttpStatusCode.Forbidden => "Access denied.",
            HttpStatusCode.NotFound => "Resource not found.",
            HttpStatusCode.BadRequest => body,
            _ => "Unexpected server error."
        };

        throw new Exception(message);
    }

    protected async Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request)
    {
        SetAuthorizationHeader();
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync(url, content);
        return await HandleResponse<TResult>(response);
    }
}