using Microsoft.AspNetCore.Http;
using TownBites.AdminWeb.Models.File;

namespace TownBites.AdminWeb.Interfaces;

public interface IFileApiService
{
    Task<FileUploadResponse> UploadImageAsync(IFormFile file);

    Task DeleteAsync(string fileName);
}