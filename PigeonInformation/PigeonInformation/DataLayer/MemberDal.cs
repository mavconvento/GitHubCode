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
    public class MemberDal
    {
        #region Constant
        private const string SP_ENTRYSAVE = "EntrySave";
        private const string SP_ENTRYGETBYRACERELEASEPOINT = "EntryGetByRaceReleasePoint";
        private const string SP_ENTRYDELETE = "EntryDelete";

        #endregion

        #region Variable
        #endregion

        #region Properties

        #endregion

        #region Public Methods
        //public DataSet EclockEntrySave(DomainObjects.Entry entry)
        //{
        //    try
        //    {
        //        DataSet dataResult = new DataSet();
        //        dbconn = new DatabaseConnection();
        //        dbconn.DatabaseConn("EclockEntryGlobalSave");

        //        if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
        //        dbconn.sqlConn.Open();
        //        dbconn.sqlComm.Parameters.Clear();
        //        dbconn.sqlComm.Parameters.AddWithValue("@ClubName", entry.Clubname);
        //        dbconn.sqlComm.Parameters.AddWithValue("@RFID", entry.RFID);
        //        dbconn.sqlComm.Parameters.AddWithValue("@MemberIDNo", entry.MemberIDNo);
        //        dbconn.sqlComm.Parameters.AddWithValue("@BirdCategory", entry.RaceCategoryName);
        //        dbconn.sqlComm.Parameters.AddWithValue("@DateRelease", entry.ReleaseDate.Year.ToString() + "-" + entry.ReleaseDate.Month.ToString() + "-" + entry.ReleaseDate.Day.ToString());
        //        dbconn.sqlComm.Parameters.AddWithValue("@RingNumber", entry.RingNumber);
        //        dbconn.sqlComm.Parameters.AddWithValue("@MobileNumber", entry.MobileNumber);
        //        dbconn.sqlComm.Parameters.AddWithValue("@IsCopyLastCategory", entry.IsCopyLastCategory);

        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = dbconn.sqlComm;
        //        da.Fill(dataResult);
        //        dbconn.sqlConn.Close();
        //        return dataResult;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public DataTable GetMemberInfo(string memberid)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"select ifNull(t.ClockID,"""") ClockId,IFNULL(d.IsMultipleClub,0) IsMultiple,d.* FROM tec_loftclocks t
                                    left join tec_loftdata d on t.loftno = d.loftno
                                where t.lastsynchdate = (select max(lastsynchdate) from tec_loftclocks c
                                where c.loftno = t.loftno)
                                and d.loftNo = @memberid";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@memberid";
                dr["value"] = memberid;

                param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetMemberWithMultipleClub()
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"select ifNull(t.ClockID,"""") ClockId,IFNULL(d.IsMultipleClub,0) IsMultiple,d.* FROM tec_loftclocks t
                                    left join tec_loftdata d on t.loftno = d.loftno
                                where t.lastsynchdate = (select max(lastsynchdate) from tec_loftclocks c
                                where c.loftno = t.loftno)
                                and IsMultipleClub = @IsMultipleClub";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@IsMultipleClub";
                dr["value"] = 1;

                param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void PigeonSave(string pigeonId,string memberId, bool isRegistered, string ering)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"Update tec_pigdata set OtherClub = @isRegistered,E_ring = @Ering where PRingNo = @pigeonId and loftNo = @memberId;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@pigeonId";
                dr["value"] = pigeonId;
                param.Rows.Add(dr);

                dr = param.NewRow();
                dr["key"] = "@memberId";
                dr["value"] = memberId;
                param.Rows.Add(dr);

                dr = param.NewRow();
                dr["key"] = "@Ering ";
                dr["value"] = ering;
                param.Rows.Add(dr);

                dr = param.NewRow();
                dr["key"] = "@isRegistered";

                if (isRegistered) dr["value"] = 1;
                else dr["value"] = 0;

                param.Rows.Add(dr);

                mySqlDatabaseConnection.Update(query, param);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void MemberSave(string memberId, bool isMultiple)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"Update tec_loftdata set IsMultipleClub = @isMultiple where loftNo = @memberId;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@memberId";
                dr["value"] = memberId;
                param.Rows.Add(dr);

                dr = param.NewRow();
                dr["key"] = "@isMultiple";

                if (isMultiple) dr["value"] = 1;
                else dr["value"] = 0;

                param.Rows.Add(dr);

                mySqlDatabaseConnection.Update(query, param);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetPigeonList(string memberid)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT *,IFNULL(OtherClub,1) IsRegistered FROM tec_pigdata t where loftNo = @memberid;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@memberid";
                dr["value"] = memberid;

                param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetPigeonInfo(string rfid)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT * FROM tec_pigdata t where E_Ring = @ering;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@ering";
                dr["value"] = rfid;

                param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetPigeonInfoByRingNo(string ring,string memberid)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT * FROM tec_pigdata t where loftNo = @memberid and E_Ring = @ring;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@ring"; dr["value"] = ring; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@memberid"; dr["value"] = memberid; param.Rows.Add(dr);

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
