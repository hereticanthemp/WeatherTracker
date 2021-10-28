using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WeatherTracker.Dapper
{
    public class RepositoryBase<T> : IRepositoryBase<T>
    {
        private IConfiguration Configuration;
        string connectionString = "";//"Server=localhost;Database=TestDB;User Id=sa;Password=Abcd@1234;";

        public RepositoryBase(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("SqlConnection");
        }

        public async Task Delete(int Id, string deleteSql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                await conn.ExecuteAsync(deleteSql, new { Id = Id });
            }
        }

        public async Task<T> Detail(int Id, string detailSql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<T>(detailSql, new { Id = Id });
            }
        }

        public async Task<List<T>> ExecQuerySP(string SPName)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                //return await Task.Run(() => conn.Query<T>(SPName, null, null, true, null, CommandType.StoredProcedure).ToList());
                return (await conn.QueryAsync<T>(SPName, null, null, null, CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<int> Insert(T entity, string insertSql)
        {
            var queryScopeId = "SELECT CAST(SCOPE_IDENTITY() as int)";
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                return (await conn.QueryAsync<int>(insertSql + queryScopeId, entity)).Single();
            }
        }

        public async Task<List<T>> Select(string selectSql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                //return await Task.Run(() => conn.Query<T>(selectSql).ToList());
                return (await conn.QueryAsync<T>(selectSql)).ToList();
            }
        }

        public async Task Update(T entity, string updateSql)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                await conn.ExecuteAsync(updateSql, entity);
            }
        }
    }
}
