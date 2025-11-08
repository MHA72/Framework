namespace SentinelCore.Application.Contracts.AuditLogContract;

public interface IEntityChangeLoggerContract
{
    Task LogCreateAsync<T>(T entity) where T : class;
    Task LogUpdateAsync<T>(T original, T updated) where T : class;
    Task LogDeleteAsync<T>(T entity) where T : class;
}