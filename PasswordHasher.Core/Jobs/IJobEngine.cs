namespace PasswordHasher.Core.Jobs
{
    public interface IJobEngine
    {
        int? StartJob(string toHash);
        string GetJobResult(int jobId);
    }
}
