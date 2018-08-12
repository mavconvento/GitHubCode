using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Integrate_Data
{
    public class RaceReleasePoint
    {
        const string import_Get = "Import_RaceReleasePointGet";
        const string import_Process = "Import_RaceReleasePointProcess";
        Integrate_Data.DatabaseConnection dbconn;

        public void RaceReleasePointImport(string primaryID, string clubID, string action, string fileNotesID)
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
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubID ,", rows["ClubID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryID ,", rows["RaceScheduleCategoryID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleDetailsID ,", rows["RaceScheduleDetailsID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID ,", rows["RaceReleasePointID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ReleaseTime ,", rows["ReleaseTime ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Multiplier ,", rows["Multiplier ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LapNo ,", rows["LapNo ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@MinSpeed ,", rows["MinSpeed ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsStop ,", rows["IsStop ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@StopFromDate ,", rows["StopFromDate ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@StopFromTime ,", rows["StopFromTime ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@StopToDate ,", rows["StopToDate ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@StopToTime ,", rows["StopToTime ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@StopTime ,", rows["StopTime ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Description ,", rows["Description ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsActive ,", rows["IsActive ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Createdby ,", rows["Createdby ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateCreated ,", rows["DateCreated ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@UpdatedBy ,", rows["UpdatedBy ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateUpdated ,", rows["DateUpdated ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ExternalID ,", rows["ExternalID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateRelease ,", rows["DateRelease ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LocationName ,", rows["LocationName ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LocationID ,", rows["LocationID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RacescheduleID ,", rows["RacescheduleID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RacescheduleName ,", rows["RacescheduleName ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LatDegree ,", rows["LatDegree ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LatMinutes ,", rows["LatMinutes ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LatSecond ,", rows["LatSecond ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LatSign ,", rows["LatSign ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LatDegreeSimplified ,", rows["LatDegreeSimplified ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LongDegree ,", rows["LongDegree ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LongMinutes ,", rows["LongMinutes ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LongSecond ,", rows["LongSecond ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LongSign ,", rows["LongSign ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LongDegreeSimplified", rows["LongDegreeSimplified"]);
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
