using DPS.Service.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await authService.InitResetUserPassword(request.Email);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] SetNewPasswordRequest request)
    {
        await authService.ResetPassword(request);
        return Ok();
    }
}