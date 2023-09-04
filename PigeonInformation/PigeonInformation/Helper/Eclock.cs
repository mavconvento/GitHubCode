using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Helper
{
    public class Eclock
    {
        public void InitializeEclock(String comPortNumber)
        {
            try
            {
                SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
                comPort.Open();
                comPort.Close();
                comPort.Dispose();
            }
            catch (Exception ex)
            {
                Common.Logs(ex.Message);
            }

        }
        public void ResetEclock(String comPortNumber)
        {
            SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
            try
            {
                //System.Threading.Thread.Sleep(500);
                string Command = "$ReSt$#";

                comPort.Open();
                foreach (char item in Command)
                {
                    comPort.Write(item.ToString());
                }
                comPort.Close();
                comPort.Dispose();
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SyncTime(String comPortNumber)
        {
            SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
            try
            {

                DateTime now = new DateTime();
                now = DateTime.Now.AddSeconds(1);
                //System.Threading.Thread.Sleep(500);
                string Time = "$TiMe$" + now.Year.ToString() + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0');
                int daysOfWeek = 0;

                switch (now.DayOfWeek.ToString().ToUpper())
                {
                    case "MONDAY":
                        daysOfWeek = 1;
                        break;
                    case "TUESDAY":
                        daysOfWeek = 2;
                        break;
                    case "WEDNESDAY":
                        daysOfWeek = 3;
                        break;
                    case "THURSDAY":
                        daysOfWeek = 4;
                        break;
                    case "FRIDAY":
                        daysOfWeek = 5;
                        break;
                    case "SATURDAY":
                        daysOfWeek = 6;
                        break;
                    case "SUNDAY":
                        daysOfWeek = 7;
                        break;
                    default:
                        break;
                }
                Time = Time + daysOfWeek.ToString() + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + (now.Second + 1).ToString().PadLeft(2, '0') + "x#";
                comPort.Open();
                foreach (char item in Time)
                {
                    comPort.Write(item.ToString());
                }
                comPort.Close();
                comPort.Dispose();
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SendData(String Data, SerialPort comPort)
        {
            //SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
            comPort.Open();
            foreach (char item in Data)
            {
                comPort.Write(item.ToString());
            }
            comPort.Close();
            comPort.Dispose();
            return true;
        }

        public bool SendData(String Data, string comPortNumber)
        {
            SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
            comPort.Open();
            foreach (char item in Data)
            {
                comPort.Write(item.ToString());
            }
            comPort.Close();
            comPort.Dispose();
            return true;
        }

        public string ReceiveData(SerialPort comPort)
        {
            try
            {

                //if (comPort.IsOpen) comPort.Close();

                comPort.Open();
                System.Threading.Thread.Sleep(800);
                String incommingData = comPort.ReadExisting();
                int counter = 0;
                bool Isread = true;
                while (Isread)
                {
                    incommingData = comPort.ReadExisting();
                    System.Threading.Tasks.Task.Delay(2000);

                    counter++;

                    if (incommingData.Contains("dataend") & incommingData.Contains("datastart"))
                    {
                        String[] value = incommingData.Split('|');
                        int index = 0;

                        foreach (var item in value)
                        {
                            if (item.Contains("datastart"))
                            {
                                string rfid = value[index + 1];
                                if (rfid.Length >= 5)
                                {
                                    Isread = false;
                                }
                                break;
                            }
                            index++;
                        }   
                    }
                    else if (counter > 1000000 &&  string.IsNullOrEmpty(incommingData)) 
                    {
                        Isread = false;
                    }
                    //else if (incommingData.Trim().Length > 50)
                    //{
                    //    incommingData = "";
                    //    Isread = false;
                    //}
                }
                comPort.Close();
                comPort.Dispose();

                if (!incommingData.Contains("dataend") & !incommingData.Contains("datastart"))
                {
                    incommingData = "";
                }
                
                return incommingData;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public string ReceiveDataResult(string message,SerialPort comPort)
        {
            try
            {

                comPort.Open();
                foreach (char item in message)
                {
                    comPort.Write(item.ToString());
                }

                System.Threading.Thread.Sleep(1000);
                String incommingData = "";
                bool Isread = true;
                while (Isread)
                {
                    incommingData = comPort.ReadExisting();
                    if (incommingData.Contains("dataend") & incommingData.Contains("datastart"))
                    {
                        Isread = false;
                   }
                } 
                comPort.Close();
                comPort.Dispose();

                if (!incommingData.Contains("dataend") & !incommingData.Contains("datastart"))
                {
                    incommingData = "";
                }
                return incommingData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public String GetPort()
        {

            String value = "";
            ManagementClass processClass = new ManagementClass("Win32_PnPEntity");

            ManagementObjectCollection Ports = processClass.GetInstances();
            string device = "No recognized";
            foreach (ManagementObject property in Ports)
            {
                if (property.GetPropertyValue("Name") != null)
                    if ((property.GetPropertyValue("Name").ToString().Contains("USB") || (property.GetPropertyValue("Name").ToString().Contains("Arduino"))) &&
                        property.GetPropertyValue("Name").ToString().Contains("COM"))
                    {
                        Console.WriteLine(property.GetPropertyValue("Name").ToString());
                        device = property.GetPropertyValue("Name").ToString();

                        if (device.Contains("USB-SERIAL CH340"))
                        {
                            value = device;
                            break;
                        }
                        else if (device.Contains("Arduino Mega 2560"))
                        {
                            value = device;
                            break;
                        }
                        else if (device.Contains("USB Serial Device"))
                        {
                            value = device;
                            break;
                        }
                    }

            }
            return value;
        }
    }
}
