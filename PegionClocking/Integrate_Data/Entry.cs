using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Integrate_Data
{
    public class Entry
    {
        const string import_Get = "Import_EntryGet";
        const string import_Process = "Import_EntryProcess";
        Integrate_Data.DatabaseConnection dbconn;

        public void EntryImport(string primaryID, string clubID, string action, string fileNotesID)
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
                    dbconn.sqlComm.Parameters.AddWithValue("@EntryID", rows["EntryID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubID", rows["ClubID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceReleasePointID", rows["RaceReleasePointID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryID", rows["RaceCategoryID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroupID", rows["RaceCategoryGroupID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@MemberID", rows["MemberID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@StickerCode", rows["StickerCode"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@BandID", rows["BandID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@CreatedBy", rows["CreatedBy"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateCreated", rows["DateCreated"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@UpdatedBy", rows["UpdatedBy"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateUpdatedBy", rows["DateUpdatedBy"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsActive", rows["IsActive"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Remarks", rows["Remarks"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@EntryBarcodeID", rows["EntryBarcodeID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ExternalID", rows["ExternalID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LatDegreeSimplified", rows["LatDegreeSimplified"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LongDegreeSimplified", rows["LongDegreeSimplified"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@MemberName", rows["MemberName"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@BandNumber", rows["BandNumber"]);
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
