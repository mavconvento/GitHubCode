using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ResultDal
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
        public DataSet EclockResultSave(DomainObjects.Result result)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn("EclockCommand");

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@SMSContent", result.SMSContent);
                dbconn.sqlComm.Parameters.AddWithValue("@ActionFrom", result.ActionFrom);
                dbconn.sqlComm.Parameters.AddWithValue("@ClubName", result.ClubName);

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

        public DataTable GetResultListByDate(DateTime liberDate)
        {
            try
            {
                Common sqlparam = new Common();
                DataTable dt = new DataTable();
                MySqlDataConnection mySqlDatabaseConnection = new MySqlDataConnection();
                string query = @"SELECT t.*,IFNULL(d.OtherClub,1) IsRegistered,d.E_Ring,d.loftno FROM tec_pigracedata t
                                left join tec_pigdata d on t.PringNo = d.PringNo
                                where t.LiberDate = @liberDate and backtime is not null;";

                DataTable param = sqlparam.sqlParam();
                DataRow dr = param.NewRow();
                dr["key"] = "@liberDate";
                dr["value"] = liberDate.ToShortDateString();

                param.Rows.Add(dr);

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
