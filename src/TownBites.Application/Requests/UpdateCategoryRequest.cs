using System.ComponentModel.DataAnnotations;

namespace TownBites.Application.Requests;

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