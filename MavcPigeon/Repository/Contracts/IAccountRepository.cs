using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace Repository.Contracts
{
    public interface IAccountRepository
    {
        Task<String> Insert(User user);
        Task<String> Update(User user);
        Task<User> Authenticate(string userName, string password);
    }
}
