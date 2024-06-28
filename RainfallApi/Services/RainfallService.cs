using RainfallApi.Models;
using RainfallApi.Responses;

namespace RainfallApi.Services
{
    public class RainfallService : IRainfallService
    {
        private readonly IExternalApiService _externalApiService;

        public RainfallService(
            IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService 
                ?? throw new ArgumentNullException(nameof(externalApiService));
        }

        public async Task<RainfallReadingResponse?> GetRainfallReadings(
            string stationId,
            int count)
        {
            var externalResponse = await _externalApiService.GetExternalRainfallReadings(
                stationId,
                count);

            if (externalResponse == null || !externalResponse.Items.Any())
            {
                throw new ArgumentNullException($"No readings found for station {stationId}");
            }

            return MapToInternalResponse(externalResponse);
        }

        private static RainfallReadingResponse MapToInternalResponse(
            ExternalRainfallReadingResponse externalResponse)
        {
            return new RainfallReadingResponse
            {
                Readings = externalResponse.Items.Select(
                    e => new RainfallReading
                    {
                        DateMeasured = e.DateTime,
                        AmountMeasured = (decimal)e.Value
                    }).ToList()
            };
        }
    }
}
