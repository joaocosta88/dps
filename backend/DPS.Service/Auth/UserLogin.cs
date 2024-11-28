using DPS.Data.Entities;
using DPS.Service.Common;

namespace DPS.Service.Auth;

public class UserLoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required bool KeepLoggedIn { get; init; }
}

public class UserLoginResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required int ExpiresIn { get; init; }
}

public partial class AuthService
{
    public async Task<AppResponse<UserLoginResponse>> UserLoginAsync(UserLoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return AppResponse<UserLoginResponse>.GetErrorResponse("email_not_found", "Email not found");

        if (!user.EmailConfirmed)
            return AppResponse<UserLoginResponse>.GetErrorResponse("email_not_confirmed", "Email not confirmed");
        
        var isValidPassword = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (!isValidPassword.Succeeded)
            return AppResponse<UserLoginResponse>.GetErrorResponse("invalid_password", isValidPassword.ToString());

        //var result = await GenerateUserTokensAsync(user, request.KeepLoggedIn);
        var claims = GetUserClaims(user);
        var accessToken = TokenUtils.GetAccessToken(tokenSettings, user, claims);
        var newRefreshToken = TokenUtils.GetRefreshToken();

        //store new token
        var refreshToken = new UserRefreshToken
        {
            RefreshToken = newRefreshToken,
            IsValid = true,
            ExpirationDate = DateTime.Now.AddSeconds(tokenSettings.RefreshTokenExpireSeconds),
            User = user,
            IsSessionToken = request.KeepLoggedIn
        };
        applicationDbContext.UserRefreshTokens.Add(refreshToken);
        await applicationDbContext.SaveChangesAsync();

        return AppResponse<UserLoginResponse>.GetSuccessResponse(new UserLoginResponse()
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = tokenSettings.TokenExpireSeconds
        });
    }
}