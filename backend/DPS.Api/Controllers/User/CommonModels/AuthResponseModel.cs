namespace DPS.Api.Controllers.User.CommonModels;

public record AuthResponseModel
{
    public required string AccessToken { get; init; }
    public  required int ExpiresIn { get; init; }

}