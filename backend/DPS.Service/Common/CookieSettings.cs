using Microsoft.AspNetCore.Http;

namespace DPS.Service.Common;

public class CookieSettings
{
    public bool Secure { get; init; }
    public SameSiteMode SameSite { get; init; }
}