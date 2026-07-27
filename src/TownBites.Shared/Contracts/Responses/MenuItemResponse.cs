namespace TownBites.Shared.Contracts.Responses;

public class MenuItemResponse
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public bool IsVeg { get; set; }

    public bool IsAvailable { get; set; }

    public int PreparationTimeInMinutes { get; set; }

    public string? ImageUrl { get; set; }
}