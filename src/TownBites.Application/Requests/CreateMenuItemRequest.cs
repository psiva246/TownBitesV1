using System.ComponentModel.DataAnnotations;

namespace TownBites.Application.Requests;

public class CreateMenuItemRequest
{
    [Required]
    public int RestaurantId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}