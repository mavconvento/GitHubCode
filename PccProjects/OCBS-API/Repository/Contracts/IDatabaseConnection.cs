using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IDatabaseConnection
    {
        Task<string> DBConnection(string database = "DefaultConnection");
        //Task<DataSet> ExecuteSqlDataAdapter(string procName, SqlParameter sqlParameters, string connectionString);
        //Task<bool> ExecuteNonQueryAsync(string procName, SqlParameter sqlParameters, string connectionString);
        Task<bool> ExecuteScalarAsync(string procName, SqlParameter sqlParameters, string connectionString);
        //Task<T> ExecuteReadAsync<T>(string procName, SqlParameter sqlParameters, string connectionString,T entity);
    }
}
