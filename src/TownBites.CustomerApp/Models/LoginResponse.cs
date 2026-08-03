namespace TownBites.CustomerApp.Models;

public class LoginResponse
{
    //public string Token { get; set; } = string.Empty;

    //public string UserName { get; set; } = string.Empty;

    //public int UserId { get; set; }

    //public string Email { get; set; } = string.Empty;
    
    public string token { get; set; } = string.Empty;

    public string name { get; set; } = string.Empty;

    public int Id { get; set; }

    public string email { get; set; } = string.Empty;
    public string role { get; set; } = string.Empty;
}