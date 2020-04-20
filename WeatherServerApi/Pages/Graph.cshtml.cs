using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherServerApi.Data;

namespace WeatherServerApi
{
    public class GraphModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GraphModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Station> stations { get; set; }

        public void OnGet()
        {
            stations = _context.Stations.ToList();
        }
    }
}