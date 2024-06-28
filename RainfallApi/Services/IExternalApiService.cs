using RainfallApi.Responses;

namespace RainfallApi.Services
{
    public interface IExternalApiService
    {
        Task<ExternalRainfallReadingResponse?> GetExternalRainfallReadings(string stationId, int count);
    }
}