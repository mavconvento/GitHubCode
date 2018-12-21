using System;
using System.Collections.Generic;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using MavcPigeonClockingPortal.Constants;
using MavcPigeonClockingPortal.Models;

//--common dll
using LWT.Common.DAL;
using LWT.Common;

namespace MavcPigeonClockingPortal.DAL
{
    public class MyProfile :BaseDAL
    {

        #region Variables
        DAL.DatabaseConnection dbconn;
        #endregion Variables

        public DataSet GetRegisteredMobileList(String mobileNumber)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetMobileNumberRegisteredList");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@mobilenumber", mobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@RequestFrom", "pilipinaskalapati");

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
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }

        }

        public DataSet GetMemberDistance(string ClubID, String MemberID)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("MemberDistance",ClubID);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromPilipinasKalapati", true);

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
            finally
            {
                dbconn.sqlConn.Close();
                dbconn.sqlConn.Dispose();
                SqlConnection.ClearPool(dbconn.sqlConn);
            }

        }

        public DataSet LoadMavcCard(string ClubID, String mobileNumber,string keyword)
        {
            try
            {

                 DAL.Common common = new DAL.Common();
                 return common.WebClockingSave(ClubID, mobileNumber, keyword, "Load");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet Pasaload(string mobileNumberFrom, String mobileNumberTo, string Amount)
        {
            try
            {
                DAL.Common common = new DAL.Common();
                return common.WebClockingSave("", mobileNumberFrom, Amount, "Pasaload", mobileNumberTo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet UnregMobileNumber(string ClubID, String mobileNumber,string UserID, string keyword)
        {
            try
            {
                DAL.Common common = new DAL.Common();
                return common.WebClockingSave(ClubID, mobileNumber, keyword, "Unreg", UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}