using System.ComponentModel.DataAnnotations;
using TownBites.Shared.Enums;

namespace TownBites.Application.Requests;

public class CreateUserRequest
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }
}