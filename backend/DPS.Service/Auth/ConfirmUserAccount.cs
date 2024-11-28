using DPS.Service.Auth.Common;

namespace DPS.Service.Auth;

public partial class AuthService 
{
    public async Task<AppResponse<bool>> ConfirmUserAccountAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return AppResponse<bool>.GetErrorResponse("missing_parameter");
  
        var (email, confirmationToken) = AuthTokensHelper.DecodeAuthOperationToken(token);
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(confirmationToken))
            return AppResponse<bool>.GetErrorResponse("missing_parameter");

        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return AppResponse<bool>.GetErrorResponse("invalid_parameter");
        
        var result = await userManager.ConfirmEmailAsync(user, confirmationToken);
        return !result.Succeeded 
            ? AppResponse<bool>.GetErrorResponse("general_auth_error", errorDetails: GetIdentityErrors(result)) 
            : AppResponse<bool>.GetSuccessResponse(true);
    }    
}