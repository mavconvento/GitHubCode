using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using SMSWindowService.Manager;
using System.Xml;
using SMSWindowService.Entity;
using SMSWindowService.Factory;


namespace SMSWindowService
{
    public partial class Service1 : ServiceBase
    {
        private Timer Timer1 = null;
        XMLConfig oSetting;
        XmlNode oNode;
        SMSComponent smsComponent;

        public Service1()
        {
            InitializeComponent();
        }

        public void startWin()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            try
            {

                String cInstance = "";
                String type = "";

                oSetting = Entity.Config.GetConfig();

                oNode = oSetting.SystemSettingsXML;
                cInstance = oNode.SelectSingleNode("environment").InnerXml;
                ErrMrg.LogMessage(cInstance + " Service Started", EventLogEntryType.Information);

                type = oNode.SelectSingleNode("smssettings/type").InnerXml;

                if (type == "Receiver" || type == "Sender")
                {
                    smsComponent = Entity.Config.GetSMSComponent();
                    smsComponent.InitializeModem();
                    smsComponent.OpenPort();
                }

                Timer1 = new Timer();
                this.Timer1.Interval = 1000;
                this.Timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer1_Tick);
                Timer1.Start();
                GC.KeepAlive(Timer1);
            }
            catch (Exception ex)
            {
                ErrMrg.LogMessage(ex.Message, EventLogEntryType.Error);
                this.startWin();
            }
        }

        protected override void OnStop()
        {

            String cInstance = "";

            oSetting = Entity.Config.GetConfig();

            oNode = oSetting.SystemSettingsXML.SelectSingleNode("environment");
            cInstance = oNode.InnerXml;
            smsComponent.ClosePort();

            ErrMrg.LogMessage(cInstance + " Service Stoped", EventLogEntryType.Information);
        }

        private void Timer1_Tick(object sender, ElapsedEventArgs e)
        {
            string processFlow = "";
            string type = "";
            try
            {
                Timer1.Stop();
                oNode = oSetting.SystemSettingsXML;
                processFlow = oNode.SelectSingleNode("processflow").InnerXml;
                type = oNode.SelectSingleNode("smssettings/type").InnerXml;
                //ErrMrg.LogMessage(type + " Inbound Process Starting.", EventLogEntryType.Information);

                if (processFlow == "Inbound")
                {
                    Factory.Inbound.InboundProcess inboundProcess = new Factory.Inbound.InboundProcess();
                    inboundProcess.GetInboundProcess(type,smsComponent);
                }
                else if (processFlow == "Outbound")
                {
                    Factory.Outbound.OutboundProcess outboundProcess = new Factory.Outbound.OutboundProcess();
                    outboundProcess.GetOutBoundProcess(type);
                }
            }
            catch (Exception ex)
            {
                ErrMrg.LogMessage(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                Timer1.Start();// = true;
            }
        }

    }
}
