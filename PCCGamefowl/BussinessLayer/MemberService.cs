using BussinessLayer.Contracts;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _member;

        public MemberService(IMemberRepository memberRepository)
        {
            _member = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        }
        public async Task<DataSet> GetMemberDistance(string clubname, string memberidno, string dbname)
        {
            try
            {
                return await _member.GetMemberDistance(clubname, memberidno, dbname);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<DataSet> GetLogs(String ClubID, String MobileNumber, String Keyword, String DateFrom, String DateTo, String DBName)
        {
            try
            {
                return await _member.GetLogs(ClubID, MobileNumber, Keyword, DateFrom, DateTo, DBName);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
