using DPS.Api.Controllers.Auth.CommonModels;
using DPS.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequest req)
    {
        var res = await authService.UserLoginAsync(req);
        if (!res.Success)
        {
            return BadRequest(res.Error?.Message);
        }
        
        //store refresh token in cookie
        if (!string.IsNullOrWhiteSpace(res?.Data?.RefreshToken))
            StoreRefreshTokenCookie(res.Data.RefreshToken, isSession: !req.KeepLoggedIn);
        
        return Ok(new AuthResponseModel
        {
            AccessToken = res.Data.AccessToken,
            ExpiresIn = res.Data.ExpiresIn
        });
    }   
}