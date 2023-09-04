using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects;

namespace DataLayer
{
    public class EntryDal
    {
        #region Constant
        private const string SP_ENTRYSAVE = "EntrySave";
        private const string SP_ENTRYGETBYRACERELEASEPOINT = "EntryGetByRaceReleasePoint";
        private const string SP_ENTRYDELETE = "EntryDelete";
        #endregion

        #region Variable
        DataLayer.DatabaseConnection dbconn;
        #endregion

        #region Properties
       
        #endregion

        #region Public Methods
        public DataSet EclockEntrySave(DomainObjects.Entry entry)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("EclockEntryGlobalSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", entry.Clubname);
                dbconn.sqlComm.Parameters.AddWithValue("@RFID", entry.RFID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", entry.MemberIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@BirdCategory", entry.RaceCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@DateRelease", entry.ReleaseDate.Year.ToString() + "-" + entry.ReleaseDate.Month.ToString() + "-" + entry.ReleaseDate.Day.ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@RingNumber", entry.RingNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", entry.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@IsCopyLastCategory", entry.IsCopyLastCategory);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
