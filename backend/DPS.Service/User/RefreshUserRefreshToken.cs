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
		var userRefreshToken = applicationDbContext.UserRefreshTokens.Include(userRefreshToken => userRefreshToken.User)
			.FirstOrDefault(m => m.RefreshToken == request.CurrentRefreshToken);
		
		if (userRefreshToken == null || !userRefreshToken.IsValid || DateTime.UtcNow > userRefreshToken.ExpirationDate) 
			return AppResponse<RefreshTokenResponse>.GetErrorResponse("token_not_valid");
		
		var user = await userManager.FindByIdAsync(userRefreshToken.User.Id);

		if (user == null)
			return AppResponse<RefreshTokenResponse>.GetErrorResponse("could_not_find_user", "User not found");

		var token = await GenerateUserTokensAsync(user, userRefreshToken.IsSessionToken);
		
		return AppResponse<RefreshTokenResponse>.GetSuccessResponse(
			new RefreshTokenResponse()
			{
				ExpiresIn = token.AccessTokenExpiresIn,
				AccessToken = token.AccessToken, 
				RefreshToken = token.RefreshToken,
				IsSessionToken = token.IsSessionToken
			});
	}
	
	
}