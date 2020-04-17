using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherServerApi.Data;

namespace WeatherServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeasurementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class localMeasurmentModel
        {
            public long Id { get; set; }
            public decimal Temperature { get; set; }
            public decimal Humidity { get; set; }
            public decimal WindSpeed { get; set; }
            public long Station { get; set; }
        }

        // GET: api/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<localMeasurmentModel>>> GetMeasurements()
        {
            //return await _context.Measurements.ToListAsync();

            return await _context.Measurements.Select(x => new localMeasurmentModel
            {
                Id = x.Id,
                Temperature = x.Temperature,
                Humidity = x.Humidity,
                WindSpeed = x.WindSpeed,
                Station = x.Station.Id,
            }).ToListAsync();
        }


        // GET: api/Measurements
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<localMeasurmentModel>>> GetLatestMeasurements()
        {
            //return await _context.Measurements.ToListAsync();
            List<Station> stations = await _context.Stations.ToListAsync();
            List<localMeasurmentModel> measurment = new List<localMeasurmentModel>();

            foreach (Station station in stations)
            {

                //I Know this is slow and wrong, but if you can get it to work the proper way, power to you! #PowerToYou
                localMeasurmentModel? m = await _context.Measurements.Where(m => m.Station.Id == station.Id).Where(m => m.Time >= DateTime.Now.AddMinutes(-30)).OrderByDescending(m => m.Id).Select(x => new localMeasurmentModel
                {
                    Id = x.Id,
                    Temperature = x.Temperature,
                    Humidity = x.Humidity,
                    WindSpeed = x.WindSpeed,
                    Station = x.Station.Id,
                }).FirstOrDefaultAsync();

                if (m != null)
                {
                    measurment.Add(m);
                }

                
            }

            return measurment;
        }

        // GET: api/Measurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Measurement>> GetMeasurement(long id)
        {
            var measurement = await _context.Measurements.FindAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            return measurement;
        }

        // PUT: api/Measurements/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement(long id, Measurement measurement)
        {
            if (id != measurement.Id)
            {
                return BadRequest();
            }

            _context.Entry(measurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Measurements/[api_key]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("{api}")]
        public async Task<ActionResult<Measurement>> PostMeasurement(Measurement measurement, Guid api)
        {
            Station station = await _context.Stations.FirstOrDefaultAsync(x => x.Api == api);
            measurement.Station = station;
            measurement.Time = DateTime.Now;
            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeasurement", new { id = measurement.Id }, measurement);
        }

        // DELETE: api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Measurement>> DeleteMeasurement(long id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();

            return measurement;
        }

        private bool MeasurementExists(long id)
        {
            return _context.Measurements.Any(e => e.Id == id);
        }
    }
}
