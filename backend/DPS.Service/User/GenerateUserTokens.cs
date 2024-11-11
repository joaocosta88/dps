using System.Security.Claims;
using DPS.Data.Entities;
using DPS.Service.Common;

namespace DPS.Service.User;

public partial class UserService 
{
    public struct UserTokensResult
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
        public required int AccessTokenExpiresIn { get; init; }
        public required int RefreshTokenExpiresIn { get; init; }

        public required IList<string> Roles { get; init; }
        public required bool IsSessionToken { get; init; }
    }
    
    private async Task<UserTokensResult> GenerateUserTokensAsync(ApplicationUser user, bool isSessionRefreshToken)
    {
        var claims = GetUserClaims(user);
        var accessToken = GenerateAccessToken(user, claims);
        var refreshToken = await GenerateRefreshTokenAsync(user, isSessionRefreshToken);
			
        return new UserTokensResult()
        {
            AccessToken = accessToken, 
            RefreshToken = refreshToken, 
            AccessTokenExpiresIn = tokenSettings.TokenExpireSeconds,
            RefreshTokenExpiresIn  = tokenSettings.RefreshTokenExpireSeconds,
            Roles = claims.Select(m => m.ToString()).ToList(),
            IsSessionToken = isSessionRefreshToken
        };
    }

    private string GenerateAccessToken(ApplicationUser user, IList<Claim> claims)
    {
        return TokenUtils.GetAccessToken(tokenSettings, user, claims);
    }
    
    private async Task<string> GenerateRefreshTokenAsync(ApplicationUser user, bool isSessionToken)
    {
        var token = TokenUtils.GetRefreshToken();

        var refreshToken = new UserRefreshToken
        {
            RefreshToken = token,
            IsValid = true,
            ExpirationDate = DateTime.Now.AddSeconds(tokenSettings.RefreshTokenExpireSeconds),
            User = user,
            IsSessionToken = isSessionToken
        };
        applicationDbContext.UserRefreshTokens.Add(refreshToken);
        await applicationDbContext.SaveChangesAsync();

        return token;
    }

    private IList<Claim> GetUserClaims(ApplicationUser user)
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
        return claims;
    }
}