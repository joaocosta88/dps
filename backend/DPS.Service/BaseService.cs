using DPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Service;
public abstract class BaseService(UserManager<ApplicationUser> userManager, IMemoryCache memoryCache) {
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly IMemoryCache _memoryCache = memoryCache;


	public async Task<ApplicationUser?> GetUserById(string userId)
	{
		return await _memoryCache.GetOrCreateAsync(userId, async entry =>
		{
			return await _userManager.FindByIdAsync(userId);

		});
	}
}
