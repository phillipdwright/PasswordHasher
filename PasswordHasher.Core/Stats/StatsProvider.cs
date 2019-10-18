using System.Linq;
using PasswordHasher.Core.Repositories;

namespace PasswordHasher.Core.Stats
{
    public class StatsProvider : IStatsProvider
    {
        private IJobRepository _jobRepository;

        public StatsProvider(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public int GetAverageProcessTimeInMilliseconds()
        {
            return (int)_jobRepository
                    .GetAll()
                    .Average(j => j.ElapsedMilliseconds);
        }

        public int GetJobCount()
            => _jobRepository.Count();
    }
}