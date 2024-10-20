using DPS.Service;
using DPS.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DPS.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController(UserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<AppResponse<bool>> Register(UserRegisterRequest req)
        {
            return await userService.UserRegisterAsync(req);
        }

        [HttpPost]
        public async Task<AppResponse<UserLoginResponse>> Login(UserLoginRequest req)
        {
            return await userService.UserLoginAsync(req);
        }

        [HttpPost]
        public async Task<AppResponse<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest req)
        {
            return await userService.UserRefreshTokenAsync(req);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
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
}