using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;

public partial class UsersController
{
    [HttpGet("{id}")]
    public IActionResult GetByUsername(string id)
    {
        var user = userService.GetUserById(id);
        if (user is null)
            return NotFound();

        return Ok(user);
    }
}