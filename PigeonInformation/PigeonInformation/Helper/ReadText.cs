using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class ReadText
    {
        public static string ReadFilePath(string filename)
        {
            try
            {
                string sysDir = "";
                string path = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                path = sysDir + filename + ".inf";
                if (File.Exists(path))
                {
                    System.IO.TextReader tr = new StreamReader(path);
                    using (tr)
                    {
                        path = tr.ReadLine();
                    }

                    path = path + "\\" + ReadClub("club");
                }

                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ReadClub(string filename)
        {
            try
            {
                string sysDir = "";
                string path = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                path = sysDir + "\\" + filename + ".txt";
                if (File.Exists(path))
                {
                    System.IO.TextReader tr = new StreamReader(path);
                    using (tr)
                    {
                        path = tr.ReadLine();
                    }
                }

                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string[] ReadTextFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    string[] lines = System.IO.File.ReadAllLines(filename);
                    return lines;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
