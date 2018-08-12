using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Integrate_Data
{
    public class RaceScheduleDetails
    {
        const string import_Get = "Import_RaceScheduleDetailsGet";
        const string import_Process = "Import_RaceScheduleDetailsProcess";
        Integrate_Data.DatabaseConnection dbconn;

        public void RaceScheduleDetailsImport(string primaryID, string clubID, string action, string fileNotesID)
        {
            try
            {
                switch (action)
                {
                    case "Insert":
                        ProcessDetails(primaryID, action, GetDetails(primaryID).Tables[0].Rows[0]); break;
                    case "Update":
                        ProcessDetails(primaryID, action, GetDetails(primaryID).Tables[0].Rows[0]); break;
                    case "Delete":
                        ProcessDetails(primaryID, action); break;
                    default:
                        break;
                }

                //update filenotes which is already imported
                UpdateFilenotes(fileNotesID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private DataSet GetDetails(string Index)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(import_Get, "_web");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();

                SqlDataAdapter da = new SqlDataAdapter();
                dbconn.sqlComm.Parameters.AddWithValue("@Index", Index);
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

        private void ProcessDetails(string Index, string Action, DataRow rows = null)
        {
            try
            {
                //DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(import_Process, "");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@Index", Index);
                dbconn.sqlComm.Parameters.AddWithValue("@Action", Action);
                if (Action != "Delete")
                {
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleDetailsID", rows["RaceScheduleDetailsID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubID", rows["ClubID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleID", rows["RaceScheduleID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LocationID", rows["LocationID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Loading", rows["Loading"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LoadingTimeFrom", rows["LoadingTimeFrom"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LoadingTimeTo", rows["LoadingTimeTo"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateRelease", rows["DateRelease"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Createby", rows["Createby"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Datecreated", rows["Datecreated"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@UpdatedBy", rows["UpdatedBy"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateUpdated", rows["DateUpdated"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ExternalID", rows["ExternalID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LocationName", rows["LocationName"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ScheduleName", rows["ScheduleName"]);
                }
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateFilenotes(string fileNotesID)
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("UpdateFileNotes", "_web");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@FileNotesID", fileNotesID);

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
