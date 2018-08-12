using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace MAVC_Mobile_Integration
{
    public class IngtegrationDAL
    {
        public DataSet TransferDate()
        {
            try
            {
                DataSet dtResult = new DataSet();
                //dbconn = new DatabaseConnection("local");
                //dbconn.DatabaseConn("FileNotesGetAll");

                //if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
                //dbconn.sqlConn.Open();
                //dbconn.sqlComm.CommandTimeout = 0;
                //dbconn.sqlComm.Parameters.Clear();

                //SqlDataAdapter da = new SqlDataAdapter();
                //da.SelectCommand = dbconn.sqlComm;
                //da.Fill(dtResult);
                //dbconn.sqlConn.Close();

                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
