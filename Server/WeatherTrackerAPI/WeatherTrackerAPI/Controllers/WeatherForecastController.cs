using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherTracker.Dapper.Entities;
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
        private readonly IWeatherDataRepository WeatherData;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, IOpenDataProvider openDataProvider, IQueryLogRepository queryLog, IWeatherDataRepository weatherData)
        {
            _logger = logger;
            Configuration = configuration;
            OpenDataProvider = openDataProvider;
            QueryLog = queryLog;
            WeatherData = weatherData;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherData>> GetWeatherForecasts()
        {
            var latest = await QueryLog.GetLatestSuccess();

            if (latest != null)
            {
                var t = latest.Timestamp;
                if (DateTime.Now - t < TimeSpan.FromMinutes(1))
                {
                    // return weather data from database
                    return await WeatherData.GetWeatherData(latest.Id);
                }
                else
                {
                    // send new request below
                }

            }

            var resp = OpenDataProvider.Get36HourWeatherForecast();
            if (resp != null && resp.success)
            {
                var id = await QueryLog.AddLog(new WeatherTracker.Dapper.Entities.QueryLog()
                {
                    Result = 0,
                    Timestamp = DateTime.Now
                });

                var datas = resp.records.location.Select(location =>
                {
                    var weather = location.weatherElement
                                        .Find(w => w.elementName == "Wx")
                                        .time.FirstOrDefault().parameter.parameterName;
                    var minT = location.weatherElement
                                        .Find(w => w.elementName == "MinT")
                                        .time.FirstOrDefault().parameter.parameterName;
                    var maxT = location.weatherElement
                                        .Find(w => w.elementName == "MaxT")
                                        .time.FirstOrDefault().parameter.parameterName;
                    var pop = location.weatherElement
                                        .Find(w => w.elementName == "PoP")
                                        .time.FirstOrDefault().parameter.parameterName;
                    var ci = location.weatherElement
                                        .Find(w => w.elementName == "CI")
                                        .time.FirstOrDefault().parameter.parameterName;

                    var startT = location.weatherElement.FirstOrDefault()
                                    .time.FirstOrDefault().startTime;
                    var endT = location.weatherElement.FirstOrDefault()
                                    .time.FirstOrDefault().endTime;
                    var data =
                        new WeatherData()
                        {
                            QueryLogId = id,
                            LocationName = location.LocationName,
                            Weather = weather,
                            MinTemperature = int.Parse(minT),
                            MaxTemperature = int.Parse(maxT),
                            ChanceOfRain = int.Parse(pop),
                            Comfort = ci,
                            StartTime = DateTime.Parse(startT),
                            EndTime = DateTime.Parse(endT),
                            Timestamp = DateTime.Now
                        };
                    return data;
                });

                await WeatherData.PostWeatherData(datas);

                return datas;
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
