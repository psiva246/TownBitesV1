using System.ComponentModel.DataAnnotations;

namespace TownBites.AdminWeb.Models.Menu;

public class MenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsVeg { get; set; }
    public bool IsAvailable { get; set; }
    public string? ImageUrl { get; set; }
}


public class MenuItemModel
{
    public int Id { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = "";

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [StringLength(500)]
    public string Description { get; set; } = "";

    [Range(1, 100000)]
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; } = true;

    public string? ImageUrl { get; set; }
}