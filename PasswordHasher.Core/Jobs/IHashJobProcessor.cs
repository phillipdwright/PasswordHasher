using PasswordHasher.Core.Entities;

namespace PasswordHasher.Core.Jobs
{
    public interface IHashJobProcessor
    {
        Job Process(Job job, string input);
    }
}