using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TownBites.AdminWeb.Models.Menu;

public class MenuItemRequest
{
    public int Id { get; set; }
    [Required]
    public int RestaurantId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsVeg { get; set; }

    [Required]
    [Range(1, 100000)]
    public decimal Price { get; set; }

    [Range(1, 100000)]
    public decimal DiscountPrice { get; set; }
    public bool IsAvailable { get; set; } = true;

    public IFormFile? ImageUrl { get; set; }
    public string? uploadedImageUrl { get; set; }
    public string? ExistingImageUrl { get; set; }
}

public class CreateMenuItemRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    [Range(1, 100000)]
    public decimal Price { get; set; }

    [Range(1, 100000)]
    public decimal DiscountPrice { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public int RestaurantId { get; set; }
    public bool IsVeg { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? ImageUrl { get; set; }
}

public class UpdateMenuItemRequest
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    [Range(1, 100000)]
    public decimal Price { get; set; }
    [Range(1, 100000)]
    public decimal DiscountPrice { get; set; }
    public int CategoryId { get; set; }
    public int RestaurantId { get; set; }
    public bool IsVeg { get; set; }
    public bool IsAvailable { get; set; }
    public string? ExistingImageUrl { get; set; }
    public string? ImageUrl { get; set; }
}