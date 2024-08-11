using Microsoft.AspNetCore.Identity;

namespace DPS.Infrastructure.Identity {
	public class ApplicationUser : IdentityUser {
		public override string? Email { get; set; }
		public override string? UserName { get; set; }
	}
}
