using DPS.Data.Entities;

namespace DPS.Service.Listings.Common;

public abstract class ListingResponse(Listing listing)
{
    public Guid Id { get; init; } = listing.Id;
    public string Name { get; init; } = listing.Title;
    public string Description { get; init; } = listing.Description;
    public ListingAuthorResponse Author { get; init; } = new ListingAuthorResponse(listing.Author);
    public decimal Price { get; init; } = listing.Price;
    public IList<string> ImageUrls { get; init; } = listing.ImageUrls;
}

public class ListingAuthorResponse(ApplicationUser author)
{
    public string Id { get; init; } = author.Id;
    public string Username { get; init; } = author.UserName ?? string.Empty;
}