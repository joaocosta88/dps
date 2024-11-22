using DPS.Data.Entities;
using DPS.Service.User.Common;

namespace DPS.Service.User;

public class GetUserByUsernameResponse(ApplicationUser user)
    : UserResponse(user)
{
}

public partial class UserService
{
    public GetUserByUsernameResponse? GetUserByUsername(string username)
    {
        var user = userManager.Users.FirstOrDefault(m => m.UserName == username);

        if (user == null)
            return null;
        
        return new GetUserByUsernameResponse(user);
    }
    
    public GetUserByUsernameResponse? GetUserById(string id)
    {
        var user = userManager.Users.FirstOrDefault(m => m.Id == id);

        if (user == null)
            return null;
        
        return new GetUserByUsernameResponse(user);
    }
    
    public GetUserByUsernameResponse? GetUserByEmail(string email)
    {
        var user = userManager.Users.FirstOrDefault(m => m.Email == email);

        if (user == null)
            return null;
        
        return new GetUserByUsernameResponse(user);
    }
}