using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.PigeonIDSystem
{
    public class Member
    {
        #region Constant
        #endregion

        #region Variables
        DataAccess.DatabaseConnection dbconn;
        #endregion

        public DataSet MemberSave(string dbSource, string MemberIDNo, string MemberName,Int64 PigeonID, string BandNumber,string Sex, string Color,Byte[] Photo)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("MemberSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberName", MemberName);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonID);
                dbconn.sqlComm.Parameters.AddWithValue("@BandNumber", BandNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Sex", Sex);
                dbconn.sqlComm.Parameters.AddWithValue("@Color", Color);
                dbconn.sqlComm.Parameters.AddWithValue("@Photo", Photo);

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
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }
        public DataSet GetPigeonDetails(string dbSource,Int64 PigeonID)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("GetPigeonDetails");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonID);

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
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }

        public void DeletePigeon(string dbSource, Int64 PigeonID)
        {
            try
            {
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("DeletePigeon");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }

        public DataSet GetAllPigeonDetails(string dbSource, string MemberIDNo)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("GetAllPigeonDetails");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberIDNo);

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
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }

        public DataSet GetMemberDetails(string dbSource, string MemberIDNo)
        {
            try
            {
                DataSet dtResult = new DataSet();
                dbconn = new DatabaseConnection(dbSource);
                dbconn.DatabaseConn("GetMemberDetails");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberIDNo);

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
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }
        }
    }
}
