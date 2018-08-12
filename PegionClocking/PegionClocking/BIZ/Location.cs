using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class Location
    {
        #region Constant
        #endregion

        #region Variable
        DAL.Location location;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 ScheduleCategoryID { get; set; }
        public Int64 ScheduleID { get; set; }
        public Int64 LocationID { get; set; }
        public String LocationName { get; set; }
        public String RegionName { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSecond { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSecond { get; set; }
        public String DistanceLongSign { get; set; }
        #endregion

        #region Public Methods
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                location = new DAL.Location();
                PopulateDataLayer();
                location.Save();
                MessageBox.Show("Location Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LocationSelectAll(DataGridView locationList)
        {
            try
            {
                location = new DAL.Location();
                PopulateDataLayer();
                locationList.DataSource = location.LocationSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LocationSelectAll()
        {
            try
            {
                DataTable dtLocation;
                location = new DAL.Location();
                PopulateDataLayer();
                dtLocation = location.LocationSelectAll().Tables[0];
                return dtLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LocationSelectByRegion()
        {
            try
            {
                DataTable dtLocation;
                location = new DAL.Location();
                PopulateDataLayer();
                dtLocation = location.LocationSelectByRegion().Tables[0];
                return dtLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LocationSearchByScheduleCategory()
        {
            try
            {
                DataTable dtLocation;
                location = new DAL.Location();
                PopulateDataLayer();
                dtLocation = location.LocationSearchbyScheduleCategory().Tables[0];
                return dtLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LocationSearchByKey()
        {
            try
            {
                location = new DAL.Location();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = location.LocationSearchByKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean LocationDelete()
        {
            try
            {
                Boolean status = false;
                location = new DAL.Location();
                PopulateDataLayer();
                location.LocationDelete();
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
                location.ClubID = ClubID;
                location.UserID = UserID;
                location.LocationID = LocationID;
                location.ScheduleCategoryID = ScheduleCategoryID;
                location.ScheduleID = ScheduleID;
                location.LocationName = LocationName;
                location.RegionName = RegionName;
                location.DistanceLatDegree = DistanceLatDegree;
                location.DistanceLatMinutes = DistanceLatMinutes;
                location.DistanceLatSecond = DistanceLatSecond;
                location.DistanceLatSign = DistanceLatSign;
                location.DistanceLongDegree = DistanceLongDegree;
                location.DistanceLongMinutes = DistanceLongMinutes;
                location.DistanceLongSecond = DistanceLongSecond;
                location.DistanceLongSign = DistanceLongSign;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
