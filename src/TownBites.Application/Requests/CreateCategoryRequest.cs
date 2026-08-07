using System.ComponentModel.DataAnnotations;

namespace TownBites.Application.Requests;

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