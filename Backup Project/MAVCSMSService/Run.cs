using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MAVCSMSService
{
    public partial class Run : System.ServiceProcess.ServiceBase
    {
        private System.ComponentModel.IContainer components;
        private System.Diagnostics.EventLog eventLog1;

        public Run()
        {
            InitializeComponent();
            this.ServiceName = "MAVCSMSService";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = false;

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("SMSService"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "SMSService", "Initialize");
            }
            eventLog1.Source = "SMSService";
            eventLog1.Log = "Initialize";
        }


        protected override void OnStart(string[] args)
        {
            try
            {
                eventLog1.WriteEntry("Service OnStart");
                // Set up a timer to trigger every minute.
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 60000; // 60 seconds
                timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
                timer.Start();
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message);
            }
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStop()
        {
            try
            {
                 eventLog1.WriteEntry("Service OnStop");
            }
            catch (Exception ex)
            {
                
               eventLog1.WriteEntry(ex.Message);
            }
        }
    }
}
