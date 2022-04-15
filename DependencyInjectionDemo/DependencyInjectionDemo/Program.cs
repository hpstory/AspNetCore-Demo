using ConfigServices;
using MailServices;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new ServiceCollection();
services.AddScoped<IConfigService, EnvironmentConfigService>();
// services.AddScoped<IConfigService>(s => new IniFileConfigService { FilePath = "mail.ini" });
services.AddIniFileConfig("mail.ini");
services.AddLayeredConfig();
services.AddScoped<IMailService, MailService>();
// services.AddScoped<ILogProvider, ConsoleLogProvider>();
services.AddConsoleLog();

using (ServiceProvider sp = services.BuildServiceProvider())
{
    var mailService = sp.GetRequiredService<IMailService>();
    mailService.Send("hello", "c#", "hello world");
}
