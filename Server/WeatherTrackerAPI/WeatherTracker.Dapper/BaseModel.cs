using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Dapper
{
    public class BaseModel : IEntity<int>
    {
        public int Id { get; set; }
    }
}
