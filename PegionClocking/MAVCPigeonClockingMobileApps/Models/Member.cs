using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using MAVCPigeonClockingMobileApps;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace MAVCPigeonClockingMobileApps.Models
{
    public class Member
    {
        public string Page { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }

        DAL.MemberDAL oMemberDAL;

        public DataSet AuthenticateLogin(string _username, string _password, string _page)
        {
            oMemberDAL = new DAL.MemberDAL();
            this.UserName = _username;
            this.Password = _password;
            this.Page = _page;
            try
            {
                return oMemberDAL.ValidateLogin(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}