using System.ComponentModel.DataAnnotations;

namespace TownBites.Application.Requests;

public class UpdateRestaurantRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}