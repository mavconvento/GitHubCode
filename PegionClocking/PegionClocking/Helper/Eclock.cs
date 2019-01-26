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
        public void SyncTime()
        {
            SerialPort comPort = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
            try
            {
               
                DateTime now = new DateTime();
                now = DateTime.Now.AddSeconds(2);
                //System.Threading.Thread.Sleep(500);
                string Time = "$TiMe$" + now.Year.ToString().Substring(2).PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0');
                Time = Time + "w" + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + "x#";
                comPort.Open();
                foreach (char item in Time)
                {
                    comPort.Write(item.ToString());
                }
                comPort.Close();
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
    }
}
