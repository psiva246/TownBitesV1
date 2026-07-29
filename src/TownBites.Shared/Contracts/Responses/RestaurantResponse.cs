namespace TownBites.Shared.Contracts.Responses;

public class RestaurantResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string OwnerName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public bool IsOpen { get; set; }

    public bool IsActive { get; set; }
}

public class RestaurantProfileResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public TimeSpan OpeningTime { get; set; }

    public TimeSpan ClosingTime { get; set; }

    public decimal DeliveryCharge { get; set; }

    public decimal MinimumOrderAmount { get; set; }

    public int DeliveryRadiusInKm { get; set; }

    public int EstimatedDeliveryMinutes { get; set; }

    public bool IsOpen { get; set; }
}