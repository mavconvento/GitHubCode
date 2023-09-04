using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Contracts
{
    public interface IMemberService
    {
        Task<DataSet> GetMemberDistance(string clubname, string memberidno, string dbname);
        Task<DataSet> GetLogs(String ClubID, String MobileNumber,String Keyword,String DateFrom,String DateTo, String DBName);
    }
}
