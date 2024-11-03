using DPS.Data.Entities;

namespace DPS.Service.User;

public partial class UserService
{
    public List<ApplicationUser> GetAll()
    {
        return userManager.Users.ToList();
    }
}