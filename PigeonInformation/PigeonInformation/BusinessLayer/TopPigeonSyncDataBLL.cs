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
    public class TopPigeonSyncDataBLL
    {
        
        public Boolean TopPigeonPigDataToMavcDB(DomainObjects.TopPigeonPigData pigData)
        {
            try
            {
                DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
                topPigeonDal.TopPigeonPigDataToMavcDB(pigData);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Boolean MavcToTopPigeonPigData(DomainObjects.TopPigeonPigData pigData)
        {
            try
            {
                DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
                topPigeonDal.MavcToTopPigeonPigData(pigData);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable CheckPigeonExists(DomainObjects.TopPigeonPigData pigData)
        {
            DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
            return topPigeonDal.CheckPigeonExists(pigData);
        }

        public bool InsertPigeonInTopPigeonDB(DomainObjects.TopPigeonPigData pigData, string clubname)
        {
            DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
            topPigeonDal.InsertPigeonInTopPigeonDB(pigData, clubname);
            return true;
        }

        public bool UpdatePigeonInTopPigeonDB(DomainObjects.TopPigeonPigData pigData, string clubname)
        {
            DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
            topPigeonDal.UpdatePigeonInTopPigeonDB(pigData, clubname);
            return true;
        }

        public DataTable CheckPigeonRaceDataExists(DomainObjects.TopPigeonPigRaceData pigData)
        {
            DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
            return topPigeonDal.CheckPigeonRaceDataExists(pigData);
        }

        public bool InsertPigeonRaceDataInTopPigeonDB(DomainObjects.TopPigeonPigRaceData pigData)
        {
            DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
            topPigeonDal.InsertPigeonRaceDataInTopPigeonDB(pigData);
            return true;
        }

        public bool UpdatePigeonRaceDataInTopPigeonDB(DomainObjects.TopPigeonPigRaceData pigData)
        {
            DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
            topPigeonDal.UpdatePigeonRaceDataInTopPigeonDB(pigData);
            return true;
        }

        public DataSet GETTopPigeonDataByClockID(string ClockId, DateTime batchtime)
        {
            try
            {
                DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
                return topPigeonDal.GETTopPigeonDataByClockID(ClockId, batchtime); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet EclockReturnBirdSave(string clubid, string clockId, DateTime backdate, string ering, string action, bool saveRecord)
        {
            try
            {
                DataLayer.TopPigeonSyncDataDal topPigeonDal = new TopPigeonSyncDataDal();
                return topPigeonDal.EclockReturnBirdSave(clubid, clockId, backdate, ering, action, saveRecord);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
