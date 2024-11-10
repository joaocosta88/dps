using DPS.Data;
using DPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Cryptography;
using DPS.Service.Common;

namespace DPS.Service.User
{
    public partial class UserService(UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager,
		RoleManager<IdentityRole> roleManager,
		ApplicationDbContext applicationDbContext,
		TokenSettings tokenSettings) {
	    
		private async Task<UserLoginResponse> GenerateUserTokenAsync(ApplicationUser user)
		{
			var res = GenerateAccessToken(user);
			var refreshToken = await GenerateRefreshTokenAsync(user);
			
			return new UserLoginResponse()
			{
				AccessToken = res.Item1, 
				RefreshToken = refreshToken, 
				ExpiresIn = tokenSettings.TokenExpireSeconds,
				Roles = res.Item2
			};
		}

		private (string, IList<string>) GenerateAccessToken(ApplicationUser user)
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
			var claimsResult = claims.Select(m => m.ToString()).ToList();

			var accessToken = TokenUtils.GetToken(tokenSettings, user, claims);
			return (accessToken, claimsResult);
		}

		private async Task<string> GenerateRefreshTokenAsync(ApplicationUser user)
		{
			var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

			var refreshToken = new UserRefreshToken
			{
				Id = default,
				RefreshToken = token,
				IsValid = true,
				ExpirationDate = DateTime.Now.AddSeconds(tokenSettings.RefreshTokenExpireSeconds),
				User = user
			};
			applicationDbContext.UserRefreshTokens.Add(refreshToken);
			await applicationDbContext.SaveChangesAsync();

			return token;
		}
	}
}
