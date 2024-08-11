namespace DPS.Data.Entities;
public abstract class BaseEntity {
	public Guid Id { get; set; }

	public DateTimeOffset CreatedAt { get; set; }

	public string? CreatedBy { get; set; }

	public DateTimeOffset LastModifiedAt { get; set; }

	public string? LastModifiedBy { get; set; }

}
