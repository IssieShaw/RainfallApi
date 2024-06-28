using RainfallApi.Models;
using RainfallApi.Responses;
using RainfallApi.Services;

namespace RainfallApiTests.Services
{
    public class FakeExternalApiService : IExternalApiService
    {
        private readonly List<ExternalRainfallReading> _readings;

        public FakeExternalApiService(List<ExternalRainfallReading> readings)
        {
            _readings = readings;
        }

        public Task<ExternalRainfallReadingResponse?> GetExternalRainfallReadings(
            string stationId, int count)
        {
            ExternalRainfallReadingResponse? response = new()
            {
                Items = _readings.Take(count).ToList()
            };

            return Task.FromResult(response);
        }
    }

}
