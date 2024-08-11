﻿namespace DPS.Service.User;

public class UserLoginRequest {
	public string Email { get; set; } = "";
	public string Password { get; set; } = "";
}
public class UserLoginResponse {
	public string AccessToken { get; set; } = "";
	public string RefreshToken { get; set; } = "";

	public int ExpiresIn { get; set; }
}

public partial class UserService {
	public async Task<AppResponse<UserLoginResponse>> UserLoginAsync(UserLoginRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user == null)
			return new AppResponse<UserLoginResponse>().SetErrorResponse("email", "Email not found");


		var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
		if (!result.Succeeded)
			return new AppResponse<UserLoginResponse>().SetErrorResponse("password", result.ToString());

		var token = await GenerateUserToken(user);
		return new AppResponse<UserLoginResponse>().SetSuccessResponse(token);
	}
}