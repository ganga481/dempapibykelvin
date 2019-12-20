using firstcoreappbykelvin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstcoreappbykelvin.Controllers
{

    [Route("api/cities")]
    public class CityController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var CityToreturn = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == id);
            if (CityToreturn == null)
            {
                return NotFound();
            }
            return Ok(CityToreturn);
        }
    }
}
