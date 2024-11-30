using DPS.Service.Auth.Common;

namespace DPS.Service.Auth;

public class ConfirmUserAccountRequest
{
    public required string Token { get; init; }
}

public partial class AuthService 
{
    public async Task<AppResponse<bool>> ConfirmUserAccountAsync(ConfirmUserAccountRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            return AppResponse<bool>.GetErrorResponse("missing_parameter");

        var token = Uri.UnescapeDataString(request.Token);
        var (email, confirmAccountToken) = AuthTokensHelper.DecodeAuthOperationToken(token);
        
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return AppResponse<bool>.GetErrorResponse("could_not_find_user", "User not found");

        var result = await userManager.ConfirmEmailAsync(user, confirmAccountToken);
        return !result.Succeeded 
            ? AppResponse<bool>.GetErrorResponse("general_auth_error", errorDetails: GetIdentityErrors(result)) 
            : AppResponse<bool>.GetSuccessResponse(true);
    }    
}