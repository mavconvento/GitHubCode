using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class RaceReleasePoint
    {
        #region Constant
        #endregion

        #region Variable
        DAL.RaceReleasePoint raceReleasePoint;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public string LocationName { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public Int64 RaceScheduleCategoryID { get; set; }
        public Int64 RaceScheduleDetailsID { get; set; }
        public string ReleaseTime { get; set; }
        public string ReleaseDate { get; set; }
        public double Multiplier { get; set; }
        public Int64 LapNo { get; set; }
        public string MinSpeed { get; set; }
        public bool IsStop { get; set; }
        public DateTime StopFromDate { get; set; }
        public string StopFromTime { get; set; }
        public DateTime StopToDate { get; set; }
        public string StopToTime { get; set; }
        public String RaceScheduleName { get; set; }
        public String Description { get; set; }
        #endregion

        #region Public Methods
        public DataTable RaceReleasePointGetbyRaceScheduleCategory()
        {
            try
            {
                raceReleasePoint = new DAL.RaceReleasePoint();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceReleasePoint.RaceReleasePointGetbyRaceScheduleCategory().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RaceReleasePointSummary()
        {
            try
            {
                raceReleasePoint = new DAL.RaceReleasePoint();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceReleasePoint.RaceReleasePointSummary().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RaceReleasePointGetLocation()
        {
            try
            {
                raceReleasePoint = new DAL.RaceReleasePoint();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceReleasePoint.LocationSearchByKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRaceReleasePointBySchedule()
        {
            try
            {
                raceReleasePoint = new DAL.RaceReleasePoint();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceReleasePoint.GetRaceReleasePointBySchedule().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public DataTable RaceReleasePointGetbyKey()
        {
            try
            {
                raceReleasePoint = new DAL.RaceReleasePoint();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceReleasePoint.RaceReleasePointGetbyKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable EClockRaceReleasePoint()
        {
            try
            {
                raceReleasePoint = new DAL.RaceReleasePoint();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceReleasePoint.EClockRaceReleasePoint().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                raceReleasePoint = new DAL.RaceReleasePoint();
                PopulateDataLayer();
                raceReleasePoint.Save();
                MessageBox.Show("Release Point Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RaceReleasePointSelectAll(DataGridView ScheduleList)
        {
            try
            {
                raceReleasePoint = new DAL.RaceReleasePoint();
                PopulateDataLayer();
                ScheduleList.DataSource = raceReleasePoint.RaceReleasePointSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean RaceReleasePointDelete()
        {
            try
            {
                Boolean status = false;
                raceReleasePoint = new DAL.RaceReleasePoint();
                PopulateDataLayer();
                raceReleasePoint.RaceReleasePointDelete();
                MessageBox.Show("Record Successfully Deleted!", "Delete Record");
                status = true;
                return status;
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
                raceReleasePoint.ClubID = ClubID;
                raceReleasePoint.UserID = UserID;
                raceReleasePoint.LocationName = LocationName;
                raceReleasePoint.RaceReleasePointID = RaceReleasePointID;
                raceReleasePoint.RaceScheduleDetailsID = RaceScheduleDetailsID;
                raceReleasePoint.RaceScheduleCategoryID = RaceScheduleCategoryID;
                raceReleasePoint.RaceScheduleCategoryName = RaceScheduleCategoryName;
                raceReleasePoint.ReleaseTime = ReleaseTime;
                raceReleasePoint.ReleaseDate = ReleaseDate;
                raceReleasePoint.Multiplier = Multiplier;
                raceReleasePoint.LapNo = LapNo;
                raceReleasePoint.MinSpeed = MinSpeed;
                raceReleasePoint.IsStop = IsStop;
                raceReleasePoint.StopFromDate = StopFromDate.Date;
                raceReleasePoint.StopFromTime = StopFromTime;
                raceReleasePoint.StopToDate = StopToDate.Date;
                raceReleasePoint.StopToTime = StopToTime;
                raceReleasePoint.RaceScheduleName = RaceScheduleName;
                raceReleasePoint.Description = Description;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
