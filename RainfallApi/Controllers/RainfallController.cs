using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models;
using RainfallApi.Responses;
using RainfallApi.Services;

namespace RainfallApi.Controllers
{
    [ApiController]
    [Route("api/rainfall")]
    public class RainfallController : ControllerBase
    {   
        private readonly IRainfallService _rainfallService;

        public RainfallController(
            IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet("stations/{stationId}/readings")]
        public async Task<ActionResult<RainfallReadingResponse>> GetRainfallReadings(
            string stationId,
            int count = 10)
        {
            if (count <= 0)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = "Invalid request",
                        Detail = new ErrorDetail
                        {
                            PropertyName = "count",
                            Message = "Count must be greater than zero"
                        }
                    }
                };
                return BadRequest(errorResponse);
            }
            try
            {
                var readings = await _rainfallService.GetRainfallReadings(stationId, count);

                if (readings == null || readings.Readings.Count == 0)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = new Error
                        {
                            Message = "No readings found",
                            Detail = new ErrorDetail
                            {
                                PropertyName = "stationId",
                                Message = $"No readings found for station {stationId}"
                            }
                        }
                    };
                    return NotFound(errorResponse);
                }

                return Ok(readings);
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = $"Error retrieving rainfall readings: {ex.Message}"
                    }
                };
                return StatusCode(500, errorResponse);
            }
            catch (ArgumentNullException ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = $"Invalid input: {ex.Message}"
                    }
                };
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = $"An unexpected error occurred: {ex.Message}"
                    }
                };
                return StatusCode(500, errorResponse);
            }
        }
    }
}