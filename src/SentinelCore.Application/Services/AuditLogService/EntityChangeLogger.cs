using SentinelCore.Core.Models;
using SentinelCore.Application.Interfaces.AuditLog;

namespace SentinelCore.Application.Services.AuditLog;

public class EntityChangeLogger(IAuditService audit) : IEntityChangeLogger
{
    public async Task LogCreateAsync<T>(T entity) where T : class
    {
        await audit.LogAsync(new AuditLogRequest
        {
            ActionType = "Create",
            EntityName = typeof(T).Name,
            EntityId = GetEntityId(entity),
            Message = $"رکورد جدید از نوع {typeof(T).Name} ایجاد شد"
        });
    }

    public async Task LogUpdateAsync<T>(T original, T updated) where T : class
    {
        var props = typeof(T).GetProperties();

        foreach (var prop in props)
        {
            var oldValue = prop.GetValue(original)?.ToString();
            var newValue = prop.GetValue(updated)?.ToString();

            if (oldValue != newValue)
            {
                await audit.LogAsync(new AuditLogRequest
                {
                    ActionType = "Update",
                    EntityName = typeof(T).Name,
                    EntityId = GetEntityId(updated),
                    Message = $"فیلد {prop.Name} تغییر کرد از '{oldValue}' به '{newValue}'"
                });
            }
        }
    }

    public async Task LogDeleteAsync<T>(T entity) where T : class
    {
        await audit.LogAsync(new AuditLogRequest
        {
            ActionType = "Delete",
            EntityName = typeof(T).Name,
            EntityId = GetEntityId(entity),
            Message = $"رکورد از نوع {typeof(T).Name} حذف شد"
        });
    }

    private string? GetEntityId<T>(T entity)
    {
        var idProp = typeof(T).GetProperty("Id");
        return idProp?.GetValue(entity)?.ToString();
    }
}