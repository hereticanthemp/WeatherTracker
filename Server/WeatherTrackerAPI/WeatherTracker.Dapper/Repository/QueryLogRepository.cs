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
    public class QueryLogRepository : RepositoryBase<QueryLog>, IQueryLogRepository
    {
        public QueryLogRepository(IConfiguration configuration) :base(configuration)
        { 

        }
        public async Task<int> AddLog(QueryLog entity)
        {
            string insertSql = @"INSERT INTO [dbo].[QueryLog](Result, Timestamp) VALUES(@Result, @Timestamp)";
            return await Insert(entity, insertSql);
        }

        Task<IEnumerable<QueryLog>> IQueryLogRepository.GetLatestLogs(int count)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<QueryLog>> IQueryLogRepository.GetLogs(DateTime? startTime, DateTime? endTime)
        {
            throw new NotImplementedException();
        }
    }
}
