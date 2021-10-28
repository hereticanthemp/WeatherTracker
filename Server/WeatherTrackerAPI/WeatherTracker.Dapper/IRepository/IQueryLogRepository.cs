using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTracker.Dapper.Entities;

namespace WeatherTracker.Dapper.IRepository
{
    public interface IQueryLogRepository : IRepositoryBase<QueryLog>
    {
        // Extension Query
        Task<int> AddLog(QueryLog entity);
        Task<IEnumerable<QueryLog>> GetLogs(DateTime? startTime = null, DateTime? endTime = null);
        Task<IEnumerable<QueryLog>> GetLatestLogs(int count = 1);
        Task<QueryLog> GetLatestSuccess();
    }
}
