using DPS.Data.Entities;

namespace DPS.Service.User.Common;

public abstract class UserResponse (ApplicationUser user)
{
    public string UserId { get; init; } = user.Id;
    public string Username { get; init; } = user.Email;
}