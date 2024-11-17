using DPS.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DPS.Service.Listings;

public class UpdateListingRequest {
	public required Guid Id { get; init; }
	public required string Name { get; init; }
	public required string Description { get; init; }
	public required decimal Price { get; init; }
}

public class UpdateListingResponse {
	public required Guid Id { get; init; }
	public required string Name { get; init; }
	public required string Description { get; init; }
	public required decimal Price { get; init; }
}
public partial class ListingService {

	public AppResponse<UpdateListingResponse> UpdateListing(UpdateListingRequest request, string? currentUserId)
	{
		var listing = _context.Listings
			.Include(listing => listing.Author)
			.FirstOrDefault(m => m.Id == request.Id);
		
		if (listing == null)
			throw new InvalidParameterException($"Could not find listing with id {request.Id}");
		if (string.IsNullOrWhiteSpace(currentUserId))
			throw new InvalidParameterException($"Empty current user Id");

		if (listing.Author.Id != currentUserId)
			throw new ForbiddenOperationException($"Listing owner {listing.Author.Id} does not match current user id {currentUserId}");

		listing.Title = request.Name;
		listing.Description = request.Description;
		listing.Price = request.Price;

		_context.Listings.Update(listing);

		var res = new UpdateListingResponse
		{
			Id = listing.Id,
			Name = listing.Title,
			Description = listing.Description,
			Price = listing.Price,
		};

		return AppResponse<UpdateListingResponse>.GetSuccessResponse(res);
	}
}
