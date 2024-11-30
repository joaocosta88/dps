namespace DPS.Service.Auth.Common;

public static class AuthTokensHelper
{
    public static string EncodeAuthOperationToken(string email, string token) 
        => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{email}:{token}"));
    
    public static (string email, string token) DecodeAuthOperationToken(string encodedToken)
    {
       var str = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedToken));
       var tokens = str.Split(":");
       return (tokens[0], tokens[1]);
    }
}