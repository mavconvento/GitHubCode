using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SMSWindowService.Entity;

namespace SMSWindowService.Entity
{
    public class Config
    {
        public static XmlNode _systemSettings;
        public static String _DBConnection;
        public static String _Messages;

        const string xmlSettingFile = @"\XMLConfig.xml";


        public static SMSComponent GetSMSComponent()
        {
            try
            {
                XMLConfig oSetting;
                XmlNode oNode;

                oSetting = Entity.Config.GetConfig();
                oNode = oSetting.SystemSettingsXML;

                SMSComponent smsComponent = new SMSComponent();
                smsComponent.ModemVersion = oNode.SelectSingleNode("smssettings/modemVersion").InnerXml;
                smsComponent.ModemID = oNode.SelectSingleNode("smssettings/modemID").InnerXml;
                smsComponent.PortNo = oNode.SelectSingleNode("smssettings/portNo").InnerXml;
                smsComponent.BaudRate = Convert.ToInt32(oNode.SelectSingleNode("smssettings/baudRate").InnerXml);
                smsComponent.MemoryType = oNode.SelectSingleNode("smssettings/memoryType").InnerXml;
                smsComponent.ReplyDelay = Convert.ToInt16(oNode.SelectSingleNode("smssettings/replyDelay").InnerXml);
                smsComponent.SleepValue = Convert.ToInt16(oNode.SelectSingleNode("smssettings/delay").InnerXml);
                smsComponent.AdditionalDelay = Convert.ToInt16(oNode.SelectSingleNode("smssettings/additionalDelay").InnerXml);
                smsComponent.MessageType = oNode.SelectSingleNode("smssettings/messageType").InnerXml;
                smsComponent.Type = oNode.SelectSingleNode("smssettings/type").InnerXml;
                smsComponent.IntegrationType = oNode.SelectSingleNode("smssettings/integrationType").InnerXml;

                return smsComponent;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        
        }

        public static XMLConfig GetConfig()
        {
            XMLConfig oXmlConfig = new XMLConfig();
            String cLocalPath = "";

            cLocalPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            oXmlConfig.SystemSettingsXML = GetSystemSettings(cLocalPath);
            //oXmlConfig.DbConnectionString = oXmlConfig.SystemSettingsXML.SelectSingleNode("databaseConnection").InnerXml;

            return oXmlConfig;
        }


        private static XmlNode GetSystemSettings(String cLocalPath)
        {
            XmlDocument oDoc;

            try
            {
                if (_systemSettings == null)
                {
                    oDoc = new XmlDocument();
                    oDoc.Load(cLocalPath + xmlSettingFile);
                    _systemSettings = oDoc.SelectSingleNode("/systemsetting");
                }

                return _systemSettings;
            }
            catch (Exception ex)
            {
                //ErrorMgr.LogMessage(ex.ToString, EventLogEntryType.Error);
                throw ex;
            }
        }
    }
}
