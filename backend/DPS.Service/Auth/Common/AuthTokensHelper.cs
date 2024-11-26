namespace DPS.Service.Auth.Common;

public static class AuthUrlHelper
{
    public static string CreateConfirmAccountUrl(string email, string token)
        => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{email}:{token}"));

    public static (string email, string confirmationToken) DecodeConfirmAccountToken(string token)
    {
       var str = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
       var tokens = str.Split(":");
       return (tokens[0], tokens[1]);
    }
}