using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class Region
    {
        #region Constant
        #endregion

        #region Variable
        DAL.Region region;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RegionID { get; set; }
        public String RegionName { get; set; }
        #endregion

        #region Public Methods
        public DataSet RegionGetByKey()
        {
            try
            {
                region = new DAL.Region();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = region.RegionGetByKey();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetRaceSeason()
        {
            try
            {
                region = new DAL.Region();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = region.GetRaceSeason();
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
                region = new DAL.Region();
                PopulateDataLayer();
                region.Save();
                MessageBox.Show("Region Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RegionSelectAll(DataGridView ScheduleList)
        {
            try
            {
                region = new DAL.Region();
                PopulateDataLayer();
                ScheduleList.DataSource = region.RegionSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean RegionDelete()
        {
            try
            {
                Boolean status = false;
                region = new DAL.Region();
                PopulateDataLayer();
                region.RegionDelete();
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
                region.RegionID = RegionID;
                region.ClubID = ClubID;
                region.UserID = UserID;
                region.RegionName = RegionName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
