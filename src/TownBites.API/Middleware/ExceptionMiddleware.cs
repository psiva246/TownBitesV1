using System.Net;
using System.Text.Json;
using TownBites.Shared.Common;
//using TownBites.Shared.Responses;

namespace TownBites.API.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid().ToString("N");

            _logger.LogError(ex, "Unhandled exception. ErrorId: {ErrorId}", errorId);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.Fail(
                "An unexpected error occurred.",
                new ApiError
                {
                    Code = "SERVER_ERROR", Message = $"Reference Id: {errorId}"
                });

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}