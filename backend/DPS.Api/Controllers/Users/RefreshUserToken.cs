using DPS.Api.Controllers.Users.CommonModels;
using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;


public partial class UsersController
{
    [HttpPost]
    public async Task<IActionResult> RefreshToken()
    {
        //get refresh token from cookie
        var currentRefreshToken = GetRefreshTokenCookie();
        if (string.IsNullOrEmpty(currentRefreshToken))
        {
            return Unauthorized("Refresh token is missing.");
        }

        //get access token from header
        var currentAccessToken = Request.Headers.Authorization.ToString().Substring("Bearer ".Length).Trim();
        
        RefreshTokenRequest req = new()
        {
            RefreshToken = currentRefreshToken,
            AccessToken = currentAccessToken
        };
        
        //create and store refresh token in cookie
        var res = await userService.UserRefreshTokenAsync(req);
        if (!string.IsNullOrWhiteSpace(res.Data?.RefreshToken))
            StoreRefreshTokenCookie(res.Data.RefreshToken);

        if (!res.Success)
            return Unauthorized();
        
        return Ok(new AuthResponseModel
        {
            AccessToken = res.Data.AccessToken,
            ExpiresIn = res.Data.ExpiresIn
        });
    }
}