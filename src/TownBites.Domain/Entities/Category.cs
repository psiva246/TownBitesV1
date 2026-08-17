namespace TownBites.Domain.Entities;

/// <summary>
/// Menu category for a restaurant.
/// </summary>
public class Category : BaseEntity
{
    public int RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    // Navigation Property
    public Restaurant Restaurant { get; set; } = null!;
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}

