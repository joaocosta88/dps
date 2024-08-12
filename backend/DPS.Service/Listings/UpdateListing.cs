using DPS.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Service.Listings;

public class UpdateListingRequest {
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
}

public class UpdateListingResponse {
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
}
public partial class ListingService {

	public AppResponse<UpdateListingResponse> UpdateListing(UpdateListingRequest request, string? currentUserId)
	{
		var listing = _context.Listings.FirstOrDefault(m => m.Id == request.Id);
		if (listing == null)
			throw new InvalidParameterException($"Could not find listing with id {request.Id}");
		if (string.IsNullOrWhiteSpace(currentUserId))
			throw new InvalidParameterException($"Empty current user Id");

		if (listing.Owner.Id != currentUserId)
			throw new ForbiddenOperationException($"Listing owner {listing.Owner.Id} does not match current user id {currentUserId}");

		listing.Name = request.Name;
		listing.Description = request.Description;
		listing.Price = request.Price;

		_context.Listings.Update(listing);

		var res = new UpdateListingResponse
		{
			Id = listing.Id,
			Name = listing.Name,
			Description = listing.Description,
			Price = listing.Price,
		};

		return new AppResponse<UpdateListingResponse>().SetSuccessResponse(res);
	}
}
