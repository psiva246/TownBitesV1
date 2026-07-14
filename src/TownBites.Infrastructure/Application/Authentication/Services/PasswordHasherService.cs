using Microsoft.AspNetCore.Identity;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Application.Authentication.Interfaces;

namespace TownBites.Infrastructure.Application.Authentication.Services;

/// <summary>
/// Password hashing service.
/// </summary>
public class PasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public string HashPassword(string password)
    {
        var user = new User();

        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        var user = new User();

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            hashedPassword,
            password);

        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}