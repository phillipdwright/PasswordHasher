using System.Threading.Tasks;

namespace PasswordHasher.Core.Jobs
{
    public interface IJobBag
    {
        bool TryAdd(int jobId, Task job);
        Task AwaitRemainingAsync();
    }
}
