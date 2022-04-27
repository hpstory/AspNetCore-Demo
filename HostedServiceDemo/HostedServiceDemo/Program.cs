using HostedServiceDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHostedService<DemoHostedService>();
builder.Services.AddScoped<MyScopeService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
