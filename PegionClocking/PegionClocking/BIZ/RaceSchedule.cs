using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class RaceSchedule
    {
        #region Constant
        #endregion

        #region Variable
        DAL.RaceSchedule raceSchedule;
        #endregion

        #region Properties
        //public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 ScheduleID { get; set; }
        public String ScheduleName { get; set; }
        public String RegionName { get; set; }
        #endregion

        #region Public Methods
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                raceSchedule = new DAL.RaceSchedule();
                PopulateDataLayer();
                raceSchedule.Save();
                MessageBox.Show("Schedule Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ScheduleSelectAll(DataGridView ScheduleList)
        {
            try
            {
                raceSchedule = new DAL.RaceSchedule();
                PopulateDataLayer();
                ScheduleList.DataSource = raceSchedule.RaceScheduleSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ScheduleSelectAll()
        {
            try
            {
                DataTable dtResult;
                raceSchedule = new DAL.RaceSchedule();
                PopulateDataLayer();
                dtResult = raceSchedule.RaceScheduleSelectAll().Tables[0];
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ScheduleSearchByKey()
        {
            try
            {
                raceSchedule = new DAL.RaceSchedule();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceSchedule.RaceScheduleGetByKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean ScheduleDelete()
        {
            try
            {
                Boolean status = false;
                raceSchedule = new DAL.RaceSchedule();
                PopulateDataLayer();
                raceSchedule.RaceScheduleDelete();
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
                raceSchedule.ScheduleID = ScheduleID;
                raceSchedule.ScheduleName = ScheduleName;
                raceSchedule.ClubID = ClubID;
                raceSchedule.UserID = UserID;
                raceSchedule.RegionName = RegionName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}