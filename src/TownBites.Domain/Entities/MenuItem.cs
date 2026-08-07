namespace TownBites.Domain.Entities;

public class MenuItem : BaseEntity
{
    public int RestaurantId { get; set;  }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public bool IsVeg { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int PreparationTimeInMinutes { get; set; }
    public string? ImageUrl { get; set; }
    public Category Category { get; set; } = null!;
}