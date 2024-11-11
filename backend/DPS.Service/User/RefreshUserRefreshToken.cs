using DPS.Data.Entities;
using DPS.Service.Common;
using Microsoft.EntityFrameworkCore;

namespace DPS.Service.User;

public class RefreshTokenRequest {
	public required string CurrentRefreshToken { get; init; }
}
public class RefreshTokenResponse {
	public required string AccessToken { get; init; }
	public required string RefreshToken { get; init; }
	public  required int ExpiresIn { get; init; }
	public required bool IsSessionToken { get; init; }
}

public partial class UserService
{
	public async Task<AppResponse<RefreshTokenResponse>> UserRefreshTokenAsync(RefreshTokenRequest request)
	{
		//get current token
		var oldRefreshToken = applicationDbContext.UserRefreshTokens.Include(userRefreshToken => userRefreshToken.User)
			.FirstOrDefault(m => m.RefreshToken == request.CurrentRefreshToken);
		
		//perform validations
		if (oldRefreshToken == null || !oldRefreshToken.IsValid || DateTime.UtcNow > oldRefreshToken.ExpirationDate) 
			return AppResponse<RefreshTokenResponse>.GetErrorResponse("token_not_valid");
		var user = await userManager.FindByIdAsync(oldRefreshToken.User.Id);
		if (user == null)
			return AppResponse<RefreshTokenResponse>.GetErrorResponse("could_not_find_user", "User not found");

		//generate a new refresh token and new access token
		var userClaims = GetUserClaims(user);
		var newAccessToken = TokenUtils.GetAccessToken(tokenSettings, user, userClaims);
		var newRefreshToken = TokenUtils.GetRefreshToken();
		
		//store new token and invalidate previous one
		var refreshToken = new UserRefreshToken
		{
			RefreshToken = newRefreshToken,
			IsValid = true,
			ExpirationDate = DateTime.Now.AddSeconds(tokenSettings.RefreshTokenExpireSeconds),
			User = user,
			IsSessionToken = oldRefreshToken.IsSessionToken,
			PreviousRefreshToken = oldRefreshToken.RefreshToken
		};
		applicationDbContext.UserRefreshTokens.Add(refreshToken);
		oldRefreshToken.IsValid = false;
		await applicationDbContext.SaveChangesAsync();
		
		return AppResponse<RefreshTokenResponse>.GetSuccessResponse(
			new RefreshTokenResponse()
			{
				ExpiresIn = tokenSettings.TokenExpireSeconds,
				AccessToken = newAccessToken,
				RefreshToken = refreshToken.RefreshToken,
				IsSessionToken = refreshToken.IsSessionToken
			});
	}
	
	
}