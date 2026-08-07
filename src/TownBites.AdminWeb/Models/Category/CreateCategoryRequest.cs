using System.ComponentModel.DataAnnotations;

namespace TownBites.AdminWeb.Models.Category;

public class CreateCategoryRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int RestaurantId { get; set; }
    public int DisplayOrder { get; set; }
}

public class UpdateCategoryRequest
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}