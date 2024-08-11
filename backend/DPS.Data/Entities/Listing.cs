namespace DPS.Data.Entities;
public class Listing : BaseEntity {
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required decimal Price { get; set; }

	public IList<string> ImageUrls { get; set; } = [];
	public required ApplicationUser Owner { get; set; }
}
