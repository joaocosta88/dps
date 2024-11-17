using Microsoft.AspNetCore.Mvc;

namespace DPS.Api.Controllers.Listings;

public partial class ListingsController {
    
    [HttpGet]
    public async Task<IActionResult> GetListings(
        string? userId = null, string? keyword = null, int pageNumber = 0, int pageSize = 10)
    {
        var res = await listingService.SearchListingsAsync(userId, keyword, pageNumber, pageSize);
        return Ok(res);
    }
}