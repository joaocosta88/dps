using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public async Task<IActionResult> ConfirmAccount(string token)
    {
        var res 
            = await authService.ConfirmUserAccountAsync(token);

        if (!res.Success)
            return BadRequest();

        return Ok();
    }
}