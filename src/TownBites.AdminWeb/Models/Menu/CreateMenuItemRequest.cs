using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TownBites.AdminWeb.Models.Menu;

public class CreateMenuItemRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    [Range(1, 100000)]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public bool IsVeg { get; set; }
    public bool IsAvailable { get; set; } = true;
    public IFormFile? Image { get; set; }
}

public class UpdateMenuItemRequest
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    [Range(1, 100000)]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public bool IsVeg { get; set; }
    public bool IsAvailable { get; set; }
    public string? ExistingImageUrl { get; set; }
    public IFormFile? Image { get; set; }
}