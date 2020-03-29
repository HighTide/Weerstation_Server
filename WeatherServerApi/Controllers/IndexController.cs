using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherServerApi.Models;

namespace WeatherServerApi.Controllers
{
    public class IndexController : ControllerBase
    {
        // GET: api/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetIndex()
        {
            return Ok();
        }
    }
}
