using DPS.Service;
using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.User;

public partial class UserController
{
    [HttpPost]
    public async Task<AppResponse<bool>> Register(UserRegisterRequest req)
    {
        return await userService.UserRegisterAsync(req);
    }
}