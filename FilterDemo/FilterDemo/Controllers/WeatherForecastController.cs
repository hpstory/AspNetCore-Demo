using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace FilterDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;

        private readonly DemoDbContext dbContext;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            DemoDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("error")]
        public Task GetError()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Add()
        {
            var weather = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            var anotherWeather = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "a very long summary"
            };

            //{
            //    // first save success
            //    dbContext.WeatherForecasts.Add(weather);
            //    dbContext.SaveChanges();
            //    // second save failed
            //    dbContext.WeatherForecasts.Add(anotherWeather);
            //    dbContext.SaveChanges();
            //}

            {
                using (TransactionScope ts = new TransactionScope())
                {
                    // both failed
                    dbContext.WeatherForecasts.Add(weather);
                    dbContext.SaveChanges();
                    dbContext.WeatherForecasts.Add(anotherWeather);
                    dbContext.SaveChanges();
                    ts.Complete();
                }
            }
        }

        [HttpPost("async")]
        public async Task AddAsync()
        {
            var weather = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            var anotherWeather = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "a summary"
            };

            {
                // for async
                using (TransactionScope ts = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    // both success
                    dbContext.WeatherForecasts.Add(weather);
                    await dbContext.SaveChangesAsync();
                    dbContext.WeatherForecasts.Add(anotherWeather);
                    await dbContext.SaveChangesAsync();
                    ts.Complete();
                }
            }
        }

        [HttpPost("asyncWithFilter")]
        public async Task AddFilterAsync()
        {
            var weather = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            var anotherWeather = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "a very long summary"
            };

            {
                // both failed
                dbContext.WeatherForecasts.Add(weather);
                await dbContext.SaveChangesAsync();
                dbContext.WeatherForecasts.Add(anotherWeather);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}