using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PasswordHasher.Core.Jobs;
using PasswordHasher.WebApi.Controllers;
using PasswordHasher.WebApi.Models;

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

        [Test]
        public void Create_GivenJobEngineReturnsId_ReturnsJobIdFromJobEngine()
        {
            var password = "A password";
            var request = new CreateHashRequest { Password = password };
            var expectedJobId = 2;
            _mockJobEngine.Setup(je => je.StartJob(password)).Returns(expectedJobId);

            var result = _classUnderTest.Create(request);

            Assert.That(result, Has.Property(nameof(AcceptedResult.Value)).EqualTo(expectedJobId));
        }

        [Test]
        public void Create_GivenJobEngineReturnsNoJobId_ReturnsServiceUnavailable()
        {
            var password = "A password";
            var request = new CreateHashRequest { Password = password };
            _mockJobEngine.Setup(je => je.StartJob(password)).Returns(default(int?));

            var result = _classUnderTest.Create(request);

            Assert.That(result, Has.Property(nameof(StatusCodeResult.StatusCode)).EqualTo((int)HttpStatusCode.ServiceUnavailable));
        }
    }
}