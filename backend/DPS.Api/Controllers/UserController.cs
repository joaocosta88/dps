using DPS.Service;
using DPS.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers {
	[ApiController]
	[Route("[controller]/[action]")]
	public class UserController(UserService userService) : ControllerBase {
		private readonly UserService _userService = userService;

		[HttpPost]
		public async Task<AppResponse<bool>> Register(UserRegisterRequest req)
		{
			return await _userService.UserRegisterAsync(req);
		}

		[HttpPost]
		public async Task<AppResponse<UserLoginResponse>> Login(UserLoginRequest req)
		{
			return await _userService.UserLoginAsync(req);
		}

		[HttpPost]
		public async Task<AppResponse<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest req)
		{
			return await _userService.UserRefreshTokenAsync(req);
		}

		[HttpGet]
		[Authorize]
		public IActionResult Profile()
		{
			return Ok(new AppResponse<object>()
			{
				Data = new
				{
					Id = User.FindFirstValue("Id"),
					Username = User.FindFirstValue("UserName"),
					Email = User.FindFirstValue("Email"),
					IsAdmin = User.Claims
					   .Where(c => c.Type == ClaimTypes.Role)
					   .Select(c => c.Value)
					   .ToList()
				},
				IsSucceed = true
			});
		}
	}
}
