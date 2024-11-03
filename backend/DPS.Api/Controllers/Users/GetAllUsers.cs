using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;

public partial class UsersController
{
    [HttpGet]
    [Authorize]
    public IActionResult All()
    {
        return Ok(userService.GetAll());
    }
}