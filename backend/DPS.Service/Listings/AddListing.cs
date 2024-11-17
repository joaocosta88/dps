using DPS.Common.Exceptions;
using DPS.Data.Entities;
using DPS.Service.Listings.Common;

namespace DPS.Service.Listings;

public class AddListingRequest {
	public required string Title {  get; init; }
	public required string Description { get; init; }
	public required decimal Price { get; init; }
}

public class AddListingResponse(Listing listing) 
	: ListingResponse(listing)
{
}

public partial class ListingService {

	public async Task<AppResponse<AddListingResponse>> AddListingAsync(AddListingRequest request, string? userId)
	{
		if (string.IsNullOrWhiteSpace(userId))
			throw new InvalidParameterException($"Invalid user Id {userId}");
		
		var owner = await _userManager.FindByIdAsync(userId)
			?? throw new InvalidParameterException($"Could not find user with id {userId}");

		var listing = new Listing
		{
			Title = request.Title,
			Description = request.Description,
			Price = request.Price,
			Author = owner
		};

		await _context.Listings.AddAsync(listing);
		await _context.SaveChangesAsync();

		var res = new AddListingResponse(listing);

		return AppResponse<AddListingResponse>.GetSuccessResponse(res);
	}
}
