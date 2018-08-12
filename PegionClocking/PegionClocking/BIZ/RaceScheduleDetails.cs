using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class RaceScheduleDetails
    {
        #region Constant
        #endregion

        #region Variable
        DAL.RaceScheduleDetails raceScheduleDetails;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 ID { get; set; }
        public Int64 ScheduleID { get; set; }
        public Int64 LocationID { get; set; }
        public String LocationName { get; set; }
        public DateTime DateRelease { get; set; }
        public DateTime Loading { get; set; }
        public string LoadingTimeFrom { get; set; }
        public string LoadingTimeTo { get; set; }
        #endregion

        #region Public Methods
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                raceScheduleDetails = new DAL.RaceScheduleDetails();
                PopulateDataLayer();
                raceScheduleDetails.AddLocation();
                MessageBox.Show("ScheduleDetails Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ScheduleDetailsSelectAll(DataGridView ScheduleDetailsList)
        {
            try
            {
                raceScheduleDetails = new DAL.RaceScheduleDetails();
                PopulateDataLayer();
                ScheduleDetailsList.DataSource = raceScheduleDetails.ScheduleDetailsSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ScheduleDetailsSearchByKey()
        {
            try
            {
                raceScheduleDetails = new DAL.RaceScheduleDetails();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = raceScheduleDetails.LocationSearchByKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean ScheduleDetailsDelete()
        {
            try
            {
                Boolean status = false;
                raceScheduleDetails = new DAL.RaceScheduleDetails();
                PopulateDataLayer();
                raceScheduleDetails.RemoveLocation();
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
                raceScheduleDetails.ClubID = ClubID;
                raceScheduleDetails.UserID = UserID;
                raceScheduleDetails.ID = ID;
                raceScheduleDetails.RaceScheduleID = ScheduleID;
                raceScheduleDetails.LocationID = LocationID;
                raceScheduleDetails.LocationName = LocationName;
                raceScheduleDetails.DateRelease = DateRelease.Date;
                raceScheduleDetails.Loading = Loading.Date;
                raceScheduleDetails.LoadingTimeFrom = LoadingTimeFrom;
                raceScheduleDetails.LoadingTimeTo = LoadingTimeTo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
