using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class RaceResult
    {
        #region Constant
        #endregion

        #region Variable
        DAL.RaceResult raceResult;
        DAL.StickerNumber stickerNumber;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public String RaceCategoryName { get; set; }
        public String RaceCategoryGroupName { get; set; }
        public DateTime ReleasedDate { get; set; }
        public String Sender { get; set; }
        public String StickerCode { get; set; }
        public String Arrival { get; set; }
        public String PigeonID { get; set; }
        #endregion

        #region Public Methods
        public DataSet RaceResultAddFromBackup()
        {
            try
            {
                raceResult = new DAL.RaceResult();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = raceResult.RaceResultAddFromBackup();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ViewClubRace()
        {
            try
            {
                raceResult = new DAL.RaceResult();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = raceResult.ViewClubRace();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetSticker()
        {
            try
            {
                stickerNumber = new DAL.StickerNumber();
                DataSet dataResult = new DataSet();
                stickerNumber.Code = StickerCode;
                dataResult = stickerNumber.GetSticker();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultGetByKey()
        {
            try
            {
                raceResult = new DAL.RaceResult();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = raceResult.RaceResultGetByKeys();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultDelete()
        {
            try
            {
                raceResult = new DAL.RaceResult();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = raceResult.RaceResultDelete();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultGetByScheduleCategory()
        {
            try
            {
                raceResult = new DAL.RaceResult();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = raceResult.RaceResultGetByScheduleCategory();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Private Methods
        private void PopulateDataLayer()
        {
            try
            {
                raceResult.ClubID = ClubID;
                raceResult.UserID = UserID;
                raceResult.RaceScheduleCategoryName = RaceScheduleCategoryName;
                raceResult.RaceCategoryName = RaceCategoryName;
                raceResult.RaceCategoryGroupName = RaceCategoryGroupName;
                raceResult.DateRelease = ReleasedDate.Date;
                raceResult.Sender = Sender;
                raceResult.StickerCode = StickerCode;
                raceResult.Arrival = Arrival;
                raceResult.PigeonID = PigeonID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
