using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Common
    {
        #region Constant
        private const string SP_RFIDSAVE = "RFIDListSave";
        //private const string SP_ENTRYGETBYRACERELEASEPOINT = "EntryGetByRaceReleasePoint";
        //private const string SP_ENTRYDELETE = "EntryDelete";
        #endregion

        #region Variable
        DataLayer.DatabaseConnection dbconn;
        #endregion

        public DataSet RfidSave(string rfidTags)
        {
            try
            {
                DataSet dataResult = new DataSet();
                dbconn = new DatabaseConnection();
                dbconn.DatabaseConn(SP_RFIDSAVE);

                if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                dbconn.sqlConn.Open();
                dbconn.sqlComm.Parameters.Clear();
                dbconn.sqlComm.Parameters.AddWithValue("@RFIDTags", rfidTags);

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
