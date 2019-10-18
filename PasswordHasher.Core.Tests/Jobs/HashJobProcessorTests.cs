using Moq;
using NUnit.Framework;
using PasswordHasher.Core.Entities;
using PasswordHasher.Core.Hashing;
using PasswordHasher.Core.Jobs;

namespace PasswordHasher.Core.Tests.Jobs
{
    public class HashJobProcessorTests
    {
        private Mock<IHasher> _mockHasher;
        private HashJobProcessor _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockHasher = new Mock<IHasher>();
            _classUnderTest = new HashJobProcessor(_mockHasher.Object);
        }

        [Test]
        public void Process_ReturnsJobWithHashedResult()
        {
            var expectedResult = "Hashed result";
            _mockHasher.Setup(h => h.ToBase64EncodedSha512(It.IsAny<string>())).Returns(expectedResult);

            var processedJob = _classUnderTest.Process(new Job(), "toHash");

            Assert.That(processedJob.Result, Is.EqualTo(expectedResult));
        }
    }
}