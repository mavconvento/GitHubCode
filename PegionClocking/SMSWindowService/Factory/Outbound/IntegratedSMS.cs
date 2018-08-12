using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace SMSWindowService.Factory.Outbound
{
    public class IntegratedSMS
    {
        public void IntegrateSMS()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = GetSMSDataForExport();
                SMSDataExport(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private DataSet GetSMSDataForExport()
        {
            try
            {
                DataSet ds = new DataSet();
                DAL.SMSIntegrateDal smsIntegrateDAL = new DAL.SMSIntegrateDal();
                ds = smsIntegrateDAL.GetInbox();
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SMSDataExport(DataSet ds)
        {
            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string id = "0";
                        DAL.SMSIntegrateDal integrateInbox = new DAL.SMSIntegrateDal();
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            DataSet dst = new DataSet();
                            id = item["ID"].ToString();
                            dst = integrateInbox.SaveInbox(item["SMSID"].ToString(), item["SMSContent"].ToString(), item["Sender"].ToString(), item["SMSDate"].ToString(), item["SMSTime"].ToString(), item["ActivationCode"].ToString(), item["ModemID"].ToString(), item["IsProcess"].ToString(), "SMS", item["IsStickerNo"].ToString(), item["Value"].ToString());
                            integrateInbox.UpdateInboxImport(id, dst.Tables[0].Rows[0]["ReplyMessage"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SMSDataSaveToMainDB()
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
