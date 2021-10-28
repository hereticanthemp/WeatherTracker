using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Dapper.Entities
{
    public class WeatherData : BaseModel
    {
        public int QueryLogId { get; set; }
        public string LocationName { get; set; }
        public string Weather { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public int ChanceOfRain { get; set; }
        public string Comfort { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
