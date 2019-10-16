using System.Threading.Tasks;

namespace PasswordHasher.Core
{
    public interface IJobBag
    {
        void Add(Task job);
        Task AwaitRemaining();
    }
}
