using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeatherServerApi.Data;

namespace WeatherServerApi
{
    public class DeleteModel : PageModel
    {
        private readonly WeatherServerApi.Data.ApplicationDbContext _context;

        public DeleteModel(WeatherServerApi.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Station = await _context.Stations.FirstOrDefaultAsync(m => m.Id == id);

            if (Station == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Station = await _context.Stations.FindAsync(id);

            if (Station != null)
            {
                List<Measurement> mesurements = _context.Measurements.Where(x => x.Station == Station).ToList();

                foreach (Measurement mesurement in mesurements)
                {
                    _context.Measurements.Remove(mesurement);
                }

                _context.Stations.Remove(Station);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./StationsIndex");
        }
    }
}
