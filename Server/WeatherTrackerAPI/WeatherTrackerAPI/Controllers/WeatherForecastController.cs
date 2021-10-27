using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, IOpenDataProvider openDataProvider)
        {
            _logger = logger;
            Configuration = configuration;
            OpenDataProvider = openDataProvider;
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

        [HttpGet]
        public Records GetWeatherForecasts()
        {
            return OpenDataProvider.Get36HourWeatherForecast().records;
        }
    }
}
