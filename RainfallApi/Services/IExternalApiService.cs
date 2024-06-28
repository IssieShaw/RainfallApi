using RainfallApi.Models;

namespace RainfallApi.Services
{
    public interface IExternalApiService
    {
        Task<ExternalRainfallReadingResponse?> GetExternalRainfallReadings(string stationId, int count);
    }
}