using System.ComponentModel.DataAnnotations;

namespace TownBites.Shared.Contracts.Requests;

/// <summary>
/// Login request.
/// </summary>
public class LoginRequest
{
    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}