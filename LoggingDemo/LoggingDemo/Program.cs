using LoggingDemo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel.Resolution;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

ServiceCollection services = new ServiceCollection();

services.AddLogging(logBuilder =>
{
    // To Console
    // logBuilder.AddConsole();

    // To File and Console
    // NLog
    // logBuilder.AddNLog();
    // logBuilder.SetMinimumLevel(LogLevel.Trace);

    // Serilog
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File(new CompactJsonFormatter(), "log.txt",
            rollingInterval: RollingInterval.Day,
            rollOnFileSizeLimit: true)
        .CreateLogger();
    logBuilder.AddSerilog();  
});

services.AddScoped<DatabaseLog>();
services.AddScoped<WebLog>();
using (var sp = services.BuildServiceProvider())
{
    var dbService = sp.GetRequiredService<DatabaseLog>();
    dbService.SaveLog();

    var webService = sp.GetRequiredService<WebLog>();
    webService.SaveLog();
}
