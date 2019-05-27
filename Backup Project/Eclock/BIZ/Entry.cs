using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Eclock.BIZ
{
    public class Entry
    {
        #region Variable
        DAL.Entry DalEntry;
        DAL.RegisterRFID DalRegisterRFID;
        #endregion

        #region Properties
        public Int64 EclockEntryID { get; set; }
        public Int64 ClubID { get; set; }
        public String MemberIDNo { get; set; }
        public Int64 MemberID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Int64 ReleasepointID { get; set; }
        public Int64 BandID { get; set; }
        public String BandNumber { get; set; }
        public String RFIDSerialNo { get; set; }
        public Int64 MemberRFIDRegisterID { get; set; }
        #endregion

        #region Public Methods
        public DataSet GetReleasePointDetails()
        {
            try
            {
                DalEntry = new DAL.Entry();
                return DalEntry.GetReleasePointDetails(this);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
        public DataSet VerifyRFID()
        {
            try
            {
                DalEntry = new DAL.Entry();
                return DalEntry.VerifyRFID(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public DataSet GetMemberEclockEntry()
        {
            try
            {
                DalEntry = new DAL.Entry();
                return DalEntry.GetMemberEclockEntry(this);
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
                DalEntry = new DAL.Entry();
                return DalEntry.Save(this);
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
                DalEntry = new DAL.Entry();
                return DalEntry.Delete(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
    }
}
