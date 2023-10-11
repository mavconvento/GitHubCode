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

        public DataTable GetEntryList(string memberid, DateTime liberDate)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.GetEntryList(memberid, liberDate);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetEntryListByDate(DateTime liberDate, string liberCode)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.GetEntryListByDate(liberDate, liberCode);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEntryListByDateAndClockID(DateTime liberDate, string liberCode, string clockid)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.GetEntryListByDateAndClockID(liberDate, liberCode, clockid);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetRaceCode(DateTime liberDate)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.GetRaceCode(liberDate);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public DataSet TopPigeonRaceCodeSave(TopPigeonRaceCode racecode)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.TopPigeonRaceCodeSave(racecode);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetTopPigeonPigRaceData(string ClockID, DateTime LiberDate, string RaceCode)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.GetTopPigeonPigRaceData(ClockID, LiberDate, RaceCode);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetRaceCode(string ClubId, DateTime LiberDate)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.GetRaceCode(ClubId, LiberDate);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet TopPigeonPigRaceDataSave(DomainObjects.TopPigeonPigRaceData raceData)
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.TopPigeonPigRaceDataSave(raceData);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetTopPigeonRaceCode()
        {
            try
            {
                DataLayer.EntryDal entryDal = new EntryDal();
                return entryDal.GetTopPigeonRaceCode();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
