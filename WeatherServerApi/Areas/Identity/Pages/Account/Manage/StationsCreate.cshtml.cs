using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using WeatherServerApi.Data;

namespace WeatherServerApi
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WeatherServerApi.Data.ApplicationDbContext _context;

        public CreateModel(
            WeatherServerApi.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Station Station { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Station.Api = Guid.NewGuid();
            Station.Owner = await _userManager.GetUserAsync(User);

            _context.Stations.Add(Station);
            await _context.SaveChangesAsync();

            return RedirectToPage("./StationsIndex");
        }
    }
}
