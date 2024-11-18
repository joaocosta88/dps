using DPS.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DPS.Service.Listings;

public partial class ListingService {

	public AppResponse<bool> DeleteListing (Guid listingId, string userId)
	{
		if (listingId ==  Guid.Empty) 
			throw new InvalidParameterException("Missing listingId parameter");

		if (string.IsNullOrWhiteSpace(userId))
			throw new InvalidParameterException("Missing userId paramter");

		var listing = _context.Listings.Include(listing => listing.Author)
			.FirstOrDefault(m => m.Id == listingId);
		
		if (listing == null)
			throw new InvalidParameterException($"Could not find listing for listing id {listingId}");

		if (listing.Author.Id != userId) 
			throw new ForbiddenOperationException($"UserId {userId} does not match listing owner id {listing.Author.Id}");

		listing.IsActive = false;
		_context.Listings.Update(listing);
		_context.SaveChanges();
		return AppResponse<bool>.GetSuccessResponse(true);
	}
}
