using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class Member
    {
        #region Constants
        private const string SP_MEMBERSAVE = "MemberSave";
        private const string SP_MEMBERLIST = "MemberDetailsSelectAll";
        private const string SP_MEMBERSEARCHBYKEYS = "MemberDetailsSearchByKey";
        private const string SP_MEMBERRINGBYKEYS = "MemberRingEnrolledGetByKey";
        private const string SP_MEMBERDETAILSDELETE = "MemberDetailsDelete";
        private const string SP_MEMBERRINGDELETE = "MemberRingEnrolledDelete";
        private const string SP_MEMBERRINGSAVE = "MemberRingEnrolledSave";
        #endregion

        #region Variables
        DAL.DatabaseConnection dbconn;
        #endregion

        #region Properties
        public String ID { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 BandID { get; set; }
        public string RingNumber { get; set; }
        public string RaceScheduleName { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public string MemberIDNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtensionName { get; set; }
        public string LoftName { get; set; }
        public string Address { get; set; }
        public DateTime DateofBirth { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSeconds { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSeconds { get; set; }
        public String DistanceLongSign { get; set; }
        public DateTime DateofMembership { get; set; }
        public DateTime LastRenewalDate { get; set; }
        public DateTime DateofExpiration { get; set; }
        public Boolean DeactivateMember { get; set; }

        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Boolean IsUploaded { get; set; }
        public Int64 LocationID { get; set; }

        public string Type { get; set; }
        public String ReaderID { get; set; }
        public String ClubName { get; set; }
        public Boolean Overwrite { get; set; }
        #endregion

        #region Public Methods
        public DataSet UnregMobileNumber(string ClubID, String mobileNumber, string UserID, string keyword)
        {
            try
            {
                DAL.Member common = new DAL.Member();
                return common.WebClockingSave(ClubID, mobileNumber, keyword, "Unreg", UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet WebClockingSave(String ClubID, String SMSMobileNumber, String Keyword, String Action, string UserID = "", String SMSMobileNumberTo = "")
        {

            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("WebClockingSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSMobileNumber", SMSMobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@SMSMobileNumberTo", SMSMobileNumberTo);
                dbconn.sqlComm.Parameters.AddWithValue("@StickerCode", Keyword);
                dbconn.sqlComm.Parameters.AddWithValue("@Action", Action);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@RequestAction", "Mobile");
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

        public DataSet Save()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_MEMBERSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@LastName", LastName);
                dbconn.sqlComm.Parameters.AddWithValue("@FirstName", FirstName);
                dbconn.sqlComm.Parameters.AddWithValue("@MiddleName", MiddleName);
                dbconn.sqlComm.Parameters.AddWithValue("@ExtensionName", ExtensionName);
                dbconn.sqlComm.Parameters.AddWithValue("@LoftName", LoftName);
                dbconn.sqlComm.Parameters.AddWithValue("@Address", Address);
                dbconn.sqlComm.Parameters.AddWithValue("@DateofBirth", DateofBirth);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegree", DistanceLatDegree);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutes", DistanceLatMinutes);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSeconds", DistanceLatSeconds);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSign", DistanceLatSign);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegree", DistanceLongDegree);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutes", DistanceLongMinutes);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSeconds", DistanceLongSeconds);
                dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSign", DistanceLongSign);
                dbconn.sqlComm.Parameters.AddWithValue("@DateofMembership", DateofMembership);
                dbconn.sqlComm.Parameters.AddWithValue("@LastRenewalDate", LastRenewalDate);
                dbconn.sqlComm.Parameters.AddWithValue("@DateofExpiration", DateofExpiration);
                dbconn.sqlComm.Parameters.AddWithValue("@IsUpload", IsUploaded);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@DeactivateMember", DeactivateMember);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
                //dbconn.sqlComm.ExecuteNonQuery();
                //dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MemberRingSave()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_MEMBERRINGSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberRingEnrolledID", BandID);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleName", RaceScheduleName);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceScheduleCategoryName", RaceScheduleCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@RingNumber", RingNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegisterRFIDSave()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("RegisterRFIDSave","_Eclock");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@RFIDSerialNo", ReaderID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@Overwrite", Overwrite);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReaderRegistrationSave()
        {
            try
            {
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("ReaderRegistrationSave", "_Eclock");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", ClubName );
                dbconn.sqlComm.Parameters.AddWithValue("@AssignedTo", Name);
                dbconn.sqlComm.Parameters.AddWithValue("@ReaderID", ReaderID);
                dbconn.sqlComm.Parameters.AddWithValue("@Type", Type);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
                //return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet MemberDetailsSelectAll()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_MEMBERLIST);
              
                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberName", Name);
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", MobileNumber);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlComm.Connection.Close();
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet MemberDetailsSearchByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_MEMBERSEARCHBYKEYS);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberIDNo);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = dbconn.sqlComm;
                da.Fill(dataResult);
                dbconn.sqlComm.Connection.Close();
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetMemberDistance()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("MemberDistance");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", MemberIDNo);

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
        public DataSet MemberRingSearchByKey()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_MEMBERRINGBYKEYS);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);
                dbconn.sqlComm.Parameters.AddWithValue("@ScheduleName", RaceScheduleName);
                dbconn.sqlComm.Parameters.AddWithValue("@ScheduleCategoryName", RaceScheduleCategoryName);

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
        public void MemberDetailsDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_MEMBERDETAILSDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ID", ID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MemberRingDelete()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_MEMBERRINGDELETE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@BandID", BandID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@UserID", UserID);
                dbconn.sqlComm.ExecuteNonQuery();
                dbconn.sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetMembetDistancePerReleasePoint()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetMemberDistanceByReleasePoint");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.CommandTimeout = 0;
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@LocationID", LocationID);

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
        #endregion
    }
}
