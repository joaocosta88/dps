using DPS.Data.Entities;
using Microsoft.AspNetCore.Identity;

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
		{
			return AppResponse<bool>.GetErrorResponse("duplicate_email");
		}
		
		var user = new ApplicationUser()
		{
			UserName = request.Email,
			Email = request.Email,

		};

		var result = await userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
			return AppResponse<bool>.GetErrorResponse("general_register_error", errorDetails: GetRegisterErrors(result));

		return AppResponse<bool>.GetSuccessResponse(true);
	}

	private Dictionary<string, string[]> GetRegisterErrors(IdentityResult result)
	{
		var errorDictionary = new Dictionary<string, string[]>(1);

		foreach (var error in result.Errors)
		{
			string[] newDescriptions;

			if (errorDictionary.TryGetValue(error.Code, out var descriptions))
			{
				newDescriptions = new string[descriptions.Length + 1];
				Array.Copy(descriptions, newDescriptions, descriptions.Length);
				newDescriptions[descriptions.Length] = error.Description;
			}
			else
			{
				newDescriptions = [error.Description];
			}

			errorDictionary[error.Code] = newDescriptions;
		}

		return errorDictionary;
	}
}