namespace PasswordHasher.Core.Stats
{
    public interface IStatsProvider
    {
        int GetJobCount();
        int GetAverageProcessTimeInMilliseconds();
    }
}