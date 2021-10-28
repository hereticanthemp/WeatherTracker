using System;
using System.Data;
using System.Data.SqlClient;

namespace WeatherTracker.Dapper
{
    public class DataBaseConfig
    {
        public static IDbConnection GetSqlConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException();
            IDbConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
