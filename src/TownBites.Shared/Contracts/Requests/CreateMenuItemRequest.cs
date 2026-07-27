using System.ComponentModel.DataAnnotations;

namespace TownBites.Shared.Contracts.Requests;

public class CreateMenuItemRequest
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 100000)]
    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public bool IsVeg { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int PreparationTimeInMinutes { get; set; }

    public string? ImageUrl { get; set; }
}

public class UpdateMenuItemRequest
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 100000)]
    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public bool IsVeg { get; set; }

    public bool IsAvailable { get; set; }

    public int PreparationTimeInMinutes { get; set; }

    public string? ImageUrl { get; set; }
}