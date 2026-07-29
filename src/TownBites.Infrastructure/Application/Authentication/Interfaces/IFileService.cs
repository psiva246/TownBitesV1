using Microsoft.AspNetCore.Http;

namespace TownBites.Infrastructure.Interfaces;

public interface IFileService
{
    Task<string> UploadRestaurantLogoAsync(IFormFile file);

    Task<string> UploadRestaurantCoverAsync(IFormFile file);

    Task<string> UploadMenuItemImageAsync(IFormFile file);

    void DeleteFile(string? relativePath);
}