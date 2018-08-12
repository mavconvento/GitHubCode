using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;
using SMSWindowService.Entity;

namespace SMSWindowService.Manager
{
    public class ErrMrg
    {
        public static void LogMessage(String strMessage, EventLogEntryType iEventLogEntryType)
        {
            try
            {

                XMLConfig oSetting;
                XmlNode oNode;
                String cInstance = "";

                oSetting = Entity.Config.GetConfig();

                oNode = oSetting.SystemSettingsXML.SelectSingleNode("environment");

                if (oNode != null)
                {
                    cInstance = oNode.InnerXml;
                }

                if (cInstance == "") cInstance = "SMSMAVCService";


                EventLog el = new EventLog();
                
                if (System.Diagnostics.EventLog.SourceExists(cInstance) == null)
                {
                    System.Diagnostics.EventLog.CreateEventSource(cInstance, cInstance);
                }

                el.Source = cInstance;
                el.WriteEntry(strMessage, iEventLogEntryType);

            }
            catch (Exception ex)
            {
            }
        }
    }
}
