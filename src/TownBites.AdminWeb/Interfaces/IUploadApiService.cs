using Microsoft.AspNetCore.Http;

namespace TownBites.AdminWeb.Interfaces;

public interface IUploadApiService
{
    Task<string?> UploadImageAsync(IFormFile file);
}