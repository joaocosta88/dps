namespace DPS.Service.User;

public class UserLoginRequest {
	public required string Email { get; init; }
	public required string Password { get; init; }
}
public class UserLoginResponse {
	public required string AccessToken { get; init; }
	public required string RefreshToken { get; init; }
	public required int ExpiresIn { get; init; }
	public required IList<string> Roles { get; init; }
	public required IList<string> RoleClaims { get; init; }
}

public partial class UserService {
	public async Task<AppResponse<UserLoginResponse>> UserLoginAsync(UserLoginRequest request)
	{
		var user = await userManager.FindByEmailAsync(request.Email);
		if (user == null)
			return AppResponse<UserLoginResponse>.GetErrorResponse("email_not_found", "Email not found");


		var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
		if (!result.Succeeded)
			return AppResponse<UserLoginResponse>.GetErrorResponse("invalid_password", result.ToString());

		var token = await GenerateUserToken(user);
		return AppResponse<UserLoginResponse>.GetSuccessResponse(token);
	}
}