using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;


namespace PegionClocking.BIZ
{
    class RaceScheduleCategory
    {
        #region Constant
        #endregion

        #region Variable
        DAL.RaceScheduleCategory raceScheduleCategory;
        #endregion

        #region Properties
        //public Int64 ID {get;set;}
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RaceScheduleID { get; set; }
        public String RaceScheduleName { get; set; }
        public Int64 RaceScheduleCategoryID { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public Int64 Lap { get; set; }
        #endregion

        #region Public Methods
        public DataTable RaceScheduleCategoryGetByRaceSchedule()
        {
            try
            {
                raceScheduleCategory = new DAL.RaceScheduleCategory();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceScheduleCategory.RaceScheduleCategoryGetByRaceSchedule().Tables[0];
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
                raceScheduleCategory = new DAL.RaceScheduleCategory();
                PopulateDataLayer();
                raceScheduleCategory.Save();
                MessageBox.Show("Schedule Category Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ScheduleCategorySelectAll(DataGridView ScheduleList)
        {
            try
            {
                raceScheduleCategory = new DAL.RaceScheduleCategory();
                PopulateDataLayer();
                ScheduleList.DataSource = raceScheduleCategory.RaceScheduleCategorySelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ScheduleCategorySelectAll()
        {
            try
            {
                raceScheduleCategory = new DAL.RaceScheduleCategory();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceScheduleCategory.RaceScheduleCategorySelectAll().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ScheduleCategorySearchByKey()
        {
            try
            {
                raceScheduleCategory = new DAL.RaceScheduleCategory();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceScheduleCategory.RaceScheduleCategoryGetByKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean ScheduleCategoryDelete()
        {
            try
            {
                Boolean status = false;
                raceScheduleCategory = new DAL.RaceScheduleCategory();
                PopulateDataLayer();
                raceScheduleCategory.RaceScheduleCategoryDelete();
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
                raceScheduleCategory.ClubID = ClubID;
                raceScheduleCategory.UserID = UserID;
                raceScheduleCategory.RaceScheduleID = RaceScheduleID;
                raceScheduleCategory.RaceScheduleName = RaceScheduleName;
                raceScheduleCategory.RaceScheduleCategoryID = RaceScheduleCategoryID;
                raceScheduleCategory.RaceScheduleCategoryName = RaceScheduleCategoryName;
                raceScheduleCategory.Lap = Lap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
