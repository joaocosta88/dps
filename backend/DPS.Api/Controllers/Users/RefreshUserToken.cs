using DPS.Api.Controllers.Users.CommonModels;
using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;


public partial class UsersController
{
    [HttpPost]
    public async Task<IActionResult> RefreshToken()
    {
        var currentRefreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(currentRefreshToken))
        {
            return Unauthorized("Refresh token is missing.");
        }

        var currentAccessToken = Request.Headers.Authorization.ToString().Substring("Bearer ".Length).Trim();
        
        RefreshTokenRequest req = new()
        {
            RefreshToken = currentRefreshToken,
            AccessToken = currentAccessToken
        };
        
        var res = await userService.UserRefreshTokenAsync(req);
      
        //store refresh token in cookie
        if (!string.IsNullOrWhiteSpace(res.Data?.RefreshToken))
            Response.Cookies.Append("refreshToken", res.Data?.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        
        return Ok(new AuthResponseModel
        {
            AccessToken = res.Data.AccessToken,
            ExpiresIn = res.Data.ExpiresIn
        });
    }
}