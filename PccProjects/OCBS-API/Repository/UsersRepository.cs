using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObject.DatabaseObject;
using DomainObject;
using Repository.Contracts;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDatabaseConnection _dbconn;

        public UsersRepository(IDatabaseConnection databaseConnection)
        {
            _dbconn = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public async Task<User> Authenticate(UserLogin user)
        {
            try
            {

                User results = new User();

                string connString = await _dbconn.DBConnection();

                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("Authenticate", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", user.username);
                        cmd.Parameters.AddWithValue("@Password", user.password);

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Status = reader["Status"].ToString();
                                if (reader["Status"].ToString() == "success")
                                {
                                    results.firstName = reader["FirstName"].ToString();
                                    results.userId = (Int64)reader["UserId"];
                                    results.roleDescription = reader["role"].ToString();
                                    results.platformBearerToken = reader["TokenBearer"].ToString();
                                    results.platformUserId = reader["PlatformUserId"].ToString();
                                    results.IsOffline = (Boolean)reader["IsOffline"];
                                    results.companyId = (Int64)reader["CompanyId"];
                                }
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Company>> GetCompany()
        {
            try
            {
                List<Company> companies = new List<Company>();
                string connString = await _dbconn.DBConnection();
                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetCompany", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Company comp = new Company()
                                {
                                    CompanyName = reader["Name"].ToString(),
                                    CompanyId = (Int64)reader["Id"]
                                };

                                companies.Add(comp);
                            }
                        }
                    }
                }

                return companies;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public  async Task<List<Role>> GetRole()
        {
            try
            {
                List<Role> roles = new List<Role>();
                string connString = await _dbconn.DBConnection();
                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserRole", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Role role = new Role()
                                {
                                    Description = reader["Description"].ToString(),
                                    RoleId = (Int64)reader["Id"]
                                };

                                roles.Add(role);
                            }
                        }
                    }
                }

                return roles;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Teller>> GetTellerList(Int64 companyId, Int64 eventid, Int64 userid)
        {
            try
            {
                List<Teller> results = new List<Teller>();

                string connString = await _dbconn.DBConnection();
                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTeller", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@companyId", companyId);
                        cmd.Parameters.AddWithValue("@eventId", eventid);

                        if (userid != 0) cmd.Parameters.AddWithValue("@userid", userid);

                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Teller teller = new Teller()
                                {
                                    CompanyName = reader["CompanyName"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Percent = reader["Percent"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    Userid = (Int64)reader["Userid"],
                                    UserName = reader["UserName"].ToString(),
                                    //CurrentPoints = (Decimal)reader["CurrentPoints"],
                                    CashAdvance = (Decimal)reader["CashAdvance"],
                                    Payout = (Decimal)reader["TotalPayout"],
                                    CashOnhand = (Decimal)reader["CashOnhand"],
                                    TotalBetRunning = (Decimal)reader["TotalBetRunning"],
                                    Commision = (Decimal)reader["Commission"]
                                };

                                results.Add(teller);
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> GetUserById(long userId, long id, long companyId)
        {
            try
            {
                List<User> results = new List<User>();

                string connString = await _dbconn.DBConnection();
                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserById", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@companyId", companyId);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@userid", userId);

                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                User user = new User()
                                {
                                    userName = reader["username"].ToString(),
                                    firstName = reader["FirstName"].ToString(),
                                    lastName = reader["LastName"].ToString(),
                                    roleDescription = reader["description"].ToString(),
                                    companyId = (Int64)reader["companyid"],
                                    companyName = reader["companyname"].ToString(),
                                    userId = (Int64)reader["userid"],
                                    RoleId = (Int64)reader["roleid"],
                                    IsActive = (bool)reader["isactive"],
                                    Status = reader["status"].ToString(),
                                };

                                results.Add(user);
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UserSave(User user)
        {
            try
            {
                string results = "";

                string connString = await _dbconn.DBConnection();
                using (SqlConnection sql = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserSave", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@userid", user.userId);
                        cmd.Parameters.AddWithValue("@username", user.userName);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@firstname", user.firstName);
                        cmd.Parameters.AddWithValue("@lastname", user.lastName);
                        cmd.Parameters.AddWithValue("@companyid", user.companyId);
                        cmd.Parameters.AddWithValue("@roleid", user.RoleId);
                        cmd.Parameters.AddWithValue("@isactive", user.IsActive);


                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results = reader["status"].ToString();
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                throw;
            }

        }
    }
}
