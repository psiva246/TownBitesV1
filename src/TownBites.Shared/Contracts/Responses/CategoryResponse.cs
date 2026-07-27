namespace TownBites.Shared.Contracts.Responses;

public class CategoryResponse
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }
}