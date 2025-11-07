namespace SentinelCore.Core.Models.Request;

public sealed record RegisterRequest(string Username, string Email, string Password, string Role = "User");

