﻿namespace DPS.Service.User;

public class UserLoginRequest {
	public required string Email { get; init; }
	public required string Password { get; init; }
	public required bool KeepLoggedIn { get; init; }
}
public class UserLoginResponse {
	public required string AccessToken { get; init; }
	public required string RefreshToken { get; init; }
	public required int ExpiresIn { get; init; }
}

public partial class UserService {
	public async Task<AppResponse<UserLoginResponse>> UserLoginAsync(UserLoginRequest request)
	{
		var user = await userManager.FindByEmailAsync(request.Email);
		if (user == null)
			return AppResponse<UserLoginResponse>.GetErrorResponse("email_not_found", "Email not found");


		var isValidPassword = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
		if (!isValidPassword.Succeeded)
			return AppResponse<UserLoginResponse>.GetErrorResponse("invalid_password", isValidPassword.ToString());

		var result = await GenerateUserTokensAsync(user, request.KeepLoggedIn);
		
		return AppResponse<UserLoginResponse>.GetSuccessResponse(new UserLoginResponse()
		{
			AccessToken = result.AccessToken,
			RefreshToken = result.RefreshToken,
			ExpiresIn = result.AccessTokenExpiresIn
		});
	}
}