using TownBites.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime? LastLoginOn { get; set; }

    public bool IsActive { get; set; } = true;

    public UserRole Role { get; set; }
}