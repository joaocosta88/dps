using DPS.Api.Controllers.Users.CommonModels;
using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;

public partial class UsersController
{
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequest req)
    {
        var res = await userService.UserLoginAsync(req);
        if (!res.Success)
        {
            return BadRequest(res.Error.Message);
        }
        //store refresh token in cookie
        if (!string.IsNullOrWhiteSpace(res.Data?.RefreshToken))
            Response.Cookies.Append("refreshToken", res.Data?.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true in production
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