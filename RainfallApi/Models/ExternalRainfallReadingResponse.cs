namespace RainfallApi.Models
{
    public class ExternalRainfallReadingResponse
    {
        public List<ExternalRainfallReading> Items { get; set; } = new();

        public ExternalApiDetails ExternalApiDetails { get; set; } = new();
    }
}
