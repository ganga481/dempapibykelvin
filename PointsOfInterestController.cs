using firstcoreappbykelvin.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstcoreappbykelvin.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId )
        {
            try
            {
                throw new Exception("excption example");

                var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {

                    _logger.LogInformation($"the given {cityId} has not found"); // string interpolation 

                    return NotFound();
                }
                return Ok(city.Pointsofinterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"exception occured while getting required pointsofIntrest with {cityId}", ex);

                return StatusCode(500, "exception occured while getting data..");
            }

        }

        [HttpGet("{cityId}/pointsofinterest/{id}" , Name= "GetPointsOfInterest")]
        public IActionResult GetPointsOfInterest( int cityId , int id)
        {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.Pointsofinterest.FirstOrDefault(c => c.Id == id);
            if(pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointsOfInterestDto(int cityid , [FromBody] PointsOfInterestCreationDto pointofinterest)
        {
            if(pointofinterest == null)
            {
                return BadRequest(); // 400
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityid);
            if(city == null)
            {
                return NotFound();
            }
            var maxPointofInterest = CitiesDataStore.current.Cities.SelectMany(c => c.Pointsofinterest).Max(p => p.Id);
            var finalpointsofinterest = new PointsOfInterestDto()
            {
                Id = ++maxPointofInterest,
                Name = pointofinterest.Name,
                Description = pointofinterest.Description
            };

            city.Pointsofinterest.Add(finalpointsofinterest);
           return CreatedAtRoute("GetPointsOfInterest", new { cityId = cityid, id = finalpointsofinterest.Id }, finalpointsofinterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointofInterest( int cityId  , int id , [FromBody] PointOfInterestUpdateDto pointofInterestUpdate )
        {
            if(pointofInterestUpdate == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfinterestToStore = city.Pointsofinterest.FirstOrDefault(p => p.Id == id);
            if(pointOfinterestToStore == null)
            {
                return NotFound();
            }
            pointOfinterestToStore.Name = pointofInterestUpdate.Name;
            pointOfinterestToStore.Description = pointofInterestUpdate.Description;
            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartialUpdatePointsofInterst(int cityId,  int id, [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchdoc)
        {
            if(patchdoc == null)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var pointofInterstStore = city.Pointsofinterest.FirstOrDefault(c => c.Id == id);
            if(pointofInterstStore == null)
            {
                return NotFound();
            }
            var pointofinterestPatch = new PointOfInterestUpdateDto()
            {
                Name = pointofInterstStore.Name,
                Description = pointofInterstStore.Description
            };
            patchdoc.ApplyTo(pointofinterestPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(pointofinterestPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointofInterstStore.Name = pointofinterestPatch.Name;
            pointofInterstStore.Description = pointofinterestPatch.Description;

            return NoContent();
        }
        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId , int id)
        {
            var city = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            var pointofInterst = city.Pointsofinterest.FirstOrDefault(c => c.Id == id);
            if(pointofInterst == null)
            {
                return NotFound();
            }

            city.Pointsofinterest.Remove(pointofInterst);
            return NoContent();
        }

    }
    
}
