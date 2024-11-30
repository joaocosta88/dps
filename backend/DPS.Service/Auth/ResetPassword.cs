using DPS.Service.Auth.Common;

namespace DPS.Service.Auth;

public struct SetNewPasswordRequest
{    
    public required string Password { get; set; }
    public required string Token { get; set; }
}

public partial class AuthService
{
    public async Task<AppResponse<bool>> ResetPassword(SetNewPasswordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Token))
            return AppResponse<bool>.GetErrorResponse("missing_parameter");

        var token = Uri.UnescapeDataString(request.Token);
        var (email, resetPasswordToken) = AuthTokensHelper.DecodeAuthOperationToken(token);
        
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return AppResponse<bool>.GetErrorResponse("could_not_find_user", "User not found");

        var result = await userManager.ResetPasswordAsync(user, resetPasswordToken, request.Password);
        return !result.Succeeded
            ? AppResponse<bool>.GetErrorResponse("general_auth_error", errorDetails: GetIdentityErrors(result))
            : AppResponse<bool>.GetSuccessResponse(true);
    }
}