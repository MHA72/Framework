namespace SentinelCore.Application.Interfaces;

public interface IEntityChangeLogger
{
    Task LogCreateAsync<T>(T entity) where T : class;
    Task LogUpdateAsync<T>(T original, T updated) where T : class;
    Task LogDeleteAsync<T>(T entity) where T : class;
}