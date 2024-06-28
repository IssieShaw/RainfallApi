using RainfallApi.Models;

namespace RainfallApi.Services
{
    public interface IRainfallService
    {
        public Task<RainfallReadingResponse?> GetRainfallReadings(string stationId, int count);
    }
}