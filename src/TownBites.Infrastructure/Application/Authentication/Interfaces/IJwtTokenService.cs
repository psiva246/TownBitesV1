using TownBites.Domain.Entities;

namespace TownBites.Infrastructure.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}