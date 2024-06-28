namespace RainfallApi.Models
{
    public class Error
    {
        public string Message { get; set; } = string.Empty;

        public ErrorDetail Detail { get; set; } = new();
    }
}
