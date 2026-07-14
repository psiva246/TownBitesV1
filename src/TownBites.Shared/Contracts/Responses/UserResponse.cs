namespace TownBites.Shared.Contracts.Responses;

/// <summary>
/// Logged in user information.
/// </summary>
public class UserResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}