using DPS.Api.Controllers.Auth.CommonModels;
using DPS.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
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
        
        RefreshTokenRequest req = new()
        {
            CurrentRefreshToken = currentRefreshToken,
        };
        
        //create and store refresh token in cookie
        var res = await authService.UserRefreshTokenAsync(req);
        if (!res.Success)
            return Unauthorized();
        
        if (!string.IsNullOrWhiteSpace(res.Data?.RefreshToken))
            StoreRefreshTokenCookie(res.Data.RefreshToken, res.Data.IsSessionToken);
        
        return Ok(new AuthResponseModel
        {
            AccessToken = res.Data.AccessToken,
            ExpiresIn = res.Data.ExpiresIn
        });
    }
}