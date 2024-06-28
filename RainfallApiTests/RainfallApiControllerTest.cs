﻿using RainfallApi.Controllers;
using RainfallApi.Services;
using NUnit.Framework;
using RainfallApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace RainfallApiTest
{
    public class RainfallControllerTest
    {
        public class MockRainfallService : IRainfallService
        {
            public async Task<RainfallReadingResponse> GetRainfallReadings(
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

                return await Task.FromResult(new RainfallReadingResponse { Readings = readings });
            }
        }

        private RainfallController _controller;

        [SetUp]
        public void Setup()
        {
            var mockService = new MockRainfallService();
            _controller = new RainfallController(mockService);
        }

        [Test]
        public async Task GetRainfallReadings_ReturnsSuccess()
        {
            var stationId = "123";
            var result = await _controller.GetRainfallReadings(stationId);

            Assert.That(result, Is.Not.Null);

            var objectResult = result.Result as OkObjectResult;
            Assert.That(objectResult, Is.Not.Null);

            var model = objectResult.Value as RainfallReadingResponse;
            Assert.That(model.Readings, Has.Count.EqualTo(2));
        }
    }
}