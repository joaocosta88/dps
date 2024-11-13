using DPS.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public async Task<IActionResult> LogoutAsync()
    {
        var currentRefreshToken = GetRefreshTokenCookie();
        if (string.IsNullOrEmpty(currentRefreshToken))
        {
            return Unauthorized("Refresh token is missing.");
        }

        await authService.LogoutUserAsync(new LogoutRequest()
        {
            CurrentRefreshToken = currentRefreshToken
        });
        
        RemoveRefreshTokenCookie();
        
        return NoContent();
    }
}