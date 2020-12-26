using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Contracts;
//using Repository.Database;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
//using Microsoft.AspNetCore.Http;
using repo = Repository.Contracts;
using IAccount = BussinessLayer.Contracts.IAccount;
using Microsoft.AspNetCore.Http;
using DomainObject;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BussinessLayer
{
    public class AccountService : IAccount
    {
        private readonly repo.IAccount _account;
        private readonly IConfiguration _configuration;

        public AccountService(repo.IAccount accountServices, IConfiguration configuration)
        {
            _account = accountServices ?? throw new ArgumentNullException(nameof(accountServices));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            try
            {
                var user = await _account.Authenticate(username, password);

                if (user != null)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    user.Token = tokenString;
                }
               
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<string> Delete()
        {
            throw new NotImplementedException();
        }

        public  Task<List<AccountService>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountService> GetUser(string userID)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Upsert(User user)
        {
            try
            {
                return await _account.Upsert(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
