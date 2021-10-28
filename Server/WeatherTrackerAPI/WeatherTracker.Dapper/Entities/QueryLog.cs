using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Dapper.Entities
{
    public class QueryLog : BaseModel
    {
        public int Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
