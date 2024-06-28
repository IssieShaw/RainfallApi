using NUnit.Framework;
using RainfallApi.Models;
using RainfallApi.Services;
using RainfallApiTests.Services;

namespace RainfallApiTests
{
    [TestFixture]
    public class RainfallServiceTests
    {
        private IRainfallService _rainfallService;
        private IExternalApiService _fakeExternalApiService;
        private int _maxNumberReadings;

        [SetUp]
        public void SetUp()
        {
            var fakeReadings = new List<ExternalRainfallReading>
            {
                new ExternalRainfallReading { DateTime = DateTime.Now, Value = 10.5 },
                new ExternalRainfallReading { DateTime = DateTime.Now.AddDays(-1), Value = 5.3 },
                new ExternalRainfallReading { DateTime = DateTime.Now.AddDays(-2), Value = 7.8 }
            };

            _fakeExternalApiService = new FakeExternalApiService(fakeReadings);
            _rainfallService = new RainfallService(_fakeExternalApiService);
            _maxNumberReadings = fakeReadings.Count;
        }

        [Test]
        public async Task GetRainfallReadings_NumberOfReadings()
        {
            string stationId = "52204";
            int count = 2;

            var result = await _rainfallService.GetRainfallReadings(stationId, count);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Readings, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetRainfallReadings_Mappings()
        {
            string stationId = "52203";
            int count = _maxNumberReadings;

            var result = await _rainfallService.GetRainfallReadings(stationId, count);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Readings, Has.Count.EqualTo(_maxNumberReadings));
            Assert.Multiple(() =>
            {
                Assert.That(result.Readings[0].AmountMeasured, Is.EqualTo(10.5));
                Assert.That(result.Readings[1].AmountMeasured, Is.EqualTo(5.3));
                Assert.That(result.Readings[2].AmountMeasured, Is.EqualTo(7.8));
            });
        }
    }
}
