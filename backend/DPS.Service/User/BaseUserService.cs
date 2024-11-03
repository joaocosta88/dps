using DPS.Data;
using DPS.Data.Entities;
using DPS.Service.User.Common;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DPS.Service.User
{
    public partial class UserService(UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager,
		RoleManager<IdentityRole> roleManager,
		ApplicationDbContext applicationDbContext,
		TokenSettings tokenSettings) {
	    
		private async Task<UserLoginResponse> GenerateUserToken(ApplicationUser user)
		{
			var claims = (from ur in applicationDbContext.UserRoles
						  where ur.UserId == user.Id
						  join r in applicationDbContext.Roles on ur.RoleId equals r.Id
						  join rc in applicationDbContext.RoleClaims on r.Id equals rc.RoleId
						  select rc)
			  .Where(rc => !string.IsNullOrEmpty(rc.ClaimValue) && !string.IsNullOrEmpty(rc.ClaimType))
			  .Select(rc => new Claim(rc.ClaimType!, rc.ClaimValue!))
			  .Distinct()
			  .ToList();

			var roleClaims = (from ur in applicationDbContext.UserRoles
							  where ur.UserId == user.Id
							  join r in applicationDbContext.Roles on ur.RoleId equals r.Id
							  select r)
			  .Where(r => !string.IsNullOrEmpty(r.Name))
			  .Select(r => new Claim(ClaimTypes.Role, r.Name!))
			  .Distinct()
			  .ToList();

			claims.AddRange(roleClaims);

			var token = TokenUtils.GetToken(tokenSettings, user, claims); 
			
			await userManager.RemoveAuthenticationTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken");
			
			var refreshToken = await userManager.GenerateUserTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken");
			await userManager.SetAuthenticationTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken", refreshToken);
			return new UserLoginResponse()
			{
				AccessToken = token, 
				RefreshToken = refreshToken, 
				ExpiresIn = tokenSettings.TokenExpireSeconds,
				Roles = claims.Select(m => m.ToString()).ToList(),
				RoleClaims = claims.Select(m => m.ToString()).ToList(),
				
			};
		}
	}
}
