using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMSSolution
{
    public class Common
    {
        public static string GetApplicationPath()
        {
            string sysDir = AppDomain.CurrentDomain.BaseDirectory;
            return sysDir;
        }
        public static void ErrorLogs(string errorMessage)
        {
            try
            {
                string applicationpath = GetApplicationPath();
                string logsPath = applicationpath + "Logs";
                string logsFileName = logsPath + @"\" + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0') + DateTime.Today.Year.ToString().PadLeft(2, '0');
                //check if logs folder exists
                if (!Directory.Exists(logsPath))
                {
                    Directory.CreateDirectory(logsPath);
                }

                if (File.Exists(logsFileName))
                {
                    using (StreamWriter sw = File.AppendText(logsFileName))
                    {
                        sw.WriteLine(errorMessage);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(logsFileName))
                    {
                        sw.WriteLine(errorMessage);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
