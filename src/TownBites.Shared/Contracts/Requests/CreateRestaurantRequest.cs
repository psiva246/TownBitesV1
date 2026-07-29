using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TownBites.Shared.Contracts.Requests;

public class CreateRestaurantRequest
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string OwnerName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;
}

public class UpdateRestaurantRequest
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string OwnerName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;

    public bool IsOpen { get; set; }
}

public class UpdateRestaurantProfileRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public TimeSpan OpeningTime { get; set; }

    public TimeSpan ClosingTime { get; set; }

    public decimal DeliveryCharge { get; set; }

    public decimal MinimumOrderAmount { get; set; }

    public int DeliveryRadiusInKm { get; set; }

    public int EstimatedDeliveryMinutes { get; set; }

    public bool IsOpen { get; set; }
}

public class UploadLogoRequest
{
    public IFormFile File { get; set; } = default!;
}