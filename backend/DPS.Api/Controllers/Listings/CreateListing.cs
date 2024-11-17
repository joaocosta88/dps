using System.Security.Claims;
using DPS.Service.Listings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Listings;

public partial class ListingsController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateListing(AddListingRequest request) 
    {
        var res = await listingService.AddListingAsync(request, User.FindFirstValue("Id"));

        return Ok(res);
    }
}