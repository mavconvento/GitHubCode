using Repository.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface ICommonRepository
    {
        Task<DataSet> WebClockingSave(String ClubName, String SMSMobileNumber, String Keyword, String Action, string UserID = "", String SMSMobileNumberTo = "", string dbName = "");
        Task<DataSet> ChargeText(string mobilenumber);
    }
}
