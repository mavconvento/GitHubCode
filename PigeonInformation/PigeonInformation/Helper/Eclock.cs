using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Eclock
    {
        public void SyncTime(String comPortNumber)
        {
            SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
            try
            {

                DateTime now = new DateTime();
                now = DateTime.Now.AddSeconds(2);
                //System.Threading.Thread.Sleep(500);
                string Time = "$TiMe$" + now.Year.ToString().Substring(2).PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0');
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
                Time = Time + daysOfWeek.ToString() + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + "x#";
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
        public bool SendData(String Data, String comPortNumber)
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

        public string ReceiveData(String comPortNumber)
        {
            SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
            comPort.Open();
            String incommingData = comPort.ReadExisting();
            comPort.Close();
            comPort.Dispose();
            return incommingData;
        }
    }
}
