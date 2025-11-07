namespace SentinelCore.Core.Models.Response;

public sealed record AuthResponse(string Token, string Username, string Role);