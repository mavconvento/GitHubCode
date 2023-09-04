using BusinessLayer.Contracts;
using DomainObject.DatabaseObject;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Contracts;
using DomainObject.PlatformObject;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer
{
    public class Users : IUsers
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPlatform _platform;
        private readonly IConfiguration _configuration;


        public Users(IUsersRepository usersRepository, IPlatform platform, IConfiguration config)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _platform = platform ?? throw new ArgumentNullException(nameof(platform));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
        }
        public async Task<User> Authenticate(UserLogin user)
        {
            try
            {
                var relativeurl = "login";
                var login = await _usersRepository.Authenticate(user);
                if (login.Status == "success")
                {
                    if (login.IsOffline)
                    {
                        login.platformBearerToken = _configuration["Platform:token"];
                        login.platformUserId = login.userId.ToString();
                    }
                    else
                    {
                        var result = await _platform.PostAsync<PlatformLogin, UserLogin>(relativeurl, "", "", user);
                        if (result.success == 1)
                        {
                            login.platformBearerToken = result.token;
                            login.platformUserId = result.data.userid.ToString();
                        }
                    }

                }
                return login;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  async Task<List<Company>> GetCompany()
        {
            try
            {
                return await _usersRepository.GetCompany();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Role>> GetRole()
        {
            try
            {
                return await _usersRepository.GetRole();
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
                return await _usersRepository.GetTellerList(companyId, eventid, userid);
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
                return await _usersRepository.GetUserById(userId, id, companyId);
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
                return await _usersRepository.UserSave(user);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
