namespace PasswordHasher.Core
{
    public interface IStatsProvider
    {
        int GetJobCount();
        int GetAverageProcessTimeInMilliseconds();
    }
}