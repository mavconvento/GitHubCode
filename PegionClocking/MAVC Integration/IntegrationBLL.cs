using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MAVC_Integration;

namespace MAVC_Integration
{
    public class IntegrationBLL
    {
        public String FileNotesID { get; set; }
        public String AccountName { get; set; }
        public String AccountID { get; set; }
        public String ClubID { get; set; }
        public String Action { get; set; }
        public String Status { get; set; }
        public String ExternalID { get; set; }

        public DataSet GetData()
        {
            try
            {
                DataSet dtResult = new DataSet();
                MAVC_Integration.IntegrationDAL DAL = new MAVC_Integration.IntegrationDAL();
                dtResult = DAL.GetRecord();
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TransferRecord()
        {
            try
            {
                DataSet dtResult = new DataSet();
                MAVC_Integration.IntegrationDAL DAL = new MAVC_Integration.IntegrationDAL();
                DAL.bll = this;
                dtResult = DAL.TransferRecord();

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        Status =(string) dtResult.Tables[0].Rows[0]["Status"];
                        ExternalID =(string) dtResult.Tables[0].Rows[0]["ExternalID"];
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
