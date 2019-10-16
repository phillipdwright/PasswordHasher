using Moq;
using NUnit.Framework;
using PasswordHasher.Core;
using PasswordHasher.WebApi.Controllers;

namespace PasswordHasher.WebApi.Tests.Controllers
{
    public class HashControllerTests
    {
        private Mock<IJobEngine> _mockJobEngine;

        private HashController _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockJobEngine = new Mock<IJobEngine>();

            _classUnderTest = new HashController(_mockJobEngine.Object);
        }

        [Test]
        public void GetByJobId_ReturnsResultFromJobMonitor()
        {
            var jobId = 3;
            var expectedResult = "Job Engine Result";
            _mockJobEngine.Setup(je => je.GetJobResult(jobId)).Returns(expectedResult);

            var result = _classUnderTest.GetByJobId(jobId);

            Assert.That(result.Value, Is.EqualTo(expectedResult));
        }
    }
}