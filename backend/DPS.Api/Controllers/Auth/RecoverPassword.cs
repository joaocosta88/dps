using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
    {
        return Ok();
    }
}