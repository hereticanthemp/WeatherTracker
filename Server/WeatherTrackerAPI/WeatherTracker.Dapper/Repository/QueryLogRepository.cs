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
        public QueryLogRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<int> AddLog(QueryLog entity)
        {
            string insertSql = @"INSERT INTO [dbo].[QueryLog](Result, Timestamp) VALUES(@Result, @Timestamp)";
            return await Insert(entity, insertSql);
        }

        public async Task<IEnumerable<QueryLog>> GetLatestLogs(int count = 1)
        {
            if (count < 1) throw new ArgumentException();
            var q = $"SELECT TOP {count} * FROM [dbo].[QueryLog] ORDER BY Id DESC";
            return (await Select(q)).ToList();
        }

        public async Task<QueryLog> GetLatestSuccess()
        {
            var q = $"SELECT TOP 1 * FROM [dbo].[QueryLog] WHERE RESULT=0 ORDER BY Id DESC";
            return (await Select(q)).Single();
        }

        public async Task<IEnumerable<QueryLog>> GetLogs(DateTime? startTime, DateTime? endTime)
        {
            throw new NotImplementedException();
        }
    }
}
