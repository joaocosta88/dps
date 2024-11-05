using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DPS.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DPS.Service.Common;

public static class TokenUtils {
	public static string GetToken(TokenSettings appSettings, ApplicationUser user, List<Claim> roleClaims)
	{
		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.SecretKey));
		var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

		var userClaims = new List<Claim>
			{
				new("Id", user.Id.ToString()),
				new ("UserName", user.UserName??""),
				new("Email", user.Email)
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

	public static ClaimsPrincipal GetPrincipalFromExpiredToken(TokenSettings tokenSettings, string token)
	{
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidAudience = tokenSettings.Audience,
			ValidIssuer = tokenSettings.Issuer,
			ValidateLifetime = false,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey))
		};

		var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
		if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			throw new SecurityTokenException("GetPrincipalFromExpiredToken Token is not validated");

		return principal;
	}
}