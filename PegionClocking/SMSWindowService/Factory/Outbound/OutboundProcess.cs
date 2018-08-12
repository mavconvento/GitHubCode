using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SMSWindowService.Entity;

namespace SMSWindowService.Factory.Outbound
{
    public class OutboundProcess
    {
        XMLConfig oSetting;
        XmlNode oNode;

        public void GetOutBoundProcess(String Type)
        {
            try
            {
                switch (Type)
                {
                    case "IntegrateSMS":
                        IntegratedSMS integrateSMS = new IntegratedSMS();
                        integrateSMS.IntegrateSMS();
                        break;
                    case "WebClockingProcess":
                        WebClockingProcess webClockingProcess = new WebClockingProcess();
                        webClockingProcess.WebClockingProces();
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
