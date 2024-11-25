namespace DPS.Email;

public struct EmailConfig
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required string FromAddress { get; init; }
    public required string FromName { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}