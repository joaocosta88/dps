using System.Security.Claims;
using DPS.Data;
using DPS.Data.Entities;
using DPS.Email;
using DPS.Service.Common;
using Microsoft.AspNetCore.Identity;

namespace DPS.Service.Auth;

public partial class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    //RoleManager<IdentityRole> roleManager,
    ApplicationDbContext applicationDbContext,
    EmailSender emailSender,
    UrlFactory urlFactory,
    TokenSettings tokenSettings)
{
    public IList<Claim> GetUserClaims(ApplicationUser user)
    {
        var claims = (from ur in applicationDbContext.UserRoles
                where ur.UserId == user.Id
                join r in applicationDbContext.Roles on ur.RoleId equals r.Id
                join rc in applicationDbContext.RoleClaims on r.Id equals rc.RoleId
                select rc)
            .Where(rc => !string.IsNullOrEmpty(rc.ClaimValue) && !string.IsNullOrEmpty(rc.ClaimType))
            .Select(rc => new Claim(rc.ClaimType!, rc.ClaimValue!))
            .Distinct()
            .ToList();

        var roleClaims = (from ur in applicationDbContext.UserRoles
                where ur.UserId == user.Id
                join r in applicationDbContext.Roles on ur.RoleId equals r.Id
                select r)
            .Where(r => !string.IsNullOrEmpty(r.Name))
            .Select(r => new Claim(ClaimTypes.Role, r.Name!))
            .Distinct()
            .ToList();

        claims.AddRange(roleClaims);
        return claims;
    }
    
    private Dictionary<string, string[]> GetIdentityErrors(IdentityResult result)
    {
        var errorDictionary = new Dictionary<string, string[]>(1);
        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
                newDescriptions = [error.Description];
            
            errorDictionary[error.Code] = newDescriptions;
        }

        return errorDictionary;
    }
}