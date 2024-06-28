using RainfallApi.Models;

namespace RainfallApi.Responses
{
    public class RainfallReadingResponse
    {
        public List<RainfallReading> Readings { get; set; } = new();
    }
}
