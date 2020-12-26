using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSWindowService.Entity;
using System.IO.Ports;

namespace SMSSolution
{
    class Program
    {
        private const int delay = 1; 
        static void Main(string[] args)
        {
            StartProcess();
        }

        private static void StartProcess()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Common.ErrorLogs(ex.Message);
            }
        }
        private static SMSWindowService.Entity.SMSComponent ReadConfigFile()
        {
            SMSComponent smsComponent = new SMSComponent();
            smsComponent = Config.GetSMSComponent();
            return smsComponent;
        }

        private static void Delay(int value)
        {
            System.Threading.Thread.Sleep(value);
        }

        private static void ModemSetup(SMSComponent modemSetupConfig)
        {
            try
            {

                using (SerialPort SMSPort = new SerialPort())
                {
                    SMSPort.PortName = modemSetupConfig.PortNo;
                    SMSPort.BaudRate = modemSetupConfig.BaudRate;
                    SMSPort.Parity = Parity.None;
                    SMSPort.DataBits = 8;
                    SMSPort.StopBits = StopBits.One;
                    SMSPort.Handshake = Handshake.RequestToSend;
                    SMSPort.DtrEnable = true;
                    SMSPort.RtsEnable = true;
                    SMSPort.NewLine = Environment.NewLine;

                    if (!SMSPort.IsOpen) SMSPort.Open();

                    if (SMSPort.IsOpen)
                    {
                        SMSPort.WriteLine("AT");
                        Delay(delay);

                        if (modemSetupConfig.MemoryType == "ME") SMSPort.WriteLine(@"AT+CPMS=""" + modemSetupConfig.MemoryType + @""",""" + modemSetupConfig.MemoryType + @""",""" + modemSetupConfig.MemoryType + @"""" + Environment.NewLine);
                        else if(modemSetupConfig.MemoryType == "SM") SMSPort.WriteLine(@"AT+CPMS=""" + modemSetupConfig.MemoryType + @""","""  + modemSetupConfig.MemoryType + @"""" + Environment.NewLine);

                        Delay(delay);
                        SMSPort.WriteLine("AT&W" + Environment.NewLine);
                        Delay(delay);
                        SMSPort.WriteTimeout = 30;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ErrorLogs(ex.Message);
            }
        }
    }
}
