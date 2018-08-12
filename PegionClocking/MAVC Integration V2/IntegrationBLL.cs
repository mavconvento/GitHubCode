using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MAVC_IntegrationV2
{
    public class IntegrationBLL
    {
        IntegrationDAL dal;
        
        public DataSet GetFileNotes()
        {
            try
            {
                dal = new IntegrationDAL();
                return dal.GetFileNote();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
