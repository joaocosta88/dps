using DPS.Common.Exceptions;
using DPS.Data.Entities;

namespace DPS.Service.Listings;

public class AddListingRequest {
	public string Name {  get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
}

public class AddListingResponse {
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public string UserId { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public string CreatedBy { get; set; }
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
		_context.SaveChanges();

		var res = new AddListingResponse
		{
			Id = listing.Id,
			Name = listing.Name,
			Description = listing.Description,
			Price = listing.Price,
			UserId = listing.Owner.Id,
			CreatedAt = listing.CreatedAt,
			CreatedBy = listing.CreatedBy
		};

		return new AppResponse<AddListingResponse>().SetSuccessResponse(res);
	}
}
