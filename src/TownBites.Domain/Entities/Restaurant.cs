namespace TownBites.Domain.Entities;

/// <summary>
/// Restaurant entity.
/// </summary>
public class Restaurant : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string OwnerName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public bool IsOpen { get; set; } = true;

    public bool IsActive { get; set; } = true;
}