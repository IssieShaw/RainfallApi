using RainfallApi.Models;

namespace RainfallApi.Responses
{
    public class ErrorResponse
    {
        public Error Error { get; set; } = new();
    }
}
