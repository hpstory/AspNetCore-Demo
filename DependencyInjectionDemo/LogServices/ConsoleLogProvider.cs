using System;

namespace LogServices
{
    class ConsoleLogProvider : ILogProvider
    {
        public void LogError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void LogInfo(string message)
        {
            Console.WriteLine($"Info: {message}");
        }
    }
}
