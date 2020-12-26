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
    public class Login : BaseDAL
    {
        #region Variables
        DAL.DatabaseConnection dbconn;
        #endregion Variables

        public DataSet ValidateLogin(LoginData mLoginData)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultLogins");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@UserName", mLoginData.UserName);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", mLoginData.Password);

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

        public DataSet LoadBalance(String userID)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetCheckLoadBalance");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber",userID);

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

        public DataSet ResetPassword(ForgotPasswordData mForgotPasswordData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ForgotPassword");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", mForgotPasswordData.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", mForgotPasswordData.Password);
                dbconn.sqlComm.Parameters.AddWithValue("@ActionType", mForgotPasswordData.ActionType);
                dbconn.sqlComm.Parameters.AddWithValue("@SecurityCode", mForgotPasswordData.SecurityCode);

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

        public DataSet ChangePassword(ForgotPasswordData mForgotPasswordData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ForgotPassword");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", mForgotPasswordData.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Password", mForgotPasswordData.Password);
                dbconn.sqlComm.Parameters.AddWithValue("@ActionType", mForgotPasswordData.ActionType);
                dbconn.sqlComm.Parameters.AddWithValue("@SecurityCode", mForgotPasswordData.SecurityCode);

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

        public DataSet GetAdvertisement()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetAdvertisement");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();

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
    }
}