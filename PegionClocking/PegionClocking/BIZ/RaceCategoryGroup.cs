using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class RaceCategoryGroup
    {
        #region Constant
        #endregion

        #region Variable
        DAL.RaceCategoryGroup raceCategoryGroup;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String MemberID { get; set; }
        public String EntryID { get; set; }
        public Int64 RaceCategoryGroupID { get; set; }
        public String RaceCategoryGroupName { get; set; }
        public String EntryList { get; set; }
        #endregion

        #region Public Methods
        public DataSet AddEntryCategory()
        {
            try
            {
                DataSet dtResult = new DataSet();
                raceCategoryGroup = new DAL.RaceCategoryGroup();
                PopulateDataLayer();
                dtResult = raceCategoryGroup.AddEntryCategory();
                MessageBox.Show("Entry Category Successfully Save!", "Add Record");
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RemoveEntryCategory()
        {
            try
            {
                DataSet dtResult = new DataSet();
                raceCategoryGroup = new DAL.RaceCategoryGroup();
                PopulateDataLayer();
                dtResult = raceCategoryGroup.RemoveEntryCategory();
                MessageBox.Show("Entry Category Successfully Removed!", "Add Record");
                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetEntryCategory()
        {
            try
            {
                DataSet dtResult = new DataSet();
                raceCategoryGroup = new DAL.RaceCategoryGroup();
                PopulateDataLayer();
                dtResult = raceCategoryGroup.GetEntryCategory();
                return dtResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet RaceCategoryGroupGetByKey()
        {
            try
            {
                raceCategoryGroup = new DAL.RaceCategoryGroup();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = raceCategoryGroup.RaceCategoryGroupGetByKey();
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
                raceCategoryGroup = new DAL.RaceCategoryGroup();
                PopulateDataLayer();
                raceCategoryGroup.Save();
                MessageBox.Show("Race Category Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RaceCategoryGroupSelectAll(DataGridView ScheduleList)
        {
            try
            {
                raceCategoryGroup = new DAL.RaceCategoryGroup();
                PopulateDataLayer();
                ScheduleList.DataSource = raceCategoryGroup.RaceCategoryGroupSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean RaceCategoryGroupDelete()
        {
            try
            {
                Boolean status = false;
                raceCategoryGroup = new DAL.RaceCategoryGroup();
                PopulateDataLayer();
                raceCategoryGroup.RaceCategoryGroupDelete();
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
                raceCategoryGroup.RaceCategoryGroupID = RaceCategoryGroupID;
                raceCategoryGroup.ClubID = ClubID;
                raceCategoryGroup.UserID = UserID;
                raceCategoryGroup.RaceCategoryGroupName = RaceCategoryGroupName;
                raceCategoryGroup.EntryID = EntryID;
                raceCategoryGroup.MemberID = MemberID;
                raceCategoryGroup.EntryList = EntryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
