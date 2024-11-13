using DPS.Service.Auth;
using DPS.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

[ApiController]
[Route("[controller]/[action]")]
public partial class AuthController(
    AuthService authService,
    CookieSettings cookieSettings,
    TokenSettings tokenSettings) : ControllerBase
{
    private void StoreRefreshTokenCookie(string refreshToken, bool isSession)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = cookieSettings.Secure,
            SameSite = cookieSettings.SameSite,
        };

        if (!isSession)
            cookieOptions.Expires = DateTime.UtcNow.AddSeconds(tokenSettings.RefreshTokenExpireSeconds);

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    private string? GetRefreshTokenCookie()
        => Request.Cookies["refreshToken"];

    private void RemoveRefreshTokenCookie()
        => Response.Cookies.Delete("refreshToken");
}