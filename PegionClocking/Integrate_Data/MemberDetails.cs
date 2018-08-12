using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Integrate_Data
{
    public class MemberDetails
    {
        const string import_Get = "Import_MemberDetailsGet";
        const string import_Process = "Import_MemberDetailsProcess";
        Integrate_Data.DatabaseConnection dbconn;

        public void MemberDetailsImport(string primaryID, string clubID, string action, string fileNotesID)
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
                    dbconn.sqlComm.Parameters.AddWithValue("@ClubID", rows["ClubID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@MemberID", rows["MemberID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", rows["MemberIDNo"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LastName", rows["LastName"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@FirstName", rows["FirstName"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@MiddleName", rows["MiddleName"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ExtensionName", rows["ExtensionName"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LoftName", rows["LoftName"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Address", rows["Address"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateofBirth", rows["DateofBirth"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegree", rows["DistanceLatDegree"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutes", rows["DistanceLatMinutes"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSeconds", rows["DistanceLatSeconds"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSign", rows["DistanceLatSign"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegree", rows["DistanceLongDegree"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutes", rows["DistanceLongMinutes"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSeconds", rows["DistanceLongSeconds"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSign", rows["DistanceLongSign"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateofMembership", rows["DateofMembership"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LastRenewalDate", rows["LastRenewalDate"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateofExpiration", rows["DateofExpiration"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@IsActive", rows["IsActive"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@CreatedBy", rows["CreatedBy"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@DateCreated", rows["DateCreated"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LastDateUpdated", rows["LastDateUpdated"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@Updatedby", rows["Updatedby"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@ExternalID", rows["ExternalID"]);
                    dbconn.sqlComm.Parameters.AddWithValue("@LatDegreeSimplified", rows["LatDegreeSimplified"]);
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
