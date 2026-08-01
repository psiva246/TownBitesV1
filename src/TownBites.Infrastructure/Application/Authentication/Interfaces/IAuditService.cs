namespace TownBites.Infrastructure.Interfaces;

public interface IAuditService
{
    Task LogAsync(string userId, string userName, string action, string entityName,
        int entityId, object? oldValues = null, object? newValues = null);
}