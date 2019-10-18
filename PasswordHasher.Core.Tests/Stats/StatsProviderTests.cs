using System.Linq;
using Moq;
using NUnit.Framework;
using PasswordHasher.Core.Entities;
using PasswordHasher.Core.Repositories;
using PasswordHasher.Core.Stats;

namespace PasswordHasher.Core.Tests.Stats
{
    public class StatsProviderTests
    {
        private Mock<IJobRepository> _mockJobRepository;
        private StatsProvider _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockJobRepository = new Mock<IJobRepository>();

            _classUnderTest = new StatsProvider(_mockJobRepository.Object);
        }

        [Test]
        public void GetJobCount_ReturnsCountFromRepository()
        {
            var expectedCount = 10;
            _mockJobRepository.Setup(r => r.Count()).Returns(expectedCount);

            var actualCount = _classUnderTest.GetJobCount();

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public void GetAverageProcessTimeInMilliseconds_ReturnsAverageOfTimesFromRepository()
        {
            var expectedTimes = new[] { 5, 18, 2, 25 };
            var expectedAverageTime = (int)expectedTimes.Average();
            var jobs = expectedTimes.Select(t => new Job { ElapsedMilliseconds = t });
            _mockJobRepository.Setup(j => j.GetAll()).Returns(jobs);

            var actualAverage = _classUnderTest.GetAverageProcessTimeInMilliseconds();

            Assert.That(actualAverage, Is.EqualTo(expectedAverageTime));
        }
    }
}