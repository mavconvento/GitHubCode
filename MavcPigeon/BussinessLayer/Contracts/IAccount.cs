using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace BussinessLayer.Contracts
{
    public interface IAccount
    {
        Task<String> Insert(User user);
        Task<String> Update(Profile profile);
        Task<User> Authenticate(string username, string password);
        Task<String> Delete();
        Task<List<AccountService>> GetAll();
        Task<AccountService> GetUser(string userID);
    }
}
