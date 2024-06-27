using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RainfallApi.Models;
using RainfallApi.Services;

namespace RainfallApi.Controllers
{
    [ApiController]
    [Route("api/rainfall")]
    public class RainfallController : ControllerBase
    {   
        private readonly IRainfallService _rainfallService;

        public RainfallController(IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet("stations/{stationId}/readings")]
        public async Task<ActionResult<RainfallReadingResponse>> GetRainfallReadings(
            string stationId,
            int count)
        {
            try
            {
                var readings = new RainfallReadingResponse();
                return Ok(readings);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error retrieving rainfall readings: {ex.Message}");
            }

            // Should also eventually return: 400, 404 alongside 200 and 500
        }
    }
}