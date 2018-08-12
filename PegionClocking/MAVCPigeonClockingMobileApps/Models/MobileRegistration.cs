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
    public class MobileRegistration
    {
        public String ClubName { get; set; }
        public String MobileNumber { get; set; }
        public String MemberID { get; set; }
        public String Password { get; set; }
        public String ActivationCode { get; set; }
        public int Step { get; set; }

        public String ResultMessage { get; set; }
        public String MobileRegistrationID { get; set; }
        public String Status { get; set; }

        public MobileRegistration MobileRegistrationStep1()
        {
            try
            {
                DAL.MobileRegistrationDAL mobileRegistrationDAL = new DAL.MobileRegistrationDAL();
                DataSet dsResult = mobileRegistrationDAL.MobileRegistrationStep1(this.ClubName, this.MemberID, this.MobileNumber, "1");

                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        this.MobileRegistrationID = dsResult.Tables[0].Rows[0]["MobileRegistrationID"].ToString();
                        this.Status = dsResult.Tables[0].Rows[0]["ResultMessage"].ToString();
                    }
                }
                return this;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public MobileRegistration MobileRegistrationStep2()
        {
            try
            {
                DAL.MobileRegistrationDAL mobileRegistrationDAL = new DAL.MobileRegistrationDAL();
                DataSet dsResult = mobileRegistrationDAL.MobileRegistrationStep2(this.Password,this.MobileNumber, "2");
                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        this.Status = dsResult.Tables[0].Rows[0]["ResultMessage"].ToString();
                    }
                }
                return this;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MobileRegistration MobileRegistrationStep3()
        {
            try
            {
                DAL.MobileRegistrationDAL mobileRegistrationDAL = new DAL.MobileRegistrationDAL();
                DataSet dsResult = mobileRegistrationDAL.MobileRegistrationStep3(this.ActivationCode, this.MobileNumber, "3");
                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        this.Status = dsResult.Tables[0].Rows[0]["ResultMessage"].ToString();
                    }
                }
                return this;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}