using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PasswordHasher.Core.Jobs;

namespace PasswordHasher.Core.Tests.Jobs
{
    public class JobBagTests : JobBag
    {
        private ConcurrentDictionary<int, Task> _jobs;

        private JobBagTests _classUnderTest;

        public JobBagTests() {}
        public JobBagTests(ConcurrentDictionary<int, Task> jobs) : base(jobs) {}

        [SetUp]
        public void Setup()
        {
            _jobs = new ConcurrentDictionary<int, Task>();
            _classUnderTest = new JobBagTests(_jobs);
        }

        [Test]
        public async Task TryAdd_AddsSelfRemovingJob()
        {
            var jobId = 1;

            _classUnderTest.TryAdd(jobId, Task.Delay(10));
            Assert.That(_jobs.ContainsKey(jobId));

            await Task.Delay(20);
            Assert.That(!_jobs.ContainsKey(jobId));
        }

        [Test]
        public async Task AwaitRemainingAsync_ReturnsWhenAllJobsAreComplete()
        {
            var jobs = Enumerable.Range(0, 10).Select(jobId => {
                var job = Task.Delay(10);
                _classUnderTest.TryAdd(jobId, job);
                return job;
            }).ToArray();

            await _classUnderTest.AwaitRemainingAsync();

            Assert.That(jobs, Is.Not.Empty);
            Assert.That(jobs, Has.All.Property(nameof(Task.IsCompleted)).True);
        }

        [Test]
        public void TryAdd_AddsJob()
        {
            var jobId = 1;

            var result = _classUnderTest.TryAdd(jobId, Task.Delay(10));

            Assert.That(_jobs.ContainsKey(jobId));
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task TryAdd_AfterAwaitingRemainingJobs_DoesNotAddJob()
        {
            var jobId = 1;
            await _classUnderTest.AwaitRemainingAsync();

            var result = _classUnderTest.TryAdd(jobId, Task.Delay(10));

            Assert.That(!_jobs.ContainsKey(jobId));
            Assert.That(result, Is.False);
        }
    }
}
