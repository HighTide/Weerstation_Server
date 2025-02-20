﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeatherServerApi.Data;

namespace WeatherServerApi
{
    public class DetailsModel : PageModel
    {
        private readonly WeatherServerApi.Data.ApplicationDbContext _context;

        public DetailsModel(WeatherServerApi.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
