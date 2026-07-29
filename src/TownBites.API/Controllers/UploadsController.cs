using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.API.Controllers;

[ApiController]
//[Authorize(Roles = "Admin,Restaurant")]
[Route("api/uploads")]
public class UploadsController : ControllerBase
{
    private readonly IFileService _fileService;

    public UploadsController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// Upload restaurant logo.
    /// </summary>
    [HttpPost("logo")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadRestaurantLogo([FromForm] UploadLogoRequest request)
    {
        if (request.File == null)
        {
            return BadRequest(ApiResponse<object>.Fail("Please select a file."));
        }

        var url = await _fileService.UploadRestaurantLogoAsync(request.File);

        return Ok(ApiResponse<string>.Ok(url, "Restaurant logo uploaded successfully."));
    }

    /// <summary>
    /// Upload restaurant cover image.
    /// </summary>
    [HttpPost("cover")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadRestaurantCover([FromForm] UploadLogoRequest request)
    {
        if (request.File == null)
        {
            return BadRequest(ApiResponse<object>.Fail("Please select a file."));
        }

        var url = await _fileService.UploadRestaurantCoverAsync(request.File);

        return Ok(ApiResponse<string>.Ok(url, "Restaurant cover uploaded successfully."));
    }

    /// <summary>
    /// Upload menu item image.
    /// </summary>
    [HttpPost("menu-item")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadMenuItemImage([FromForm] UploadLogoRequest request)
    {
        if (request.File == null)
        {
            return BadRequest(ApiResponse<object>.Fail("Please select a file."));
        }

        var url = await _fileService.UploadMenuItemImageAsync(request.File);

        return Ok(ApiResponse<string>.Ok(url, "Menu item image uploaded successfully."));
    }

    /// <summary>
    /// Delete an uploaded image.
    /// </summary>
    [HttpDelete]
    public IActionResult Delete([FromQuery] string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return BadRequest(ApiResponse<object>.Fail("File path is required."));
        }

        _fileService.DeleteFile(filePath);

        return Ok(ApiResponse<object>.Ok(null, "File deleted successfully."));
    }
}