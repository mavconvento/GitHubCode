using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SMSWindowService.Entity;


namespace SMSWindowService.Factory.Inbound
{
    public class InboundProcess
    {

        //XMLConfig oSetting;
        //XmlNode oNode;

        public void GetInboundProcess(String Type, SMSComponent smsComponent)
        {
            try
            {
                switch (Type)
                {
                    case "Receiver":
                        ReadSMS readSMS = new ReadSMS();
                        readSMS.ReadSMSProcess(smsComponent);
                        break;
                    case "Sender":
                        SendSMS sendSMS = new SendSMS();
                        sendSMS.SendSMSProcess(smsComponent);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
