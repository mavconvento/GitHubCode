using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObject.DatabaseObject;
using DomainObject;

namespace Repository.Contracts
{
    public interface IUsersRepository
    {
        Task<User> Authenticate(UserLogin user);
        Task<string> UserSave(User user);
        Task<List<User>> GetUserById(Int64 userId, Int64 id, Int64 companyId);
        Task<List<Teller>> GetTellerList(Int64 companyId, Int64 eventid, Int64 userid);
        Task<List<Role>> GetRole();
        Task<List<Company>> GetCompany();
    }
}
