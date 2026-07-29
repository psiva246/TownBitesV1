using TownBites.Domain.Entities;
using TownBites.Shared.Enums;
public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime? LastLoginOn { get; set; }

    public UserRole Role { get; set; }
}