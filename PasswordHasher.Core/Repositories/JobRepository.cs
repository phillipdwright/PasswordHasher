using System.Collections.Generic;
using PasswordHasher.Core.Entities;

namespace PasswordHasher.Core.Repositories
{
    public class JobRepository : IJobRepository
    {
        private JobContext _dbContext;

        public JobRepository(JobContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Job Get(int id)
        {
            return _dbContext.Jobs.Find(id);
        }

        public IEnumerable<Job> GetAll()
        {
            return _dbContext.Jobs;
        }

        public Job Insert(Job job)
        {
            _dbContext.Jobs.Add(job);
            _dbContext.SaveChanges();
            return job;
        }

        public void Delete(Job jobEntity)
        {
            _dbContext.Jobs.Remove(jobEntity);
            _dbContext.SaveChanges();
        }

        public Job Update(Job job)
        {
            _dbContext.Jobs.Update(job);
            _dbContext.SaveChanges();
            return job;
        }
    }
}