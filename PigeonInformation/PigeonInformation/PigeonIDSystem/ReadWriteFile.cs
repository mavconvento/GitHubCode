using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonIDSystem
{
    public class ReadWriteFile
    {
        public String Drive { get; set; }

        public void ReadConnecntionStringFile(String Action)
        {
            try
            {

                string connectionString = "";
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
