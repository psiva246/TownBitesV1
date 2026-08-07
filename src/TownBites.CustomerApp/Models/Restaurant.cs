namespace TownBites.CustomerApp.Models;

public class RestaurantDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = "";

    public double Rating { get; set; }

    public bool IsOpen { get; set; }
}
