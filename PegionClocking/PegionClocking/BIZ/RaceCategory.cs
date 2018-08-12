using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class RaceCategory
    {
        #region Constant
        #endregion

        #region Variable
        DAL.RaceCategory raceCategory;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RaceCategoryID { get; set; }
        public String RaceCategoryName { get; set; }
        public Boolean IsFromEntry { get; set; }
        #endregion

        #region Public Methods
        public DataSet RaceCategoryGetByKey()
        {
            try
            {
                raceCategory = new DAL.RaceCategory();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = raceCategory.RaceCategoryGetByKey();
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
                raceCategory = new DAL.RaceCategory();
                PopulateDataLayer();
                raceCategory.Save();
                MessageBox.Show("Race Category Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RaceCategorySelectAll(DataGridView ScheduleList)
        {
            try
            {
                raceCategory = new DAL.RaceCategory();
                PopulateDataLayer();
                ScheduleList.DataSource = raceCategory.RaceCategorySelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean RaceCategoryDelete()
        {
            try
            {
                Boolean status = false;
                raceCategory = new DAL.RaceCategory();
                PopulateDataLayer();
                raceCategory.RaceCategoryDelete();
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
                raceCategory.RaceCategoryID = RaceCategoryID;
                raceCategory.ClubID = ClubID;
                raceCategory.UserID = UserID;
                raceCategory.RaceCategoryName = RaceCategoryName;
                raceCategory.IsFromEntry = IsFromEntry;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
