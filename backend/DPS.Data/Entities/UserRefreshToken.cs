using DPS.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DPS.Data.Entities;

[Index(nameof(RefreshToken), IsUnique = true)]
public class UserRefreshToken : BaseEntity
{
    public string RefreshToken { get; set; }
    public string? PreviousRefreshToken { get; set; }
    public bool IsValid { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsSessionToken { get; set; }
}