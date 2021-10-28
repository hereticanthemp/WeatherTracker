using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTracker.Dapper.Entities;

namespace WeatherTracker.Dapper.IRepository
{
    public interface IWeatherDataRepository : IRepositoryBase<WeatherData>
    {
        Task PostWeatherData(IEnumerable<WeatherData> weathers);
        Task<IEnumerable<WeatherData>> GetWeatherData(int queryLogId);
    }
}
