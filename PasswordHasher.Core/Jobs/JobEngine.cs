using System.Threading.Tasks;
using PasswordHasher.Core.Entities;
using PasswordHasher.Core.Repositories;

namespace PasswordHasher.Core.Jobs
{
    public class JobEngine : IJobEngine
    {
        private IJobRepository _jobRepository;
        private IJobBag _jobBag;
        private IHashJobProcessor _hashJobProcessor;

        public JobEngine(IJobRepository jobRepository, IJobBag jobBag, IHashJobProcessor hashJobProcessor)
        {
            _jobRepository = jobRepository;
            _jobBag = jobBag;
            _hashJobProcessor = hashJobProcessor;
        }

        public string GetJobResult(int jobId)
        {
            var job = _jobRepository.Get(jobId);
            return job.Result;
        }

        public int? StartJob(string toHash)
        {
            var jobEntity = CreateNewJobEntity();

            var job = Task.Run(() => HashAndStore(toHash, jobEntity));
            if (_jobBag.TryAdd(jobEntity.Id, job))
                return jobEntity.Id;

            RemoveJobEntity(jobEntity);
            return null;
        }

        private Job CreateNewJobEntity()
        {
            var job = new Job();
            return _jobRepository.Insert(job);
        }

        private void RemoveJobEntity(Job jobEntity)
        {
            _jobRepository.Delete(jobEntity);
        }

        private void HashAndStore(string toHash, Job jobEntity)
        {
            jobEntity = _hashJobProcessor.Process(jobEntity, toHash);
            _jobRepository.Update(jobEntity);
        }
    }
}