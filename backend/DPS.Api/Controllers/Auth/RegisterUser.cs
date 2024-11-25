using DPS.Service;
using DPS.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Auth;

public partial class AuthController
{
    [HttpPost]
    public async Task<AppResponse<bool>> Register(UserRegisterRequest req)
    {
        return await authService.UserRegisterAsync(req);
    }

    [HttpPost]
    public IActionResult ConfirmAccountRegistration(UserRegisterRequest req)
    {
        return Ok();
    }
}