using System.Net;
using System.Text.Json;
using NUnit.Framework;
using RainfallApi.Models;
using RainfallApi.Services;
using RainfallApiTests.Services;

namespace RainfallApiTests
{
    [TestFixture]
    public class ExternalApiServiceTests
    {
        private IExternalApiService _externalApiService;
        private HttpClient _httpClient;
        private int _numResponses;

        [SetUp]
        public void SetUp()
        {
            var fakeReadings = new List<ExternalRainfallReading>
            {
                new ExternalRainfallReading { DateTime = DateTime.Now, Value = 1 },
                new ExternalRainfallReading { DateTime = DateTime.Now.AddDays(-1), Value = 3.4 }
            };

            _numResponses = fakeReadings.Count;

            var fakeResponseContent = JsonSerializer.Serialize(new ExternalRainfallReadingResponse
            {
                Items = fakeReadings
            });

            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fakeResponseContent)
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeResponse);
            _httpClient = new HttpClient(fakeHttpMessageHandler);
            _externalApiService = new ExternalApiService(_httpClient);
        }

        [Test]
        public async Task GetExternalRainfallReadings_ApiResponseMapping()
        {
            string stationId = "52203";
            int count = _numResponses;

            var result = await _externalApiService.GetExternalRainfallReadings(stationId, count);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Has.Count.EqualTo(_numResponses));
            Assert.Multiple(() =>
            {
                Assert.That(result.Items[0].Value, Is.EqualTo(1));
                Assert.That(result.Items[1].Value, Is.EqualTo(3.4));
            });
        }
    }
}
