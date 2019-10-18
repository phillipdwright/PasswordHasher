using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PasswordHasher.Core.Entities;
using PasswordHasher.Core.Jobs;
using PasswordHasher.Core.Repositories;

namespace PasswordHasher.Core.Tests.Jobs
{
    public class JobEngineTests
    {
        private Mock<IJobRepository> _mockJobRepository;
        private Mock<IJobBag> _mockJobBag;
        private Mock<IHashJobProcessor> _mockHashJobProcessor;

        private JobEngine _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockJobRepository = new Mock<IJobRepository>();
            _mockJobBag = new Mock<IJobBag>();
            _mockHashJobProcessor = new Mock<IHashJobProcessor>();

            _classUnderTest = new JobEngine(
                _mockJobRepository.Object,
                _mockJobBag.Object,
                _mockHashJobProcessor.Object
            );
        }

        [Test]
        public void GetJobResult_GetsResultFromRepository()
        {
            var expectedResult = "Job result";
            _mockJobRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(new Job { Result = expectedResult });

            var actualResult = _classUnderTest.GetJobResult(1);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void StartJob_WhenNoNewJobsCanBeAdded_DoesNotReturnId()
        {
            NoNewJobsCanBeAdded();
            _mockJobRepository.Setup(r => r.Insert(It.IsAny<Job>())).Returns(new Job());

            var jobId = _classUnderTest.StartJob("password");

            Assert.That(jobId, Is.Null);
        }

        [Test]
        public void StartJob_WhenNoNewJobsCanBeAdded_RemovesEmptyJob()
        {
            NoNewJobsCanBeAdded();
            var emptyJob = new Job();
            _mockJobRepository.Setup(r => r.Insert(It.IsAny<Job>())).Returns(emptyJob);

            _classUnderTest.StartJob("password");

            _mockJobRepository.Verify(r => r.Delete(emptyJob));
        }

        [Test]
        public void StartJob_WhenNewJobsCanBeAdded_ReturnsJobId()
        {
            NewJobsCanBeAdded();
            var expectedJobId = 5;
            _mockJobRepository.Setup(r => r.Insert(It.IsAny<Job>())).Returns(new Job { Id = expectedJobId});

            var jobId = _classUnderTest.StartJob("password");

            Assert.That(jobId, Is.EqualTo(expectedJobId));
        }

        private void NoNewJobsCanBeAdded()
        {
            _mockJobBag.Setup(jb => jb.TryAdd(It.IsAny<int>(), It.IsAny<Task>()))
                .Returns(false);
        }

        private void NewJobsCanBeAdded()
        {
            _mockJobBag.Setup(jb => jb.TryAdd(It.IsAny<int>(), It.IsAny<Task>()))
                .Returns(true);
        }
    }
}