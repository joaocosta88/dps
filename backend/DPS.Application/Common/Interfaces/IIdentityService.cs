using Microsoft.AspNetCore.Identity;

namespace DPS.Application.Common.Interfaces;
public interface IIdentityService {
	Task<bool> IsInRoleAsync(string userId, string role);

	Task<bool> AuthorizeAsync(string userId, string policyName);

	Task<IdentityUser?> LoginAsync(string email, string password);

	Task<IdentityUser?> CreateUserAsync(string email, string password);

	Task DeleteUserAsync(string userId);
}
