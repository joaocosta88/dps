using DPS.Data.Entities;
using System.Security.Claims;

namespace DPS.Api;

public class CurrentUser : IUser {
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CurrentUser(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
}
