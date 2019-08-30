using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Common
    {
        public static void Logs(string ErrorMessage)
        {
            string path = ReadText.ReadFilePath("datapath");
            string dateString = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0');
            string errorLogs = path + @"\" + dateString + ".txt";

            if (File.Exists(errorLogs))
            {
                using (StreamWriter sw = File.AppendText(errorLogs))
                {
                    sw.WriteLine(ErrorMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(errorLogs))
                {
                    sw.WriteLine(ErrorMessage);
                }
            }
        }
    }
}
