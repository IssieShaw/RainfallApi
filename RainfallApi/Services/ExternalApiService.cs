using RainfallApi.Responses;
using System.Text.Json;

namespace RainfallApi.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ExternalRainfallReadingResponse?> GetExternalRainfallReadings(
            string stationId,
            int count)
        {
            var response = await _httpClient.GetAsync(
                $"https://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit={count}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ExternalRainfallReadingResponse>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
