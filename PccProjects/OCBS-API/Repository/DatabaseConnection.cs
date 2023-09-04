using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using Repository.Contracts;
using Microsoft.Extensions.Configuration;

namespace Repository
{

    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly IConfiguration _configuration;
        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> DBConnection(string conStr = "DefaultConnection")
        {
            string conString = _configuration.GetConnectionString(conStr);
            return await Task<string>.FromResult(conString);
        }

        public async Task<bool> ExecuteScalarAsync(string procName, SqlParameter sqlParameters, string connectionString)
        {
            try
            {
                string connString = await DBConnection();
                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(procName, sql))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        if (sqlParameters != null) cmd.Parameters.Add(sqlParameters);
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
