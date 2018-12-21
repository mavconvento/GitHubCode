using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DataAccess
{
    public static class Common
    {
        public static String GetSource()
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                String source = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + "\\source.inf";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        source = tr.ReadLine(); //Decrypt();
                    }
                }
                return source;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String GetConnStringTypeSource()
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                String source = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + "\\ConnectionStringType.inf";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        source = tr.ReadLine(); //Decrypt();
                    }
                }
                return source;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String DBName()
        {
            try
            {
                string sysDir = "";
                string connectionString = "";
                String source = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + "\\dbname.inf";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        source = tr.ReadLine(); //Decrypt();
                    }
                }
                return source;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
