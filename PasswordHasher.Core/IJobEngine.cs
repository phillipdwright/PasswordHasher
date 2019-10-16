namespace PasswordHasher.Core
{
    public interface IJobEngine
    {
        string GetJobResult(int jobId);
    }
}
