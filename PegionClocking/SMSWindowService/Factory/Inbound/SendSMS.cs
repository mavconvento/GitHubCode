using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Data;
using SMSWindowService.Entity;
using SMSWindowService.Manager;
using SMSWindowService.DAL;


namespace SMSWindowService.Factory.Inbound
{
    public class SendSMS
    {
        XMLConfig oSetting;
        XmlNode oNode;

        public void SendSMSProcess(SMSComponent smsComponent)
        {
            try
            {
                oSetting = Entity.Config.GetConfig();
                oNode = oSetting.SystemSettingsXML;

                DataSet ds = new DataSet();
                ds = GetRecordForReply(oNode.SelectSingleNode("smssettings/activationCode").InnerXml);
                ReplySMSMessage(ds, smsComponent);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ReplySMSMessage(DataSet ds, SMSComponent smsComponent)
        {
            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            SendingSMS(smsComponent, item["Sender"].ToString(), item["ReplyMessage"].ToString(), false, Convert.ToInt64(item["InboxID"]), "");
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SendingSMS(SMSComponent smsComponent, String mobilenumber, String message, Boolean IsDeleted, Int64 InboxID, String InboxType)
        {
            try
            {
                String status = "";
                String Remark = "";
                SMSDal smsDal = new SMSDal();

                if (smsComponent.SendSMS(IsDeleted, InboxID, mobilenumber, message))
                {
                    status = "Success";
                }
                else
                {
                    status = "Failed";
                }

                if (InboxID > 0)
                {
                    Remark = "AutoReply";
                }
                else
                {
                    Remark = "Manual";
                }

                if (status == "Success")
                {
                    smsDal.OutboxSave(message, mobilenumber, status, Remark, InboxID, InboxType);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private DataSet GetRecordForReply(String ActivationCode)
        {
            try
            {
                DataSet ds = new DataSet();
                SMSDal smsDal = new SMSDal();
                ds = smsDal.GetRecordForReply(ActivationCode);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
