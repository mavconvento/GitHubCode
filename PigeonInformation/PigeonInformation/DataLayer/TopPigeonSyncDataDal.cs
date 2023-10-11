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
    public class TopPigeonSyncDataDal
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
        public DataSet TopPigeonPigDataToMavcDB(DomainObjects.TopPigeonPigData pigData)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("TopPigeonPigDataSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClockId", pigData.ClockId);
                dbconn.sqlComm.Parameters.AddWithValue("@LoftName", pigData.LoftName);
                dbconn.sqlComm.Parameters.AddWithValue("@LoftNo", pigData.LoftNo);
                dbconn.sqlComm.Parameters.AddWithValue("@PRingNo", pigData.PRingNo);
                dbconn.sqlComm.Parameters.AddWithValue("@RCountry", pigData.RCountry);
                dbconn.sqlComm.Parameters.AddWithValue("@RYear", pigData.RYear);
                dbconn.sqlComm.Parameters.AddWithValue("@RRegLetter", pigData.RRegLetter);
                dbconn.sqlComm.Parameters.AddWithValue("@RRegNumber", pigData.RRegNumber);
                dbconn.sqlComm.Parameters.AddWithValue("@Sex", pigData.Sex);
                dbconn.sqlComm.Parameters.AddWithValue("@E_Ring", pigData.E_Ring);
                dbconn.sqlComm.Parameters.AddWithValue("@ColorType", pigData.ColorType);
                dbconn.sqlComm.Parameters.AddWithValue("@Comment", pigData.Comment);
                dbconn.sqlComm.Parameters.AddWithValue("@ActiveStat", pigData.ActiveStat);
                dbconn.sqlComm.Parameters.AddWithValue("@Updatetime", pigData.Updatetime);
                dbconn.sqlComm.Parameters.AddWithValue("@SynchFlag", pigData.SynchFlag);
                dbconn.sqlComm.Parameters.AddWithValue("@RandomCode", pigData.RandomCode);
                dbconn.sqlComm.Parameters.AddWithValue("@UID", pigData.UID);
                dbconn.sqlComm.Parameters.AddWithValue("@CreateDate", pigData.CreateDate);
                dbconn.sqlComm.Parameters.AddWithValue("@AssignDate", pigData.AssignDate);
                dbconn.sqlComm.Parameters.AddWithValue("@OtherClub", pigData.OtherClub);
                dbconn.sqlComm.Parameters.AddWithValue("@Source", pigData.Source);
                dbconn.sqlComm.Parameters.AddWithValue("@BatchDatetime", pigData.BatchDatetime);
                dbconn.sqlComm.ExecuteNonQuery();

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = dbconn.sqlComm;
                //da.Fill(dataResult);
                dbconn.sqlConn.Close();
                return dataResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GETTopPigeonDataByClockID(string clockId, DateTime batchtime)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("GETTopPigeonDataByClockID");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClockId", clockId);
                dbconn.sqlComm.Parameters.AddWithValue("@BatchDatetime", batchtime);
                //dbconn.sqlComm.ExecuteNonQuery();

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

        public void MavcToTopPigeonPigData(DomainObjects.TopPigeonPigData pigData)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = "pigDataSave";

                DataTable param = sqlparam.sqlParam();
                //DataRow dr = param.NewRow(); 
                //dr["key"] = "_LoftNo"; dr["value"] = pigData.LoftNo.ToString(); param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_PRingNo"; dr["value"] = pigData.PRingNo; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_RCountry"; dr["value"] = pigData.RCountry; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_RYear"; dr["value"] = pigData.RYear; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_RRegLetter"; dr["value"] = pigData.RRegLetter; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_RRegNumber"; dr["value"] = pigData.RRegNumber; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_Sex"; dr["value"] = pigData.Sex; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_E_Ring"; dr["value"] = pigData.E_Ring; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_ColorType"; dr["value"] = pigData.ColorType; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_Comment"; dr["value"] = ""; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_ActiveStat"; dr["value"] = pigData.ActiveStat; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_Updatetime"; dr["value"] = pigData.Updatetime; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_SynchFlag"; dr["value"] = pigData.SynchFlag; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_RandomCode"; dr["value"] = pigData.RandomCode; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_UID"; dr["value"] = pigData.UID; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_CreateDate"; dr["value"] = pigData.CreateDate; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_AssignDate"; dr["value"] = pigData.AssignDate; param.Rows.Add(dr);
                //dr = param.NewRow(); dr["key"] = "_OtherClub"; dr["value"] = pigData.OtherClub; param.Rows.Add(dr);

                mySqlDatabaseConnection.ExecuteProceduteNoReturn(query, param);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable CheckPigeonExists(DomainObjects.TopPigeonPigData pigData)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"select Updatetime,LoftNo,E_Ring from tec_pigdata where LoftNo = @LoftNo and PRingNo = @PRingNo";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@LoftNo"; dr["value"] = pigData.LoftNo.ToString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@PRingNo"; dr["value"] = pigData.PRingNo; param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool InsertPigeonInTopPigeonDB(DomainObjects.TopPigeonPigData pigData, string clubname)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"insert into tec_pigdata
                                    (LoftNo,PRingNo,RCountry,RYear,RRegLetter,
                                    RRegNumber,Sex,E_Ring,ColorType,Comment,ActiveStat,
                                    Updatetime,SynchFlag,RandomCode,UID,CreateDate,AssignDate,OtherClub)
                                values (@LoftNo,@PRingNo,@RCountry, @RYear,@RRegLetter,
                                        @RRegNumber,@Sex,@E_Ring,@ColorType,@Comment,
                                        @ActiveStat,@Updatetime,@SynchFlag,@RandomCode,
                                        @UID,@CreateDate,@AssignDate,@OtherClub);";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@LoftNo"; dr["value"] = pigData.LoftNo.ToString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@PRingNo"; dr["value"] = pigData.PRingNo; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RCountry"; dr["value"] = pigData.RCountry; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RYear"; dr["value"] = pigData.RYear; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RRegLetter"; dr["value"] = pigData.RRegLetter; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RRegNumber"; dr["value"] = pigData.RRegNumber; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@Sex"; dr["value"] = pigData.Sex; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@E_Ring"; dr["value"] = pigData.E_Ring; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@ColorType"; dr["value"] = pigData.ColorType; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@Comment"; dr["value"] = ""; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@ActiveStat"; dr["value"] = pigData.ActiveStat; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@Updatetime"; dr["value"] = pigData.Updatetime; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@SynchFlag"; dr["value"] = pigData.SynchFlag; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RandomCode"; dr["value"] = pigData.RandomCode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@UID"; dr["value"] = pigData.UID; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@CreateDate"; dr["value"] = pigData.CreateDate; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@AssignDate"; dr["value"] = pigData.AssignDate; param.Rows.Add(dr);

                if (pigData.Source == clubname)
                {
                    dr = param.NewRow(); dr["key"] = "@OtherClub"; dr["value"] = 1; param.Rows.Add(dr);
                }
                else
                {
                    dr = param.NewRow(); dr["key"] = "@OtherClub"; dr["value"] = 0; param.Rows.Add(dr);
                }

                mySqlDatabaseConnection.Insert(query, param);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdatePigeonInTopPigeonDB(DomainObjects.TopPigeonPigData pigData, string clubname)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"update tec_pigdata
                                set PRingNo = @PRingNo,
                                    RCountry = @RCountry,
                                    RYear = @RYear,
                                    RRegLetter = @RRegLetter,
                                    RRegNumber = @RRegNumber,
                                    Sex = @Sex,
                                    E_Ring = @E_Ring,
                                    ColorType = @ColorType,
                                    Comment = @Comment,
                                    ActiveStat = @ActiveStat,
                                    Updatetime = @Updatetime,
                                    SynchFlag = @SynchFlag,
                                    RandomCode = @RandomCode,
                                    UID = @UID,
                                    CreateDate = @CreateDate,
                                    AssignDate = @AssignDate
                                    where LoftNo = @LoftNo and PRingNo = @PRingNo;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@LoftNo"; dr["value"] = pigData.LoftNo.ToString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@PRingNo"; dr["value"] = pigData.PRingNo; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RCountry"; dr["value"] = pigData.RCountry; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RYear"; dr["value"] = pigData.RYear; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RRegLetter"; dr["value"] = pigData.RRegLetter; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RRegNumber"; dr["value"] = pigData.RRegNumber; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@Sex"; dr["value"] = pigData.Sex; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@E_Ring"; dr["value"] = pigData.E_Ring; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@ColorType"; dr["value"] = pigData.ColorType; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@Comment"; dr["value"] = ""; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@ActiveStat"; dr["value"] = pigData.ActiveStat; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@Updatetime"; dr["value"] = pigData.Updatetime; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@SynchFlag"; dr["value"] = pigData.SynchFlag; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RandomCode"; dr["value"] = pigData.RandomCode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@UID"; dr["value"] = pigData.UID; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@CreateDate"; dr["value"] = pigData.CreateDate; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@AssignDate"; dr["value"] = pigData.AssignDate; param.Rows.Add(dr);

                //if (pigData.Source == clubname)
                //{
                //    dr = param.NewRow(); dr["key"] = "@OtherClub"; dr["value"] = pigData.OtherClub; param.Rows.Add(dr);
                //}

                mySqlDatabaseConnection.Update(query, param);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable CheckPigeonRaceDataExists(DomainObjects.TopPigeonPigRaceData pigData)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"select PRingNo from tec_pigracedata 
                               where PRingNo = @PRingNo and LiberCode = @LiberCode and LiberDate = @LiberDate;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@PRingNo"; dr["value"] = pigData.PRingNo.ToString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@LiberCode"; dr["value"] = pigData.LiberCode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@LiberDate"; dr["value"] = pigData.LiberDate; param.Rows.Add(dr);

                dt = mySqlDatabaseConnection.Select(query, param);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool InsertPigeonRaceDataInTopPigeonDB(DomainObjects.TopPigeonPigRaceData pigData)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"insert into tec_pigracedata
                                    (ClubID,LiberCode,LiberDate,PRingNo,RandomCode,ClockID,MarkedTime)
                                values 
                                    (@ClubID,@LiberCode,@LiberDate, @PRingNo,@RandomCode,@ClockID,@MarkedTime);";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@ClubID"; dr["value"] = pigData.ClubID.ToString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@LiberCode"; dr["value"] = pigData.LiberCode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@LiberDate"; dr["value"] = pigData.LiberDate; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@PRingNo"; dr["value"] = pigData.PRingNo; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RandomCode"; dr["value"] = pigData.RandomCode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@ClockID"; dr["value"] = pigData.ClockID; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@MarkedTime"; dr["value"] = pigData.MarkedTime; param.Rows.Add(dr);

                mySqlDatabaseConnection.Insert(query, param);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdatePigeonRaceDataInTopPigeonDB(DomainObjects.TopPigeonPigRaceData pigData)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"update tec_pigracedata
                                set ClubID = @ClubID,
                                    LiberCode = @LiberCode,
                                    LiberDate = @LiberDate,
                                    PRingNo = @PRingNo,
                                    RandomCode = @RandomCode,
                                    MarkedTime = @MarkedTime
                                    where PRingNo = @PRingNo and LiberCode = @LiberCode and LiberDate = @LiberDate;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@ClubID"; dr["value"] = pigData.ClubID.ToString(); param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@LiberCode"; dr["value"] = pigData.LiberCode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@LiberDate"; dr["value"] = pigData.LiberDate; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@PRingNo"; dr["value"] = pigData.PRingNo; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@RandomCode"; dr["value"] = pigData.RandomCode; param.Rows.Add(dr);
                dr = param.NewRow(); dr["key"] = "@MarkedTime"; dr["value"] = pigData.MarkedTime; param.Rows.Add(dr);
                

                mySqlDatabaseConnection.Update(query, param);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet EclockReturnBirdSave(string clubid, string clockId, DateTime backdate, string ering, string action, bool saveRecord)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConnTopPigeon("EclockReturnBirdSave");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@ClubId", clubid);
                dbconn.sqlComm.Parameters.AddWithValue("@ClockId", clockId);
                dbconn.sqlComm.Parameters.AddWithValue("@E_ring", ering);
                dbconn.sqlComm.Parameters.AddWithValue("@BackDate", backdate);
                dbconn.sqlComm.Parameters.AddWithValue("@Action", action);
                dbconn.sqlComm.Parameters.AddWithValue("@SaveRecord", saveRecord);

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

        #region Private

        #endregion

    }
}
