using DPS.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DPS.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
	public DbSet<Listing> Listings => Set<Listing>();
	public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
	
}