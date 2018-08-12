using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using SMSWindowService.Entity;
using SMSWindowService.Manager;
using SMSWindowService.DAL;


namespace SMSWindowService.Factory.Inbound
{
    public class ReadSMS
    {
        XMLConfig oSetting;
        XmlNode oNode;

        public void ReadSMSProcess(SMSComponent smsComponent)
        {
            try
            {
                oSetting = Entity.Config.GetConfig();
                oNode = oSetting.SystemSettingsXML;
                String message = "";

                message = smsComponent.ReadSMS();

                DecodeMessage(message, smsComponent);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void DecodeMessage(String SMSMessage, SMSComponent smsComponent)
        {

            try
            {
                Array ValCollection;
                String value = "";
                String findStr = "CMGL:";
                Int32 strIndex = 0;
                Int64 counter = 0;
                if (SMSMessage.Contains(findStr))
                {
                    strIndex = SMSMessage.IndexOf("+" + findStr, 1);
                    if (strIndex != -1)
                    {
                        SMSMessage = SMSMessage.Substring(strIndex - 1);
                        ErrMrg.LogMessage("SMSContent:" + SMSMessage, EventLogEntryType.Information);
                        while (SMSMessage != "")
                        {
                            value = "";
                            if (SMSMessage.IndexOf(findStr, 1) > 0)
                            {
                                SMSMessage = SMSMessage.Substring(SMSMessage.IndexOf(findStr, 1), SMSMessage.Length - SMSMessage.IndexOf(findStr, 1)).Trim();
                                if (SMSMessage.IndexOf(findStr, 1) > 0)
                                {
                                    value = SMSMessage.Substring(6, SMSMessage.IndexOf(findStr, 1) - 7).Trim();
                                }
                                else
                                {
                                    value = SMSMessage.Substring(6, SMSMessage.Length - 6).Trim();
                                    SMSMessage = "";
                                    if (counter > 15) value = "";
                                }
                                if (value != "")
                                {
                                    ValCollection = value.Split((char)13);
                                    if (ValCollection.Length >= 2)
                                    {
                                        ParseMessage(smsComponent,ValCollection.GetValue(0).ToString().Trim() + " " + ValCollection.GetValue(1).ToString().Trim());
                                        counter += 1;
                                    }

                                }
                                else
                                {
                                    SMSMessage = "";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ParseMessage(SMSComponent smsComponent, String message)
        {
            Array array;
            String ID = "";
            String Sender = "";
            String MessageType = "";
            String DateReceived = "";
            String TimeReceived = "";
            String Content = "";

            try
            {
           
            message = message.Replace(@"""", "").Replace(",,", ",");
            array = message.Split(',');

            ID = array.GetValue(0).ToString();
            Sender = array.GetValue(2).ToString();
            MessageType = array.GetValue(1).ToString();
            DateReceived = array.GetValue(3).ToString();

            Array spitTimeContent;

            spitTimeContent = array.GetValue(4).ToString().Split(' ');
            TimeReceived = spitTimeContent.GetValue(0).ToString();
            Content = array.GetValue(4).ToString().Substring(TimeReceived.Length + 1);

            SMSDal smsDal = new SMSDal();
            smsDal.ActivationCode = oNode.SelectSingleNode("smssettings/activationCode").InnerXml;
            smsDal.InboxSave(ID, Content, Sender, DateReceived, TimeReceived, oNode.SelectSingleNode("smssettings/modemID").InnerXml);
            DeleteSMS(smsComponent,ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void DeleteSMS(SMSComponent smsComponent, String ID)
        {

            try
            {
                smsComponent.DeleteSMS(Convert.ToInt32(ID)); //delete sms in storage
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }


}
