﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Service.Listings;

public class SearchListingsResponse
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required string UserId { get; set; }
	public required IList<string> ImageUrls { get; set; }
}

public partial class  ListingService {

	public async Task<AppResponse<List<SearchListingsResponse>>> SearchListingsAsync(string? userId, string? keyword, bool includeDeleteds = false)
	{
		var query = _context.Listings.AsQueryable();
		if (userId != null)
			query = query.Where(m => m.Owner.Id == userId);
		
		if (!string.IsNullOrWhiteSpace(keyword))
			query = query.Where(m => m.Name.Contains(keyword) ||
			m.Description.Contains(keyword));

		if (!includeDeleteds)
			query = query.Where(m => !m.IsDeleted);

		var queryResult = await query.Include(m =>m.Owner).ToListAsync();

		var res = queryResult.Select(m => new SearchListingsResponse
		{
			Id = m.Id,
			Name = m.Name,
			Description = m.Description,
			UserId = m.Owner.Id,
			ImageUrls = m.ImageUrls
		}).ToList();

		return AppResponse<List<SearchListingsResponse>>.GetSuccessResponse(res);
	}
}
