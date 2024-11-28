using DPS.Data.Entities;

namespace DPS.Service.Auth;

public class UserRegisterRequest {
	public  required string Email { get; init; }
	public required string Password { get; init; }
}

public partial class AuthService {
	public async Task<AppResponse<bool>> UserRegisterAsync(UserRegisterRequest request)
	{
		var userExists = await userManager.FindByEmailAsync(request.Email);
		if (userExists != null)
			return AppResponse<bool>.GetErrorResponse("duplicate_email");
		
		var user = new ApplicationUser()
		{
			UserName = request.Email,
			Email = request.Email,
			EmailConfirmed = false
		};
		
		//userManager.GenerateEmailConfirmationTokenAsync()
		var result = await userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
			return AppResponse<bool>.GetErrorResponse("general_register_error", errorDetails: GetIdentityErrors(result));
		
		var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
		var accountConfirmationUrl = urlFactory.GetAccountConfirmationUrl(token);
		await emailSender.SendConfirmAccountEmailAsync(user.Email, accountConfirmationUrl);
		
		return AppResponse<bool>.GetSuccessResponse(true);
	}
}