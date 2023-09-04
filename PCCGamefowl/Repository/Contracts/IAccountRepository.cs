using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace Repository.Contracts
{
    public interface IAccountRepository
    {
        Task<String> Insert(User user);
        Task<DataTable> Update(Profile profile);
        Task<User> Authenticate(string userName, string password);
        Task<DataSet> GetLinkMobileNumber(string userid);
        Task<DataSet> Pasaload(string mobileNumberFrom, string mobileNumberTo, string Amount);
        Task<DataSet> LoadMavcCard(string ClubName, string mobileNumber, string keyword, string dbName);
        Task<DataSet> Unreg(string ClubName, string mobileNumber, string keyword, string DBName, string UserID);
        Task<string> SetAsPrimary(string mobileNumber, string UserID);
        Task<DataSet> GetMobileListByEmail(string email);
        Task<DataSet> ValidateMobileNumber(string email, string mobile);
        Task<DataSet> GetMemberCoordinates(string memberidno, string clubname, string dbname);
        Task<DataSet> GetVideo(string type);
    }
}
