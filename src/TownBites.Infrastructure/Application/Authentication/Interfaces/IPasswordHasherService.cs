namespace TownBites.Infrastructure.Application.Authentication.Interfaces;

/// <summary>
/// Provides password hashing and verification.
/// </summary>
public interface IPasswordHasherService
{
    string HashPassword(string password);

    bool VerifyPassword(string hashedPassword, string password);
}