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
		var principal = TokenUtils.GetPrincipalFromExpiredToken(_tokenSettings, request.AccessToken);

		if (principal == null || principal.FindFirst("UserName")?.Value == null)
			return new AppResponse<RefreshTokenResponse>().SetErrorResponse("email", "User not found");

		var user = await _userManager.FindByNameAsync(principal.FindFirst("UserName")?.Value ?? "");

		if (user == null)
			return new AppResponse<RefreshTokenResponse>().SetErrorResponse("email", "User not found");

		if (!await _userManager.VerifyUserTokenAsync(user, "REFRESHTOKENPROVIDER", "RefreshToken", request.RefreshToken))
		{
			return new AppResponse<RefreshTokenResponse>().SetErrorResponse("token", "Refresh token expired");
		}

		var token = await GenerateUserToken(user);
		return new AppResponse<RefreshTokenResponse>().SetSuccessResponse(new RefreshTokenResponse() { ExpiresIn = token.ExpiresIn, AccessToken = token.AccessToken, RefreshToken = token.RefreshToken });

	}
}