namespace SentinelCore.API.Middlewares;

[AttributeUsage(AttributeTargets.Method)]
public class PermissionAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}
