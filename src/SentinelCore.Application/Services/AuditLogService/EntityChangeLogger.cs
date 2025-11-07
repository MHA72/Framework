using SentinelCore.Core.Models.Request;
using SentinelCore.Application.Interfaces.AuditLogContract;

namespace SentinelCore.Application.Services.AuditLogService;

public class EntityChangeLogger(IAuditService audit) : IEntityChangeLogger
{
    public async Task LogCreateAsync<T>(T entity) where T : class =>
        await audit.LogAsync(new AuditLogRequest(GetEntityId(entity), null,
            $"رکورد جدید از نوع {typeof(T).Name} ایجاد شد", "Create", typeof(T).Name));

    public async Task LogUpdateAsync<T>(T original, T updated) where T : class
    {
        var props = typeof(T).GetProperties();

        foreach (var prop in props)
        {
            var oldValue = prop.GetValue(original)?.ToString();
            var newValue = prop.GetValue(updated)?.ToString();

            if (oldValue != newValue)
            {
                await audit.LogAsync(new AuditLogRequest(GetEntityId(updated), null,
                    $"فیلد {prop.Name} تغییر کرد از '{oldValue}' به '{newValue}'", "Update", typeof(T).Name));
            }
        }
    }

    public async Task LogDeleteAsync<T>(T entity) where T : class
    {
        await audit.LogAsync(new AuditLogRequest(GetEntityId(entity), null,
            $"رکورد از نوع {typeof(T).Name} حذف شد", "Delete", typeof(T).Name));
    }

    private string? GetEntityId<T>(T entity)
    {
        var idProp = typeof(T).GetProperty("Id");
        return idProp?.GetValue(entity)?.ToString();
    }
}