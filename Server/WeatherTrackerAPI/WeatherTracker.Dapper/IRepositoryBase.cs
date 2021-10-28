using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Dapper
{
    public interface IRepositoryBase<T>
    {
        //Task Insert(T entity, string insertSql);
        Task<int> Insert(T entity, string insertSql);
        Task Update(T entity, string updateSql);
        Task Delete(int Id, string deleteSql);
        Task<List<T>> Select(string selectSql);
        Task<T> Detail(int Id, string detailSql);
        
        /// <summary>
        /// Stored Procedure
        /// </summary>
        /// <param name="SPName"></param>
        /// <returns></returns>
        Task<List<T>> ExecQuerySP(string SPName);
    }
}
