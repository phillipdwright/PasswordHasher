using PasswordHasher.Core.Entities;
using PasswordHasher.Core.Hashing;

namespace PasswordHasher.Core.Jobs
{
    public class HashJobProcessor : IHashJobProcessor
    {
        private IHasher _hasher;

        public HashJobProcessor(IHasher hasher)
        {
            _hasher = hasher;
        }

        public Job Process(Job job, string toHash)
        {
            var result = _hasher.ToBase64EncodedSha512(toHash);
            job.Result = result;
            return job;
        }
    }
}