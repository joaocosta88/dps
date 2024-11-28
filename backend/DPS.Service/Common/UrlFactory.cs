namespace DPS.Service.Common;

public class UrlFactory(string baseUrl)
{
    public string GetAccountConfirmationUrl(string token)
        => $"{baseUrl}/auth/ConfirmAccount?token={Uri.EscapeDataString(token)}";

    public string GetPasswordResetUrl(string token)
        => $"{baseUrl}/auth/PasswordReset?token={Uri.EscapeDataString(token)}";
}