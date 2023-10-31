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

        public DataTable GetResultListByDate(DateTime liberDate)
        {
            try
            {
                DataLayer.ResultDal dal = new DataLayer.ResultDal();
                return dal.GetResultListByDate(liberDate);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet EclockTrainingSave(DomainObjects.Training result)
        {
            try
            {
                DataLayer.ResultDal dal = new DataLayer.ResultDal();
                return dal.EclockTrainingSave(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
