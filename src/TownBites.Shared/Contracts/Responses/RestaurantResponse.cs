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