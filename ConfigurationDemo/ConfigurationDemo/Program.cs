using ConfigurationDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// read config
ConfigurationBuilder builder = new ConfigurationBuilder();

// from json
// builder.AddJsonFile("config.json", optional: true, reloadOnChange: true);
// from command line
// dotnet ConfigurationDemo.dll name="foo" age=18 proxy:address="127.0.0.1" proxy:ports:0=5000
// builder.AddCommandLine(args);
// from environment variables
builder.AddEnvironmentVariables();
IConfigurationRoot configRoot = builder.Build();
string name = configRoot["name"];
Console.WriteLine(name);
string address = configRoot.GetSection("proxy:address").Value;
Console.WriteLine(address);

// read by type
Proxy proxy = configRoot.GetSection("proxy").Get<Proxy>();
Console.WriteLine(proxy.Address);
Console.WriteLine(proxy.Ports[0]);

// use DI
ServiceCollection services = new ServiceCollection();
services.AddScoped<ConfigController>();
services.AddScoped<ProxyController>();
services.AddOptions()
    .Configure<Config>(c => configRoot.Bind(c))
    .Configure<Proxy>(p => configRoot.GetSection("proxy").Bind(p));

using (ServiceProvider sp = services.BuildServiceProvider())
{
    var configController = sp.GetRequiredService<ConfigController>();
    configController.Read();
    var proxyController = sp.GetRequiredService<ProxyController>();
    proxyController.Read();
}
