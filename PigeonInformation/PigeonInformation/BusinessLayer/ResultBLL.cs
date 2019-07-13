using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ResultBLL
    {
        public DataSet EclockResultSave(DomainObjects.Result result)
        {
            try
            {
                DataLayer.ResultDal resultDal = new DataLayer.ResultDal();
                return resultDal.EclockResultSave(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
