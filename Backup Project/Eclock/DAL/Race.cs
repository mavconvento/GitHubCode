using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Eclock.BIZ;
using System.Data.SqlClient;

namespace Eclock.DAL
{
    public class Race
    {
        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        public DataSet GetArrivalTime(BIZ.Race bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_GetArrivalTime", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SerialRFIDNo", bizData.SerialRFIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", bizData.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSActivated", bizData.SMSActivated);

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

        public Boolean SubmitRaceResult(BIZ.Race bizData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Eclock_RaceResultSave", "_webDB");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SerialRFIDNo", bizData.SerialRFIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@ArrivalTime", bizData.ArrivalTime);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", bizData.RaceReleasePointID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", bizData.MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", bizData.ClubID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
