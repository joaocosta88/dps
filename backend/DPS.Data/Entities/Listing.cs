using DPS.Data.Entities.Common;

namespace DPS.Data.Entities;
public class Listing : BaseAuditableEntity {
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required decimal Price { get; set; }

	public IList<string> ImageUrls { get; set; } = [];
	public bool IsDeleted { get; set; } = false;
	public required ApplicationUser Owner { get; set; }
}
