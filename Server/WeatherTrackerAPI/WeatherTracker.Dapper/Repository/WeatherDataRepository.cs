using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTracker.Dapper.Entities;
using WeatherTracker.Dapper.IRepository;

namespace WeatherTracker.Dapper.Repository
{
    public class WeatherDataRepository : RepositoryBase<WeatherData>, IWeatherDataRepository
    {
        public WeatherDataRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<WeatherData>> GetWeatherData(int queryLogId)
        {
            var q = $"SELECT * FROM [dbo].[WeatherData] WHERE QueryLogId={queryLogId}";
            return (await Select(q)).ToList();
        }

        public async Task PostWeatherData(IEnumerable<WeatherData> weathers)
        {
            var q = @"INSERT INTO [dbo].[WeatherData](QueryLogId, LocationName, Weather, MinTemperature,
                MaxTemperature, ChanceOfRain, Comfort, StartTime, EndTime, Timestamp) 
                VALUES(@QueryLogId, @LocationName, @Weather, @MinTemperature,
                @MaxTemperature, @ChanceOfRain, @Comfort, @StartTime, @EndTime, @Timestamp) ";
            var tasks = weathers.ToList().Select(w => Insert(w,q));
            var results = await Task.WhenAll(tasks);
        }
    }
}
