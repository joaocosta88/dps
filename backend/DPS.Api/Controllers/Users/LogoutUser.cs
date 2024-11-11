using DPS.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;

public partial class UsersController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> LogoutAsync()
    {
        var currentRefreshToken = GetRefreshTokenCookie();
        if (string.IsNullOrEmpty(currentRefreshToken))
        {
            return Unauthorized("Refresh token is missing.");
        }

        await userService.LogoutUserAsync(new LogoutRequest()
        {
            CurrentRefreshToken = currentRefreshToken
        });
        
        RemoveRefreshTokenCookie();
        
        return NoContent();
    }
}