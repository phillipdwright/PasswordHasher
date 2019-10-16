namespace PasswordHasher.Core
{
    public interface IJobEngine
    {
        int? StartJob(string toHash);
        string GetJobResult(int jobId);
    }
}
