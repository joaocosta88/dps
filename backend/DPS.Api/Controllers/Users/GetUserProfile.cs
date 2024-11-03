using System.Security.Claims;
using DPS.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Users;

public partial class UsersController
{
    [HttpGet]
    [Authorize]
    public IActionResult GetUserProfile()
    {
        return Ok(AppResponse<object>.GetSuccessResponse(
            new
            {
                Id = User.FindFirstValue("Id"),
                Username = User.FindFirstValue("UserName"),
                Email = User.FindFirstValue("Email"),
                IsAdmin = User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList()
            }
        ));
    }
}