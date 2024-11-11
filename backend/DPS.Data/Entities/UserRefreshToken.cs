using DPS.Data.Entities.Common;

namespace DPS.Data.Entities;

public class UserRefreshToken : BaseEntity
{
    public string RefreshToken { get; set; }
    public bool IsValid { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsSessionToken { get; set; }
}