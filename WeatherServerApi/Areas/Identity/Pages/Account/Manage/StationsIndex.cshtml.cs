using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WeatherServerApi.Data;

namespace WeatherServerApi
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WeatherServerApi.Data.ApplicationDbContext _context;

        public IndexModel(
            WeatherServerApi.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Station> Station { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Station = await _context.Stations.Where(Station => Station.Owner.Id == user.Id).ToListAsync();
        }
    }
}
