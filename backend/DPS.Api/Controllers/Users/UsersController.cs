using DPS.Service.Common;
using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class UsersController(
        UserService userService,
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
}