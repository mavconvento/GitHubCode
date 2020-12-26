using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Common
    {
        public DataSet RfidSave(string rfid)
        {
            try
            {
                DataLayer.Common entryDal = new DataLayer.Common();
                return entryDal.RfidSave(rfid);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
