using DPS.Service.User.Common;

namespace DPS.Service.User;

public class RefreshTokenRequest {
	public string AccessToken { get; set; } = "";
	public string RefreshToken { get; set; } = "";
}
public class RefreshTokenResponse {
	public string AccessToken { get; set; } = "";
	public string RefreshToken { get; set; } = "";
	public int ExpiresIn { get; set; }
}

public partial class UserService
{
	public async Task<AppResponse<RefreshTokenResponse>> UserRefreshTokenAsync(RefreshTokenRequest request)
	{
		var principal = TokenUtils.GetPrincipalFromExpiredToken(tokenSettings, request.AccessToken);

		if (principal == null || principal.FindFirst("UserName")?.Value == null)
			return AppResponse<RefreshTokenResponse>.GetErrorResponse("error_extracting_principal_from_token", "User not found");

		var user = await userManager.FindByNameAsync(principal.FindFirst("UserName")?.Value ?? "");

		if (user == null)
			return AppResponse<RefreshTokenResponse>.GetErrorResponse("could_not_find_user", "User not found");

		if (!await userManager.VerifyUserTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken", request.RefreshToken))
		{
			return AppResponse<RefreshTokenResponse>.GetErrorResponse("token_not_valid", "Refresh token expired");
		}

		var token = await GenerateUserToken(user);
		return AppResponse<RefreshTokenResponse>.GetSuccessResponse(new RefreshTokenResponse() { ExpiresIn = token.ExpiresIn, AccessToken = token.AccessToken, RefreshToken = token.RefreshToken });

	}
}