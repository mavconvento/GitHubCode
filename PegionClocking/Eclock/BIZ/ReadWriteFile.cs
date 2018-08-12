using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Data;

namespace Eclock.BIZ
{
    public class ReadWriteFile
    {
        public String Drive { get; set; }

        private void ReadSetup()
        {
            try
            {
                DAL.Common common = new DAL.Common();
                DataSet ds = common.GetSetup();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Drive = ds.Tables[0].Rows[0]["Drive"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void ReadConnecntionStringFile(String Action)
        {
            try
            {

                string connectionString = "";
                ReadSetup();
                connectionString = Drive + Action + ".inf";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
