using DPS.Service;
using DPS.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;

public partial class UsersController
{
    [HttpPost]
    public async Task<AppResponse<bool>> Register(UserRegisterRequest req)
    {
        return await userService.UserRegisterAsync(req);
    }
}