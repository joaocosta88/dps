using DPS.Data;
using DPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using DPS.Service.Common;

namespace DPS.Service.User;

public partial class UserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    //RoleManager<IdentityRole> roleManager,
    ApplicationDbContext applicationDbContext,
    TokenSettings tokenSettings)
{
    
}