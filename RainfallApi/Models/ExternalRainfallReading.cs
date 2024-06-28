using System.Text.Json.Serialization;

namespace RainfallApi.Models
{
    public class ExternalRainfallReading
    {
        [JsonPropertyName("@id")]
        public string Id { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        public string Measure { get; set; } = string.Empty;

        public double Value { get; set; }
    }
}
