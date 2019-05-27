using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Integrate_Data
{
    public class Club
    {
        const string import_Get = "Import_ClubGet";
        const string import_Process = "Import_ClubProcess";
        Integrate_Data.DatabaseConnection dbconn;

        public void ClubImport(string primaryID, string clubID, string action, string fileNotesID)
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
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubAbbreviation ,", rows["ClubAbbreviation ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubName ,", rows["ClubName ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateFounded ,", rows["DateFounded ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegree ,", rows["DistanceLatDegree ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutes ,", rows["DistanceLatMinutes ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSecond ,", rows["DistanceLatSecond ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSign ,", rows["DistanceLatSign ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegree ,", rows["DistanceLongDegree ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutes ,", rows["DistanceLongMinutes ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSecond ,", rows["DistanceLongSecond ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSign ,", rows["DistanceLongSign ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsActive ,", rows["IsActive ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Createby ,", rows["Createby ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateCreated ,", rows["DateCreated ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Updatedby ,", rows["Updatedby ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateUpdated ,", rows["DateUpdated ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Version ,", rows["Version ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateTimeSource ,", rows["DateTimeSource ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsBackup ,", rows["IsBackup ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsMAVCStickerUsed ,", rows["IsMAVCStickerUsed ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ExternalID ,", rows["ExternalID ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LastSubcription ,", rows["LastSubcription ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@SubcriptionDate ,", rows["SubcriptionDate ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsTrial ,", rows["IsTrial ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@TimeZoneDiff ,", rows["TimeZoneDiff ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@SERVER ,", rows["SERVER ,"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@AllowDoubleEntry", rows["AllowDoubleEntry"]);
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
