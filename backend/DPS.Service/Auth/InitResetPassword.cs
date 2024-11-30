using DPS.Service.Auth.Common;

namespace DPS.Service.Auth;

public partial class AuthService
{
    public async Task<AppResponse<bool>> InitResetUserPassword(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null || string.IsNullOrEmpty(user.Email))
            return AppResponse<bool>.GetErrorResponse("could_not_find_user", "User not found");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        token = AuthTokensHelper.EncodeAuthOperationToken(email, token);
        var resetPasswordUrl = urlFactory.GetPasswordResetUrl(token);
        await emailSender.SendResetPasswordEmailAsync(user.Email, resetPasswordUrl);

        return AppResponse<bool>.GetSuccessResponse(true);
    }
}