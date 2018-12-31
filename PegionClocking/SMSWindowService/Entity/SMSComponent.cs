using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using SerialPortTester;
using System.Threading;
using System.Diagnostics;
using SMSWindowService.Manager;

namespace SMSWindowService.Entity
{
    public class SMSComponent
    {
        String StrValue = "";
        public SerialPort SMSPort;
        public int BaudRate { get; set; }
        public String MessageType { get; set; }
        public String PortNo { get; set; }
        public String ModemType { get; set; }
        public String ModemID { get; set; }
        public String MemoryType { get; set; }
        public String ModemVersion { get; set; }
        public Int32 AdditionalDelay { get; set; }
        public Int32 ReplyDelay { get; set; }
        public Int32 DeleteDelay { get; set; }
        public Int32 SleepValue { get; set; }
        public String IntegrationType { get; set; }
        public String ApplicationStartMode { get; set; }
        public String TestMobileNumber { get; set; }
        public String AutostartDelay { get; set; }
        public String Type { get; set; }

        public void ComPortSetup()
        {
            SMSPort = new SerialPort();
            SMSPort.BaudRate = BaudRate;
            SMSPort.PortName = PortNo;
            SMSPort.Parity = Parity.None;
            SMSPort.DataBits = 8;
            SMSPort.Handshake = Handshake.RequestToSend;
            SMSPort.DtrEnable = true;
            SMSPort.RtsEnable = true;
            SMSPort.NewLine = Environment.NewLine;
        }

        public Boolean OpenPort()
        {
            try
            {
                
                if (!SMSPort.IsOpen)
                {
                    SMSPort.Open();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ClosePort()
        {
            try
            {
                if (SMSPort.IsOpen)
                {
                    SMSPort.DtrEnable = false;
                    SMSPort.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Sleep(Int32 value)
        {
            try
            {
                Int64 index = 0;
                while (index <= (value * 300000))
                {
                    index++;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InitializeModem()
        {
            try
            {
                //bool status = false;
                SerialPortFixer serialPortFix = new SerialPortFixer();
                ComPortSetup();
                serialPortFix.Execute(PortNo);
             
                OpenPort();
                if (SMSPort.IsOpen)
                {
                    SMSPort.WriteLine("AT");
                    Sleep(SleepValue);
                    if (ModemVersion == "2.0")
                    {
                        StrValue = @"AT+CPMS=" + @"""" + MemoryType + @""",""" + MemoryType + @""",""" + MemoryType + @"""";
                        SMSPort.WriteLine( StrValue + Environment.NewLine); //set memory use is gsm at+cpms? for me storage 
                    }
                    else if (ModemVersion == "1.0")
                    {
                        StrValue = @"AT+CPMS=""" + MemoryType + @""",""" + MemoryType + @"""";
                        SMSPort.WriteLine(StrValue + Environment.NewLine); //set memory use is gsm at+cpms? for sm storage 
                    }

                    Sleep(SleepValue);
                    SMSPort.WriteLine("AT&W" + Environment.NewLine);
                    Sleep(SleepValue);
                    SMSPort.WriteTimeout = 30;
                }
                //ClosePort();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public String ReadSMS()
        {
            try
            {
                String message = "";

                //OpenPort();
                if (SMSPort.IsOpen)
                {
                    SMSPort.WriteLine("AT");
                    Sleep(SleepValue);
                    SMSPort.WriteLine("AT+CMGF=1" + Environment.NewLine);
                    Sleep(SleepValue);
                    SMSPort.DiscardInBuffer();
                    if (MessageType == "REC READ" || MessageType == "ALL")
                    {
                        SMSPort.WriteLine(@"AT+CMGL=""REC READ""" + Environment.NewLine);
                        Sleep(AdditionalDelay);
                        message = SMSPort.ReadExisting();
                        Sleep(SleepValue);
                    }
                    if (MessageType == "REC UNREAD" || MessageType == "ALL")
                    {
                        if (!message.Contains("+CMGL:"))
                        {
                            SMSPort.DiscardInBuffer();
                            Sleep(SleepValue);
                            SMSPort.WriteLine(@"AT+CMGL=""REC UNREAD""" + Environment.NewLine);
                            Sleep(AdditionalDelay);
                            message = SMSPort.ReadExisting();
                            Sleep(SleepValue);
                        }
                    }
                }
                //ClosePort();
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean SendSMS(Boolean isDeleted, Int64 inboxID, String mobileNumber, String smsContent)
        {
            try
            {
                //Boolean isSMSSend = false;
                //OpenPort();

                if (SMSPort.IsOpen == true)
                {
                    SMSPort.WriteLine("AT");
                    Sleep(SleepValue);
                    SMSPort.WriteLine(@"AT+CMGF=1" + Environment.NewLine);

                    if (mobileNumber.Length <= 13)
                    {
                        if (ModemType == "Smart")
                        {
                            SMSPort.WriteLine(@"AT+CSCA=""+639180000371""" + Environment.NewLine); //for smart
                        }
                        else
                        {
                            SMSPort.WriteLine(@"AT+CSCA=""+639170000130""" + Environment.NewLine); //for globe
                        }

                        Sleep(ReplyDelay);
                        SMSPort.WriteLine(@"AT+CMGS=""" + mobileNumber + @"""" + Environment.NewLine);
                        Sleep(ReplyDelay);
                        SMSPort.WriteLine(smsContent + Environment.NewLine + (char)26); //SMS sending
                        Sleep(ReplyDelay);

                        return true;
                    }
                }
                //ClosePort();
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteSMS(Int32 inboxID)
        {
            try
            {
                //OpenPort();
                if (SMSPort.IsOpen)
                {
                    SMSPort.WriteLine("AT");
                    Sleep(AdditionalDelay);
                    SMSPort.WriteLine(@"AT+CMGD=" + inboxID.ToString() + "" + Environment.NewLine);
                    Sleep(SleepValue);
                    SMSPort.WriteTimeout = 30;
                }
                //ClosePort();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
