namespace SentinelCore.Core.Models.Request;

public sealed record RegisterRequest(string Username, string MobileNumber, string Password, string Role = "User");

