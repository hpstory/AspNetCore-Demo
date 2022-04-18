using Microsoft.Extensions.Logging;

namespace LoggingDemo
{
    public class DatabaseLog
    {
        private readonly ILogger<DatabaseLog> logger;

        public DatabaseLog(ILogger<DatabaseLog> logger)
        {
            this.logger = logger;
        }

        public void SaveLog()
        {
            this.logger.LogTrace("===========");
            this.logger.LogDebug("start");
            this.logger.LogInformation("success");
            this.logger.LogWarning("some problems, but still running");
            this.logger.LogError("error");
        }
    }
}
