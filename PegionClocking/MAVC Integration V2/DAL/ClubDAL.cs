using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MAVC_IntegrationV2.DAL
{
    public class ClubDAL
    {

        #region Variables
        MAVC_IntegrationV2.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public MAVC_IntegrationV2.IntegrationBLL integratedBLL { get; set; }
        #endregion

        public DataSet GetDetails(string clubID)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new MAVC_IntegrationV2.DatabaseConnection("web");
                dbconn.DatabaseConn("ClubSearchByKey");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", -1);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", clubID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dtResult);
                dbconn.sqlConn.Close();

                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SaveDetails(DataTable dt,string action)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new MAVC_IntegrationV2.DatabaseConnection("local");
                dbconn.DatabaseConn("Integrate_Club");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", dt.Rows[0]["ClubID"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@ClubAbbreviation", dt.Rows[0]["ClubAbbreviation"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", dt.Rows[0]["ClubName"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DateFounded", dt.Rows[0]["DateFounded"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegree", dt.Rows[0]["DistanceLatDegree"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutes", dt.Rows[0]["DistanceLatMinutes"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSecond", dt.Rows[0]["DistanceLatSecond"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSign", dt.Rows[0]["DistanceLatSign"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegree", dt.Rows[0]["DistanceLongDegree"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutes", dt.Rows[0]["DistanceLongMinutes"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSecond", dt.Rows[0]["DistanceLongSecond"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSign", dt.Rows[0]["DistanceLongSign"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@IsActive", dt.Rows[0]["IsActive"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@Version", dt.Rows[0]["Version"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@DateTimeSource", dt.Rows[0]["DateTimeSource"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@IsBackup", dt.Rows[0]["IsBackup"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@IsMAVCStickerUsed", dt.Rows[0]["IsMAVCStickerUsed"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@LastSubcription", dt.Rows[0]["LastSubcription"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@SubcriptionDate", dt.Rows[0]["SubcriptionDate"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@Action", action);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAccount(string accountid)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new MAVC_IntegrationV2.DatabaseConnection("local");
                dbconn.DatabaseConn("Integrate_ClubDelete");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", accountid);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateFileNotes(string fileNoteID)
        {
            try
            {

                DataSet dtResult = new DataSet();
                dbconn = new MAVC_IntegrationV2.DatabaseConnection("web");
                dbconn.DatabaseConn("UpdateFileNotes");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@FileNoteID", fileNoteID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
