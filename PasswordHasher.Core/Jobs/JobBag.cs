using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordHasher.Core.Jobs
{
    public class JobBag : IJobBag
    {
        private ConcurrentDictionary<int, Task> _jobs;
        private bool _isFlushingJobs;

        public JobBag() : this(new ConcurrentDictionary<int, Task>()) {}

        protected JobBag(ConcurrentDictionary<int, Task> jobs)
        {
            _jobs = jobs;
            _isFlushingJobs = false;
        }

        public bool TryAdd(int jobId, Task job)
        {
            if (_isFlushingJobs)
                return false;

            job.ContinueWith(j => _jobs.TryRemove(jobId, out var _));
            _jobs.AddOrUpdate(jobId, job, (key, value) => job);
            return true;
        }

        public async Task AwaitRemainingAsync()
        {
            _isFlushingJobs = true;
            var toAwait = _jobs.Select(kvp => kvp.Value).ToArray();
            await Task.WhenAll(toAwait);
        }
    }
}
