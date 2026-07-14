using TownBites.Domain.Entities;

namespace TownBites.Infrastructure.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}