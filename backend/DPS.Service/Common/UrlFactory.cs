namespace DPS.Service.Common;

public class UrlFactory(string baseUrl)
{
    public string GetAccountConfirmationUrl(string token)
        => $"{baseUrl}/ConfirmAccount?token={Uri.EscapeDataString(token)}";

    public string GetPasswordResetUrl(string token)
        => $"{baseUrl}/ResetPassword?token={Uri.EscapeDataString(token)}";
}