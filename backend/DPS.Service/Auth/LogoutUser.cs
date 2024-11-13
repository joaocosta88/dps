using Microsoft.EntityFrameworkCore;

namespace DPS.Service.Auth;

public class LogoutRequest {
    public required string CurrentRefreshToken { get; init; }
}

public partial class AuthService
{
    public async Task<AppResponse<bool>> LogoutUserAsync(LogoutRequest request)
    {
        var refreshToken = applicationDbContext.UserRefreshTokens.Include(userRefreshToken => userRefreshToken.User)
            .FirstOrDefault(m => m.RefreshToken == request.CurrentRefreshToken);

        if (refreshToken == null) 
            return AppResponse<bool>.GetErrorResponse("token_not_valid");
        
        refreshToken.IsValid = false;
        await applicationDbContext.SaveChangesAsync();

        return AppResponse<bool>.GetSuccessResponse(true);
    }
}