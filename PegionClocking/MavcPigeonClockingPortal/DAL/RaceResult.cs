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
    public class RaceResult : BaseDAL
    {

        #region Variables
        DAL.DatabaseConnection dbconn;
        #endregion Variables

        public DataSet GetClubList(String UserID)
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ClubSelectAll");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@UserName", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@NotExpired",  1);
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

        public DataSet GetBirdCategory(String ClubID = "")
        {
            try
            {

                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceCategorySelectAll", ClubID);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReturnAll", 0);

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

        public DataSet GetGroupCategory(String ClubID = "")
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceCategoryGroupSelectAll", ClubID);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReturnAll", 0);

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
        
        public DataSet GetRaceResult(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultGetbyKey", ClubID);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID",  0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate",  ReleaseDate);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory",  BirdCategory);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", RaceCategory);
                dbconn.sqlComm.Parameters.AddWithValue("@Version",  0);
                dbconn.sqlComm.Parameters.AddWithValue("@Name",  SearchName);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromWeb", 1);

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

        public DataSet GetRaceDetails(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName, String Sender)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceDetailsGetbyKeys",ClubID);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", BirdCategory);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", RaceCategory);
                dbconn.sqlComm.Parameters.AddWithValue("@Version", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@Name", SearchName);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromWeb", 1);
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", Sender);

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

        public DataSet GetRaceEntry(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName,String Sender,String Source = "")
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceDetailsGetbyKeys",ClubID);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategory", BirdCategory);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCategoryGroup", RaceCategory);
                dbconn.sqlComm.Parameters.AddWithValue("@Version", 0);
                dbconn.sqlComm.Parameters.AddWithValue("@Name", SearchName);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFromWeb", 1);
                dbconn.sqlComm.Parameters.AddWithValue("@Sender", Sender);
                dbconn.sqlComm.Parameters.AddWithValue("@Source", Source);

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

        public DataSet SendSticker(string ClubID, String mobileNumber, string keyword)
        {
            try
            {
                DAL.Common common = new DAL.Common();
                return common.WebClockingSave(ClubID, mobileNumber, keyword, "Sticker");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Forecast(string ClubID, String mobileNumber, string keyword)
        {
            try
            {
                DAL.Common common = new DAL.Common();
                return common.WebClockingSave(ClubID, mobileNumber, keyword, "Forecast");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}