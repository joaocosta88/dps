using DPS.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Service.Listings;

public partial class ListingService {

	public AppResponse<bool> DeleteListing (Guid listingId, string userId)
	{
		if (listingId ==  Guid.Empty) 
			throw new InvalidParameterException("Missing listingId parameter");

		if (string.IsNullOrWhiteSpace(userId))
			throw new InvalidParameterException("Missing userId paramter");

		var listing = _context.Listings.FirstOrDefault(m => m.Id == listingId);
		if (listing == null)
			throw new InvalidParameterException($"Could not find listing for listing id {listingId}");

		if (listing.Owner.Id != userId) 
			throw new ForbiddenOperationException($"UserId {userId} does not match listing owner id {listing.Owner.Id}");

		listing.IsDeleted = true;
		_context.Listings.Update(listing);

		return AppResponse<bool>.GetSuccessResponse(true);
	}
}
