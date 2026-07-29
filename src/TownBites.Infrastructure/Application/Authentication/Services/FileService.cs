using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TownBites.Infrastructure.Interfaces;

namespace TownBites.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;

    public FileService(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
    }

    public Task<string> UploadRestaurantLogoAsync(IFormFile file)
    {
        return UploadAsync(file, "restaurants");
    }

    public Task<string> UploadRestaurantCoverAsync(IFormFile file)
    {
        return UploadAsync(file, "restaurants");
    }

    public Task<string> UploadMenuItemImageAsync(IFormFile file)
    {
        return UploadAsync(file, "menuitems");
    }

    public void DeleteFile(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return;

        var path = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private async Task<string> UploadAsync(IFormFile file, string folder)
    {
        ValidateFile(file);

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        var fileName = $"{Guid.NewGuid()}{extension}";

        var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", folder);

        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        var fullPath = Path.Combine(uploadFolder, fileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);

        await file.CopyToAsync(stream);

        return $"/uploads/{folder}/{fileName}";
    }

    private void ValidateFile(IFormFile file)
    {
        if (file == null)
            throw new Exception("File is required.");

        if (file.Length == 0)
            throw new Exception("File is empty.");

        var maxSize = _configuration.GetValue<long>("UploadSettings:MaxFileSize");

        if (file.Length > maxSize)
            throw new Exception("File size exceeds the allowed limit.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        var allowedExtensions = _configuration.GetSection("UploadSettings:AllowedExtensions").Get<string[]>();

        if (allowedExtensions == null || !allowedExtensions.Contains(extension))
        {
            throw new Exception($"Only {string.Join(", ", allowedExtensions ?? Array.Empty<string>())} files are allowed.");
        }
    }
}