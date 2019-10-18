using System.Collections.Generic;
using PasswordHasher.Core.Entities;

namespace PasswordHasher.Core.Repositories
{
    public interface IJobRepository
    {
        void Delete(Job jobEntity);
        Job Get(int id);
        IEnumerable<Job> GetAll();
        Job Insert(Job job);
        Job Update(Job job);
    }
}