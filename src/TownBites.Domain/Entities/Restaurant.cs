namespace TownBites.Domain.Entities;

/// <summary>
/// Restaurant entity.
/// </summary>
public class Restaurant : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsOpen { get; set; } = true;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? CoverImageUrl { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
    public decimal DeliveryCharge { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public int DeliveryRadiusInKm { get; set; }
    public int EstimatedDeliveryMinutes { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}