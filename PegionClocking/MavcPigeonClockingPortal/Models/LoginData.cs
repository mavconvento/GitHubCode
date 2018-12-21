using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using MavcPigeonClockingPortal.DAL;

namespace MavcPigeonClockingPortal.Models
{
    public class ForgotPasswordData
    {
        public String MobileNumber { get; set; }
        public String Password { get; set; }
        public String ReTypePassword { get; set; }
        public String SecurityCode { get; set; }
        public String ActionType { get; set; }
    }

    public class Advertisement
    {
        public String Description { get; set; }
        public String ImageName { get; set; }
        public Boolean IsProduct { get; set; }
    }

    public class LoginData
    {
        public String UserName { get; set; }
        public String Password { get; set; }

        public DataSet ValidateLogin(LoginData loginData)
        {
            try
            {
                DAL.Login login = new Login();
                return login.ValidateLogin(loginData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
         
        }

        public DataSet ForgotPassword(ForgotPasswordData resetPasswordData)
        {
            DAL.Login login = new Login();
            return login.ResetPassword(resetPasswordData);
        }

        public List<Advertisement> GetAdvertisement()
        {
            var advertisements = new List<Advertisement>();
            DAL.Login login = new Login();
            DataSet dt = login.GetAdvertisement();

            if (dt.Tables.Count > 0)
            {
                foreach (DataRow item in dt.Tables[0].Rows)
                {
                    Advertisement advertisement = new Advertisement();
                    advertisement.Description = LWT.Common.LWTSafeTypes.SafeString(item["Description"]);
                    advertisement.ImageName = LWT.Common.LWTSafeTypes.SafeString(item["ImageName"]);
                    advertisement.IsProduct = LWT.Common.LWTSafeTypes.SafeBool(item["IsProduct"]);
                    advertisements.Add(advertisement);
                }
            }

            return advertisements;
        }
    }
}