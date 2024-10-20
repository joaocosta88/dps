using DPS.Common.Exceptions;
using DPS.Data.Entities;

namespace DPS.Service.Listings;

public class AddListingRequest {
	public required string Name {  get; init; }
	public required string Description { get; init; }
	public required decimal Price { get; init; }
}

public class AddListingResponse {
	public Guid Id { get; init; }
	public required string Name { get; init; }
	public required string Description { get; init; }
	public required decimal Price { get; init; }
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
			Name = request.Name,
			Description = request.Description,
			Price = request.Price,
			Owner = owner
		};

		await _context.Listings.AddAsync(listing);
		await _context.SaveChangesAsync();

		var res = new AddListingResponse
		{
			Id = listing.Id,
			Name = listing.Name,
			Description = listing.Description,
			Price = listing.Price,
		};

		return AppResponse<AddListingResponse>.GetSuccessResponse(res);
	}
}
