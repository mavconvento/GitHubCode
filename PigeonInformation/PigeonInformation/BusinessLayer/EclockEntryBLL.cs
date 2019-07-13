using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DomainObjects;

namespace BusinessLayer
{
    public class EclockEntryBLL
    {
        public DataSet EclockEntrySave(DomainObjects.Entry entry)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.EclockEntrySave(entry);
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }
    }
}
