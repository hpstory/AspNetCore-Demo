using Microsoft.Extensions.Logging;

namespace LoggingDemo
{
    public class WebLog
    {
        private readonly ILogger<WebLog> logger;

        public WebLog(ILogger<WebLog> logger)
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
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
            }
        }
    }
}
