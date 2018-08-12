using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MAVC_IntegrationV2.DAL
{
    public class BandNumberDAL
    {

        #region Variables
        MAVC_IntegrationV2.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public MAVC_IntegrationV2.IntegrationBLL integratedBLL { get; set; }
        #endregion

        public DataSet GetDetails(string bandID)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new MAVC_IntegrationV2.DatabaseConnection("web");
                dbconn.DatabaseConn("BandNumberGetByKey");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@BandID", bandID);

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

        public void DeleteAccount(string accountid)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new MAVC_IntegrationV2.DatabaseConnection("local");
                dbconn.DatabaseConn("Integrate_BandNumberDelete");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@BandID", accountid);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
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
                dbconn.DatabaseConn("Integrate_BandNumber");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@BandID", dt.Rows[0]["BandID"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", dt.Rows[0]["ClubID"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", dt.Rows[0]["MemberID"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@BandNumber", dt.Rows[0]["BandNumber"].ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@Action", action);
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
