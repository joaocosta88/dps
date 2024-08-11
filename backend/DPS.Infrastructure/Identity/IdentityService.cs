using DPS.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DPS.Infrastructure.Identity;

public class IdentityService : IIdentityService {
	private readonly UserManager<ApplicationUser> _userManager;
	SignInManager<ApplicationUser> signInManager;

	private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
	private readonly IAuthorizationService _authorizationService;

	public IdentityService(
		UserManager<ApplicationUser> userManager,
		IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
		IAuthorizationService authorizationService)
	{
		_userManager = userManager;
		_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
		_authorizationService = authorizationService;
	}

	public async Task<IdentityUser?> CreateUserAsync(string email, string password)
	{
		var hasUser = await _userManager.FindByEmailAsync(email) != null;
		if (hasUser)
		{
			return null;
		}

		var user = new ApplicationUser
		{
			Email = email,
		};

		var result = await _userManager.CreateAsync(user, password);
		if (!result.Succeeded)
		{
			return null;
		}

		return user;
	}


	public async Task<IdentityUser?> LoginAsync(string email, string password)
	{
		var user = await _userManager.FindByEmailAsync(email);

		if (user != null && await _userManager.CheckPasswordAsync(user, password))
		{
			return user;
		}

		return null;
	}

	public async Task<bool> AuthorizeAsync(string userId, string policyName)
	{
		var user = await _userManager.FindByIdAsync(userId);

		if (user == null)
		{
			return false;
		}

		var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

		var result = await _authorizationService.AuthorizeAsync(principal, policyName);

		return result.Succeeded;
	}

	public async Task DeleteUserAsync(string userId)
	{
		var user = await _userManager.FindByIdAsync(userId);
		if (user == null)
			return;

		await DeleteUserAsync(user);
	}

	public async Task DeleteUserAsync(ApplicationUser user)
	{
		await _userManager.DeleteAsync(user);
	}

	public Task<bool> IsInRoleAsync(string userId, string role)
	{
		throw new NotImplementedException();
	}
}
