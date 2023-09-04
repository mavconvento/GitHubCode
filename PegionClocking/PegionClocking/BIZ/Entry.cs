using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class Entry
    {
        #region Constant
        #endregion

        #region Variable
        DAL.Entry entry;
        DAL.RaceReleasePoint raceReleasePoint;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public string RaceScheduleName { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public string RaceCategoryName { get; set; }
        public string RaceCategoryGroupName { get; set; }
        public Int64 EntryID { get; set; }
        public Int64 MemberID { get; set; }
        public string StickerCode { get; set; }
        public Int64 BandID { get; set; }
        public string RingNumber { get; set; }
        public String RFID { get; set; }
        public string BarcodeBandID { get; set; }
        public DateTime ReleaseDate { get; set; }
        #endregion

        #region Public Methods
        public DataTable EClockRegisterRFID()
        {
            try
            {
                DataTable dtResult = new DataTable();
                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                dtResult = entry.EClockRegisterRFID().Tables[0];
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable EClockRegisterRFIDMember()
        {
            try
            {
                DataTable dtResult = new DataTable();
                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                dtResult = entry.EClockRegisterRFIDMember().Tables[0];
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetEntryDetailsByEntryBarcodeID()
        {
            try
            {
                DataSet dtResult = new DataSet();
                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                dtResult = entry.GetEntryDetailsByEntryBarcodeID();
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetLastEntry()
        {
            try
            {
                DataSet dtResult = new DataSet();
                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                dtResult = entry.GetLastEntry();
                return dtResult;
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
                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                entry.Save();
                MessageBox.Show("Race Entry Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean SaveDuplicateEntry()
        {
            try
            {
                Boolean status = false;
                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                entry.SaveDuplicateEntry();
                MessageBox.Show("Race Entry Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EntryGetByRaceReleasePoint(DataGridView entryList,Label lblcount)
        {
            try
            {
                DataSet dtResult = new DataSet();
                DataTable dtEnrtry = new DataTable();

                raceReleasePoint = new DAL.RaceReleasePoint();
                PopulateDataLayer("releasepoint");
                dtResult = raceReleasePoint.RaceReleasePointSummary();

                if (dtResult.Tables.Count > 0)
                {
                    dtEnrtry = dtResult.Tables[0];
                    entryList.DataSource = dtEnrtry;
                    lblcount.Text = string.Format("{0:#,##0}", dtEnrtry.Rows.Count.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EntryGetByMemberIDNo(DataGridView entryList, Label lblcount)
        {
            try
            {
                DataSet dtResult = new DataSet();
                DataTable dtEnrtry = new DataTable();

                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                dtResult = entry.EntryGetByMemberIDNo();

                if (dtResult.Tables.Count > 0)
                {
                    dtEnrtry = dtResult.Tables[0];
                    entryList.DataSource = dtEnrtry;
                    lblcount.Text = string.Format("{0:#,##0}", dtEnrtry.Rows.Count.ToString());
                    entryList.Columns[0].Visible = false;
                    entryList.Columns[4].Visible = false;
                    entryList.Columns[5].Visible = false;
                    entryList.Columns[6].Visible = false;
                    entryList.Columns[9].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetStickerCredit()
        {
            try
            {
                DataSet dtResult = new DataSet();
                DataTable dtEnrtry = new DataTable();

                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                return entry.GetStickerCredit();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean EntryDelete()
        {
            try
            {
                Boolean status = false;
                entry = new DAL.Entry();
                PopulateDataLayer("entry");
                entry.MemberDetailsDelete();
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
        private void PopulateDataLayer(string type)
        {
            try
            {
                if (type == "entry")
                {
                    entry.ClubID = ClubID;
                    entry.UserID = UserID;
                    entry.EntryID = EntryID;
                    entry.RaceReleasePointID = RaceReleasePointID;
                    entry.RaceScheduleName = RaceScheduleName;
                    entry.RaceScheduleCategoryName = RaceScheduleCategoryName;
                    entry.RaceCategoryName = RaceCategoryName;
                    entry.RaceCategoryGroupName = RaceCategoryGroupName;
                    entry.StickerCode = StickerCode;
                    entry.BandID = BandID;
                    entry.RingNumber = RingNumber;
                    entry.BarcodeBandID = BarcodeBandID;
                    entry.ReleaseDate = ReleaseDate;
                    entry.MemberID = MemberID;
                    entry.RFIDCode = RFID;
                }
                else if (type == "releasepoint")
                {
                    raceReleasePoint.ClubID = ClubID;
                    raceReleasePoint.UserID = UserID;
                    raceReleasePoint.RaceReleasePointID = RaceReleasePointID;
                    raceReleasePoint.RaceCategoryName = RaceCategoryName;
                    raceReleasePoint.RaceCategoryGroupName = RaceCategoryGroupName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
