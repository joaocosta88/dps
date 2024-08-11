using DPS.Data;
using DPS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPS.Service.Listings;
public partial class ListingService(UserManager<ApplicationUser> userManager,
	ApplicationDbContext context) {

	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly ApplicationDbContext _context = context;

}
