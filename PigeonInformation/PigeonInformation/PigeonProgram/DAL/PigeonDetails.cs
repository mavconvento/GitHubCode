using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PigeonProgram.BIZ;

namespace PigeonProgram.DAL
{
    public class PigeonDetails
    {
        #region Constant
        #endregion

        #region Variable
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public BIZ.PigeonDetails PigeonDetail { get; set; }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        public DataSet PigeonDetailsSave()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("PigeonDetailsSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonName", PigeonDetail.PigeonName);
                dbconn.sqlComm.Parameters.AddWithValue("@BandNumber", PigeonDetail.BandNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Gender", PigeonDetail.Gender);
                dbconn.sqlComm.Parameters.AddWithValue("@EyeColor", PigeonDetail.EyeColor);
                dbconn.sqlComm.Parameters.AddWithValue("@Color", PigeonDetail.Color);
                dbconn.sqlComm.Parameters.AddWithValue("@Line", PigeonDetail.Line);
                dbconn.sqlComm.Parameters.AddWithValue("@Owner", PigeonDetail.Owner);
                dbconn.sqlComm.Parameters.AddWithValue("@Achievement", PigeonDetail.Achievement);
                dbconn.sqlComm.Parameters.AddWithValue("@Picture", PigeonDetail.Picture);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", PigeonDetail.UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@IsUnknownDate", PigeonDetail.IsUnknown);
                dbconn.sqlComm.Parameters.AddWithValue("@HatchAcquiredDate", PigeonDetail.HatchDate);
                dbconn.sqlComm.Parameters.AddWithValue("@Year", PigeonDetail.Year);
                dbconn.sqlComm.Parameters.AddWithValue("@Season", PigeonDetail.Season);
                dbconn.sqlComm.Parameters.AddWithValue("@Category", PigeonDetail.Category);
                dbconn.sqlComm.Parameters.AddWithValue("@ParentCock", PigeonDetail.ParentCock);
                dbconn.sqlComm.Parameters.AddWithValue("@ParentHen", PigeonDetail.ParentHen);
                dbconn.sqlComm.Parameters.AddWithValue("@TypeOfBreeding", PigeonDetail.TypeofBreeding);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonType", PigeonDetail.PigeonType);
                dbconn.sqlComm.Parameters.AddWithValue("@RfidTag", PigeonDetail.RFIDTag);
                dbconn.sqlComm.Parameters.AddWithValue("@BackColor", PigeonDetail.BackColor);

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
        }
        public DataSet GetPedigreeSetup()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetPedigreeSetup");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();

                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID",  PigeonDetail.UserID);

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
        }
        public DataSet GetPigeonList(BIZ.PigeonDetails pigeonDetails)
        {
            try
            {
                 DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("PigeonDetailsGetAll");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();

                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", pigeonDetails.UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", pigeonDetails.PigeonID);
                dbconn.sqlComm.Parameters.AddWithValue("@Gender", pigeonDetails.FilterGender);
                dbconn.sqlComm.Parameters.AddWithValue("@Year", pigeonDetails.FilterYear);
                dbconn.sqlComm.Parameters.AddWithValue("@Breed", pigeonDetails.FilterBreed);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonName", pigeonDetails.FilterPigeonName);

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
        }
        public DataSet GetPigeonByGender(BIZ.PigeonDetails pigeonDetails)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetPigeonList");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", pigeonDetails.UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@Gender", pigeonDetails.Gender);

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
        }
        public DataSet PigeonDetailsGetByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("PigeonDetailsGetByKey");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);
                dbconn.sqlComm.Parameters.AddWithValue("@BandNumber", PigeonDetail.BandNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@RfidTag", PigeonDetail.RFIDTag);

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
        }
        public DataSet GetPigeonOffspring()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetOffSpring");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);

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
        }
        public DataSet GetPigeonPedigree()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("Pedigree");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);
                dbconn.sqlComm.Parameters.AddWithValue("@IsFourthGen", PigeonDetail.IsFourthGen);

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
        }
        public DataSet PigeonDetailsDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("PigeonDetailsDelete");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);

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
        }
        public DataSet PigeonDetailsGetAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("PigeonDetailsGetAll");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
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
        }
        public DataSet RaceResultGetAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultGetAll");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);

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
        }
        public DataSet RaceResultGetByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultGetByKey");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RaceResultID", PigeonDetail.RaceResultID);

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
        }
        public DataSet RaceResultDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultDelete");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RaceResultID", PigeonDetail.RaceResultID);

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
        }
        public DataSet RaceResultSave()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RaceResultSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RaceResultID", PigeonDetail.RaceResultID);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleasePoint", PigeonDetail.ReleasePoint);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", PigeonDetail.ReleaseDate);
                dbconn.sqlComm.Parameters.AddWithValue("@WeatherCondition", PigeonDetail.WeatherCondition);
                dbconn.sqlComm.Parameters.AddWithValue("@BirdEntry", PigeonDetail.BirdEntry);
                dbconn.sqlComm.Parameters.AddWithValue("@BirdClock", PigeonDetail.BirdClock);
                dbconn.sqlComm.Parameters.AddWithValue("@Rank", PigeonDetail.Rank);
                dbconn.sqlComm.Parameters.AddWithValue("@Distance", PigeonDetail.Distance);
                dbconn.sqlComm.Parameters.AddWithValue("@Flight", PigeonDetail.Flight);
                dbconn.sqlComm.Parameters.AddWithValue("@Speed", PigeonDetail.Speed);
                dbconn.sqlComm.Parameters.AddWithValue("@Remarks", PigeonDetail.Remarks);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", PigeonDetail.UserID);
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
        }
        public DataSet TreatmentGetAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TreatmentGetAll");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);

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
        }
        public DataSet TreatmentGetByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TreatmentGetByKey");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@TreatmentID", PigeonDetail.TreatmentID);

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
        }
        public DataSet TreatmentDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TreatmentDelete");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@TreatmentID", PigeonDetail.TreatmentID);

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
        }
        public DataSet TreatmentSave()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TreatmentSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@TreatMentID", PigeonDetail.TreatmentID);
                dbconn.sqlComm.Parameters.AddWithValue("@PigeonID", PigeonDetail.PigeonID);
                dbconn.sqlComm.Parameters.AddWithValue("@Treatment", PigeonDetail.Treatment);
                dbconn.sqlComm.Parameters.AddWithValue("@TreatmentDate", PigeonDetail.TreatmentDate);
                dbconn.sqlComm.Parameters.AddWithValue("@Illness", PigeonDetail.Illness);
                dbconn.sqlComm.Parameters.AddWithValue("@Remarks", PigeonDetail.Remarks);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", PigeonDetail.UserID);
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
        }
    }
}
