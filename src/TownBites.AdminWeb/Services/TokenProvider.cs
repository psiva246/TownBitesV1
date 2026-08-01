using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TownBites.AdminWeb.Interfaces;

namespace TownBites.AdminWeb.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetToken()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst("AccessToken")?.Value;
    }
}