using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Listings;

public partial class ListingsController
{
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteListing(Guid id)
    {
        var res = listingService.DeleteListing(id, User.FindFirstValue("Id"));
        return Ok(res);
    }
}