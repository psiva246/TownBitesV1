namespace TownBites.Shared.Contracts.Responses;

/// <summary>
/// Login response.
/// </summary>
public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public UserResponse User { get; set; } = new();
}