namespace LogServices
{
    public interface ILogProvider
    {
        void LogError(string message);

        void LogInfo(string message);
    }
}
