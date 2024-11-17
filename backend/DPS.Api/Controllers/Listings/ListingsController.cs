using DPS.Service.Listings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DPS.Api.Controllers.Listings;

[ApiController]
[Route("[controller]/[action]")]
public partial class ListingsController(ListingService listingService) : ControllerBase
{
    [HttpPut]
    [Authorize]
    public IActionResult UpdateListing(UpdateListingRequest request)
    {
        var res = listingService.UpdateListing(request, User.FindFirstValue("Id"));
        return Ok(res);
    }

    [HttpDelete]
    public IActionResult DeleteListing(Guid listingId)
    {
        var res = listingService.DeleteListing(listingId, User.FindFirstValue("Id"));
        return Ok(res);
    }
}