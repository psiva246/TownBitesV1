using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Application.Authentication.Interfaces;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Infrastructure.Security;
using TownBites.Shared.Configurations;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using TownBites.Shared.Enums;

namespace TownBites.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(ApplicationDbContext dbContext, IJwtTokenService jwtTokenService, IOptions<JwtSettings> jwtOptions)
    {
        _dbContext = dbContext;
        _jwtTokenService = jwtTokenService;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthResponse> RegisterCustomerAsync(RegisterCustomerRequest request)
    {
        var email = request.Email.Trim().ToLower();

        var exists = await _dbContext.Users.AnyAsync(x => x.Email.ToLower() == email);

        if (exists)
        {
            throw new Exception("Email already registered.");
        }

        var user = new User
        {
            Name = request.Name.Trim(),
            Email = email,
            PhoneNumber = request.PhoneNumber.Trim(),
            PasswordHash = PasswordHasher.Hash(request.Password),
            Role = UserRole.Customer,
            IsActive = true
        };

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        var token = _jwtTokenService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var email = request.Email.Trim().ToLower();

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email && x.IsActive);

        if (user == null)
            return null;

        var passwordValid = PasswordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
            return null;

        int? restaurantId = null;

        if (user.Role == UserRole.Restaurant)
        {
            var restaurant = await _dbContext.Restaurants
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.UserId == user.Id && r.IsActive);

            if (restaurant != null)
            {
                restaurantId = restaurant.Id;
            }
        }

        var token = _jwtTokenService.GenerateToken(user, restaurantId);

        return new AuthResponse
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}