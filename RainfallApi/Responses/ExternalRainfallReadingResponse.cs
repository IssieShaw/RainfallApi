using RainfallApi.Models;

namespace RainfallApi.Responses
{
    public class ExternalRainfallReadingResponse
    {
        public List<ExternalRainfallReading> Items { get; set; } = new();

        public ExternalApiDetails ExternalApiDetails { get; set; } = new();
    }
}
