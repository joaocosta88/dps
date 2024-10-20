using DPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace DPS.Data;

public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
	private readonly ILogger<ApplicationDbContextInitialiser> _logger = logger;
	private readonly ApplicationDbContext _context = context;
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly RoleManager<IdentityRole> _roleManager = roleManager;

	public async Task InitialiseAsync()
	{
		try
		{
			await _context.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while initialising the database.");
			throw;
		}
	}
	public async Task SeedAsync()
	{
		try
		{
			await TrySeedAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while seeding the database.");
			throw;
		}
	}
	private async Task TrySeedAsync()
	{
		// Default roles
		var administratorRole = new IdentityRole("Administrator");

		if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
		{
			var role = await _roleManager.CreateAsync(administratorRole);
			if (role != null)
			{
				await _roleManager.AddClaimAsync(administratorRole, new Claim("RoleClaim", "HasRoleView"));
				await _roleManager.AddClaimAsync(administratorRole, new Claim("RoleClaim", "HasRoleAdd"));
				await _roleManager.AddClaimAsync(administratorRole, new Claim("RoleClaim", "HasRoleEdit"));
				await _roleManager.AddClaimAsync(administratorRole, new Claim("RoleClaim", "HasRoleDelete"));
			}
		}

		// Default users
		var adminUser = new ApplicationUser { Email = "admin2@gmail.com", UserName = "admin2@gmail.com"};
		if (_userManager.Users.All(u => u.Email != adminUser.Email))
		{
			var result = await _userManager.CreateAsync(adminUser, "Aa11122233334444!");
			if (!string.IsNullOrWhiteSpace(administratorRole.Name))
			{
				var roleResult = await _userManager.AddToRolesAsync(adminUser, new[] { administratorRole.Name });
				Console.WriteLine(roleResult);
			}
		}
	}
}
