namespace RainfallApi.Models
{
    public class ExternalApiDetails
    {
        public string Publisher { get; set; } = string.Empty;

        public string Licence { get; set; } = string.Empty;

        public string Documentation { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public List<string> HasFormat { get; set; } = new();

        public int Limit { get; set; }
    }
}
