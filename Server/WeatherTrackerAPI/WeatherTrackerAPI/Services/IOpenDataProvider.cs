using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherTrackerAPI.Models.Resource;

namespace WeatherTrackerAPI.Services
{
    public interface IOpenDataProvider
    {
        WeatherResponseBody Get36HourWeatherForecast();
    }
}
