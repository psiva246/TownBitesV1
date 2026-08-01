using System.Text.Json;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;

namespace TownBites.Infrastructure.Services;

public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _db;

    public AuditService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task LogAsync(string userId, string userName, string action, string entityName,
        int entityId, object? oldValues = null, object? newValues = null)
    {
        var audit = new AuditLog
        {
            UserId = userId,
            UserName = userName,
            Action = action,
            EntityName = entityName,
            EntityId = entityId,
            OldValues = oldValues == null ? null : JsonSerializer.Serialize(oldValues),
            NewValues = newValues == null ? null : JsonSerializer.Serialize(newValues),
            CreatedOn = DateTime.UtcNow
        };

        _db.AuditLogs.Add(audit);

        await _db.SaveChangesAsync();
    }
}