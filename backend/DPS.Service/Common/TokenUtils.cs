using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DPS.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DPS.Service.Common;

public static class TokenUtils
{
    public static string GetAccessToken(TokenSettings appSettings, ApplicationUser user, IList<Claim> roleClaims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.SecretKey));
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var userClaims = new List<Claim>
        {
            new("Id", user.Id),
            new("Email", user.UserName ?? ""),
        };

        userClaims.AddRange(roleClaims);

        var tokeOptions = new JwtSecurityToken(
            issuer: appSettings.Issuer,
            audience: appSettings.Audience,
            claims: userClaims,
            expires: DateTime.UtcNow.AddSeconds(appSettings.TokenExpireSeconds),
            signingCredentials: signInCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }

    public static string GetRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}