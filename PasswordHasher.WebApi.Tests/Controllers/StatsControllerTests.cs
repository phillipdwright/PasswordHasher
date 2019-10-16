using Moq;
using NUnit.Framework;
using PasswordHasher.Core;
using PasswordHasher.WebApi.Controllers;
using PasswordHasher.WebApi.Models;

namespace PasswordHasher.WebApi.Tests.Controllers
{
    public class StatsControllerTests
    {
        private Mock<IStatsProvider> _mockStatsProvider;

        private StatsController _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockStatsProvider = new Mock<IStatsProvider>();

            _classUnderTest = new StatsController(_mockStatsProvider.Object);
        }

        [Test]
        public void Get_ReturnsStatsFromProvider()
        {
            var expectedTotal = 5;
            var expectedAverage = 10;
            _mockStatsProvider.Setup(sp => sp.GetJobCount()).Returns(expectedTotal);
            _mockStatsProvider.Setup(sp => sp.GetAverageProcessTimeInMilliseconds()).Returns(expectedAverage);

            var result = _classUnderTest.Get();

            Assert.That(result.Value, Has.Property(nameof(StatsResponse.Total)).EqualTo(expectedTotal));
            Assert.That(result.Value, Has.Property(nameof(StatsResponse.Average)).EqualTo(expectedAverage));
        }
    }
}