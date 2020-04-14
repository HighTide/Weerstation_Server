using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WeatherServerApi.Data;

namespace WeatherServerApi
{
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WeatherServerApi.Data.ApplicationDbContext _context;

        public EditModel(
            WeatherServerApi.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            Station = await _context.Stations.Where(Station => Station.Owner.Id == user.Id).FirstOrDefaultAsync(m => m.Id == id);

            if (Station == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var owner = await _context.Stations.Where(Station => Station.Owner.Id == user.Id).FirstOrDefaultAsync(m => m.Id == Station.Id);
            if (!ModelState.IsValid || user != owner.Owner)
            {
                return Page();
            }

            _context.Attach(Station).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(Station.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StationExists(long id)
        {
            return _context.Stations.Any(e => e.Id == id);
        }
    }
}
