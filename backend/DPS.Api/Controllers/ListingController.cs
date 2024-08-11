using DPS.Service.Listings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DPS.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ListingController(ListingService listingService) : ControllerBase {

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateListing(AddListingRequest request) 
	{
		var res = await listingService.AddListingAsync(request, User.FindFirstValue("Id"));

		return Ok(res);
	}

	[HttpGet]
	public async Task<IActionResult> SearchListingsByUserId(string? userId = null, string? keyword = null)
	{
		var res = await listingService.SearchListingsAsync(userId, keyword);

		return Ok(res);
	}
}
