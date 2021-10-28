using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherTracker.Dapper.IRepository;
using WeatherTrackerAPI.Models.Resource;
using WeatherTrackerAPI.Services;

namespace WeatherTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration Configuration;
        private readonly IOpenDataProvider OpenDataProvider;
        private readonly IQueryLogRepository QueryLog;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, IOpenDataProvider openDataProvider, IQueryLogRepository queryLog)
        {
            _logger = logger;
            Configuration = configuration;
            OpenDataProvider = openDataProvider;
            QueryLog = queryLog;
        }

        [HttpGet]
        public async Task<Records> GetWeatherForecasts()
        {
            var resp = OpenDataProvider.Get36HourWeatherForecast();
            if (resp.success)
            {
                var id = await QueryLog.AddLog(new WeatherTracker.Dapper.Entities.QueryLog()
                {
                    Result = 0,
                    Timestamp = DateTime.Now
                });

                return resp.records;
            }
            else
            {
                var id = await QueryLog.AddLog(new WeatherTracker.Dapper.Entities.QueryLog()
                {
                    Result = 1,
                    Timestamp = DateTime.Now
                });
                return null;
            }

        }

        [HttpGet]
        [Route("Test")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
