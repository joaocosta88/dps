using DPS.Service.Common;
using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class UsersController(UserService userService, CookieSettings cookieSettings, TokenSettings tokenSettings) : ControllerBase
    {
        private void StoreRefreshTokenCookie(string refreshToken)
        {
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = cookieSettings.Secure,
                SameSite = cookieSettings.SameSite,
                Expires = DateTime.UtcNow.AddSeconds(tokenSettings.RefreshTokenExpireSeconds)
            });
        }

        private string? GetRefreshTokenCookie()
            => Request.Cookies["refreshToken"];
    }
}