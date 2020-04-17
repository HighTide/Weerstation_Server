using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WeatherServerApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace WeatherServerApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public class localStationModel {
            public long Id { get; set; }
            public String Name { get; set; }
            public String Owner { get; set; }
            public String Color { get; set; }
            public double Coordinate_X { get; set; }
            public double Coordinate_Y { get; set; }
        }


        public StationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Stations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<localStationModel>>> GetStations()
        {
            return await _context.Stations.Select(x => new localStationModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Owner.Id,
                Color = x.Color,
                Coordinate_X = x.Coordinate_X,
                Coordinate_Y = x.Coordinate_Y
            }).ToListAsync();
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Station>>> GetStation(string Owner)
        {
            var stations = await _context.Stations.Where(station => station.Owner.Id == Owner).ToListAsync();

            if (stations == null)
            {
                return NotFound();
            }

            return stations;
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> PutStation(long id, Station station)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var stationDb = await _context.Stations.FindAsync(id);

        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }

        //    if (id != station.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (stationDb.Owner.Id != user.Id || user.Id != "0" )
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(station).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // No use for post
        //// POST: api/Stations
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult<Station>> PostStation(Station station)
        //{

        //    _context.Stations.Add(station);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStation", new { id = station.Id }, station);
        //}

        // DELETE: api/Stations/5
        //[HttpDelete("{id}")]
        //[Authorize]
        //public async Task<ActionResult<Station>> DeleteStation(long id)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var station = await _context.Stations.FindAsync(id);

        //    if (station == null)
        //    {
        //        return NotFound();
        //    }

        //    if (station.Owner.Id != user.Id || user.Id != "0")
        //    {
        //        return BadRequest();
        //    }

        //    _context.Stations.Remove(station);
        //    await _context.SaveChangesAsync();

        //    return station;
        //}

        private bool StationExists(long id)
        {
            return _context.Stations.Any(e => e.Id == id);
        }
    }
}
