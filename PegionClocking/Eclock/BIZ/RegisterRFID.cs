using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Eclock.BIZ
{
    public class RegisterRFID
    {

        #region Variable
        DAL.RegisterRFID DalRegisterRFID;
        #endregion

        #region Properties
        public Int64 MemberRFIDRegistrationID { get; set; }
        public String MemberIDNo { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 MemberID { get; set; }
        public String Season { get; set; } //Schesule Name
        public String BandNumber { get; set; }
        public Int64 BandID { get; set; }
        public String RFID { get; set; }
        public String Type { get; set; }
        public byte[] Picture { get; set; }
        public String BirdCategory { get; set; }
        #endregion

        #region Public Methods
        public DataSet GetMemberDetails()
        {
            try
            {
                DalRegisterRFID = new DAL.RegisterRFID();
                return DalRegisterRFID.GetMemberDetails(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetSeason()
        {
            try
            {
                DalRegisterRFID = new DAL.RegisterRFID();
                return DalRegisterRFID.GetSeason(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetMemberRFIDRegisterGetByID()
        {
            try
            {
                DalRegisterRFID = new DAL.RegisterRFID();
                return DalRegisterRFID.GetMemberRFIDRegisterGetByID(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    
        public Boolean Save()
        {
            try
            {
                DalRegisterRFID = new DAL.RegisterRFID();
                return DalRegisterRFID.Save(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Boolean Delete()
        {
            try
            {
                DalRegisterRFID = new DAL.RegisterRFID();
                return DalRegisterRFID.Delete(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}
