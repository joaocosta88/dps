using DPS.Data.Entities;
using DPS.Service.Listings.Common;
using Microsoft.EntityFrameworkCore;

namespace DPS.Service.Listings;

public class SearchListingsResponse(Listing listing) 
    : ListingResponse(listing)
{
}

public partial class ListingService
{
    public async Task<AppResponse<IEnumerable<SearchListingsResponse>>> SearchListingsAsync(string? userId,
        string? keyword, int pageNumber, int pageSize, bool includeDeleteds = false)
    {
        var query = _context.Listings.AsQueryable();
        
        if (userId != null)
            query = query.Where(m => m.Author.Id == userId);

        if (!string.IsNullOrWhiteSpace(keyword))
            query = query.Where(m => m.Title.Contains(keyword) ||
                                     m.Description.Contains(keyword));

        if (!includeDeleteds)
            query = query.Where(m => !m.IsDeleted);

        query = query.Include(m => m.Author);

        var queryCount = await query.CountAsync();
        var queryResult = await query.ToListAsync();


        var res = queryResult.Select(m => new SearchListingsResponse(m));

        return AppResponse<IEnumerable<SearchListingsResponse>>.GetSuccessResponse(res);
    }
}