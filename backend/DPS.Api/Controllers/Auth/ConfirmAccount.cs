using DPS.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public async Task<IActionResult> ConfirmAccount(ConfirmUserAccountRequest request)
    {
        var res 
            = await authService.ConfirmUserAccountAsync(request);

        if (!res.Success)
            return BadRequest();

        return Ok();
    }
}