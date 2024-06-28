using RainfallApi.Controllers;
using RainfallApi.Services;
using NUnit.Framework;
using RainfallApi.Models;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Responses;

namespace RainfallApiTests
{
    public class RainfallControllerTest
    {
        // Proceeded without Moq due to installation issues
        public class MockRainfallService : IRainfallService
        {
            public async Task<RainfallReadingResponse?> GetRainfallReadings(
                string stationId,
                int count = 10)
            {
                var readings = new List<RainfallReading>
                {
                    new RainfallReading {
                        DateMeasured = DateTime.UtcNow,
                        AmountMeasured = (decimal)10.6 },
                    new RainfallReading {
                        DateMeasured = DateTime.UtcNow.AddDays(-1),
                        AmountMeasured = (decimal)5.3 }
                };

                return await Task.FromResult(
                    new RainfallReadingResponse { Readings = readings });
            }
        }

        private RainfallController _controller;

        [SetUp]
        public void Setup()
        {
            var mockService = new MockRainfallService();
            _controller = new RainfallController(mockService);
        }

        // Could expand test coverage by covering all error types
        [Test]
        public async Task GetRainfallReadings_ReturnsSuccess()
        {
            var stationId = "123";
            var result = await _controller.GetRainfallReadings(stationId);

            Assert.That(result, Is.Not.Null);

            var objectResult = result.Result as OkObjectResult;
            Assert.That(objectResult, Is.Not.Null);

            var model = objectResult.Value as RainfallReadingResponse;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Readings, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetRainfallReadings_ReturnsBadRequest()
        {
            string stationId = "3741";
            int count = -1;

            var expectedErrorResponse = new ErrorResponse
            {
                Error = new Error
                {
                    Message = "Invalid request",
                    Detail = new ErrorDetail
                    {
                        PropertyName = "Count",
                        Message = "Count must be greater than zero"
                    }
                }
            };

            var result = await _controller.GetRainfallReadings(stationId, count);

            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));

            var errorResponse = badRequestResult.Value as ErrorResponse;
            Assert.That(errorResponse, Is.Not.Null);
            Assert.That(errorResponse.Error.Message, Is.EqualTo(expectedErrorResponse.Error.Message));
        }
    }
}