using FilterDemo;
using FilterDemo.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<MyFirstActionFilter>();
    options.Filters.Add<MySecondActionFilter>();
    options.Filters.Add<MyExceptionFilter>();
    options.Filters.Add<TransactionScopeFilter>();
});
builder.Services.AddDbContext<DemoDbContext>(options =>
{
    options.UseSqlServer("Server=;Initial Catalog=Weather;User ID=sa;Password=Qwer1234;Connection Timeout=30;");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
