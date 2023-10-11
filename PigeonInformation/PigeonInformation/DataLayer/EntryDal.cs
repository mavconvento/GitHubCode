using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects;

namespace DataLayer
{
    public class EntryDal
    {
        #region Constant
        private const string SP_ENTRYSAVE = "EntrySave";
        private const string SP_ENTRYGETBYRACERELEASEPOINT = "EntryGetByRaceReleasePoint";
        private const string SP_ENTRYDELETE = "EntryDelete";
        #endregion

        #region Variable
        DataLayer.DatabaseConnection dbconn;
        #endregion

        #region Properties

        #endregion

        #region Public Methods
        public DataSet EclockEntrySave(DomainObjects.Entry entry)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("EclockEntryGlobalSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", entry.Clubname);
                dbconn.sqlComm.Parameters.AddWithValue("@RFID", entry.RFID);
                dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", entry.MemberIDNo);
                dbconn.sqlComm.Parameters.AddWithValue("@BirdCategory", entry.RaceCategoryName);
                dbconn.sqlComm.Parameters.AddWithValue("@DateRelease", entry.ReleaseDate.Year.ToString() + "-" + entry.ReleaseDate.Month.ToString() + "-" + entry.ReleaseDate.Day.ToString());
                dbconn.sqlComm.Parameters.AddWithValue("@RingNumber", entry.RingNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", entry.MobileNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@IsCopyLastCategory", entry.IsCopyLastCategory);

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

        public DataSet GetTopPigeonPigRaceData(string ClockID, DateTime LiberDate, string RaceCode)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetTopPigeonPigRaceData");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClockID", ClockID);
                dbconn.sqlComm.Parameters.AddWithValue("@LiberDate", LiberDate);
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCode", RaceCode);

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

        public DataSet GetRaceCode(string ClubId, DateTime LiberDate)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetRaceCode");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubId", ClubId);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", LiberDate);

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


        public DataSet TopPigeonPigRaceDataSave(DomainObjects.TopPigeonPigRaceData raceData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TopPigeonPigRaceDataSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", raceData.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@LiberCode", raceData.LiberCode);
                dbconn.sqlComm.Parameters.AddWithValue("@LiberDate", raceData.LiberDate);
                dbconn.sqlComm.Parameters.AddWithValue("@PRingNo", raceData.PRingNo);
                //dbconn.sqlComm.Parameters.AddWithValue("@BackTime", raceData.BackTime);
                //dbconn.sqlComm.Parameters.AddWithValue("@FlyTime", raceData.FlyTime);
                //dbconn.sqlComm.Parameters.AddWithValue("@FlySpeed", raceData.FlySpeed);
                //dbconn.sqlComm.Parameters.AddWithValue("@Dist", raceData.Dist);
                dbconn.sqlComm.Parameters.AddWithValue("@RandomCode", raceData.RandomCode);
                dbconn.sqlComm.Parameters.AddWithValue("@ClockID", raceData.ClockID);
                //dbconn.sqlComm.Parameters.AddWithValue("@UID_Real", raceData.UID_Real);
                //dbconn.sqlComm.Parameters.AddWithValue("@TimeVari", raceData.TimeVari);
                dbconn.sqlComm.Parameters.AddWithValue("@MarkedTime", raceData.MarkedTime);
                //dbconn.sqlComm.Parameters.AddWithValue("@RealRandom", raceData.RealRandom);
                //dbconn.sqlComm.Parameters.AddWithValue("@BackLon", raceData.BackLon);
                //dbconn.sqlComm.Parameters.AddWithValue("@BackLat", raceData.BackLat);
                dbconn.sqlComm.ExecuteNonQuery();

                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetTopPigeonRaceCode()
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GetTopPigeonRaceCode");

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
        }

        public DataSet TopPigeonRaceCodeSave(DomainObjects.TopPigeonRaceCode race)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TopPigeonRaceCodeSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RaceCode", race.RaceCode);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubID", race.ClubID);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", race.ClubName);
                dbconn.sqlComm.Parameters.AddWithValue("@ReleaseDate", race.ReleaseDate);
                dbconn.sqlComm.Parameters.AddWithValue("@LastBackTime", race.LastBackTime);
                dbconn.sqlComm.Parameters.AddWithValue("@action", race.Action);
                dbconn.sqlComm.ExecuteNonQuery();

                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEntryList(string memberId, DateTime liberDate)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT t.*,IFNULL(d.OtherClub,1) IsRegistered,d.E_Ring FROM tec_pigracedata t
                                left join tec_pigdata d on t.PringNo = d.PringNo
                                where d.loftNo = @memberId and  t.LiberDate = @liberDate;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@liberDate"; dr["value"] = liberDate.ToShortDateString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@memberId"; dr["value"] = memberId; param.Rows.Add(dr);
                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetRaceCode(DateTime liberDate)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT * FROM tec_racedata t WHERE LIBERDATE = @liberDate ORDER BY UPDATETIME DESC;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@liberDate"; dr["value"] = liberDate.ToShortDateString(); param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "@memberId"; dr["value"] = memberId; param.Rows.Add(dr);
                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEntryListByDate(DateTime liberDate,string libercode)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT t.*,IFNULL(d.OtherClub,1) IsRegistered,d.E_Ring,d.loftno FROM tec_pigracedata t
                                left join tec_pigdata d on t.PringNo = d.PringNo
                                where t.LiberDate = @liberDate and LiberCode = @liberCode;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@liberDate"; dr["value"] = liberDate.ToShortDateString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@liberCode"; dr["value"] = libercode; param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetEntryListByDateAndClockID(DateTime liberDate, string libercode, string clockid)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT t.*,IFNULL(d.OtherClub,1) IsRegistered,d.E_Ring,d.loftno FROM tec_pigracedata t
                                left join tec_pigdata d on t.PringNo = d.PringNo
                                where ClockID = @ClockId and t.LiberDate = @liberDate and LiberCode = @liberCode;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@liberDate"; dr["value"] = liberDate.ToShortDateString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@liberCode"; dr["value"] = libercode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@ClockId"; dr["value"] = clockid; param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
