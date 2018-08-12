using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PegionClocking
{
    public partial class frmEntryMultipleCategory : Form
    {
        #region Variables
        BIZ.RaceCategoryGroup raceCategoryGroup;
        BIZ.RaceCategory raceCategory;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public String EntryID { get; set; }
        public String BandNumber { get; set; }
        public String StickerCode { get; set; }
        public String MemberID { get; set; }
        public Int64 ClubID { get; set; }
        public String OrigCategory { get; set; }
        #endregion

        #region Events
        public frmEntryMultipleCategory()
        {
            InitializeComponent();
        }

        private void frmEntryMultipleCategory_Load(object sender, EventArgs e)
        {
            LoadDetails();
            PopulateCombobox();
            GetCategoryList();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SaveCategory();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RemoveCategory();
        }
        #endregion

        #region Private Methods
        private void PopulateCombobox()
        {
            try
            {
                raceCategory = new BIZ.RaceCategory();
                PopulateBusinessLayer(Common.Common.RaceEntryClassType.RaceCategory);

                //Race Schedule
                DataTable dtRaceCategoryGroup;
                dtRaceCategoryGroup = raceCategory.RaceCategoryGetByKey().Tables[1];

                if (dtRaceCategoryGroup.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtRaceCategoryGroup.Rows)
                    {
                        if (OrigCategory != dtrow["RaceCategoryGroupName"].ToString())
                        {
                            cmbCategoryList.Items.Add(dtrow["RaceCategoryGroupName"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PopulateBusinessLayer(Common.Common.RaceEntryClassType Type)
        {
            try
            {
                switch (Type)
                {
                    case Common.Common.RaceEntryClassType.RaceCategory:
                        raceCategory.UserID = UserID;
                        raceCategory.ClubID = ClubID;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void LoadDetails()
        {
            try
            {
                txtBandNumber.Text = BandNumber;
                txtStickerCode.Text = StickerCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveCategory()
        {
            try
            {
                raceCategoryGroup = new BIZ.RaceCategoryGroup();
                raceCategoryGroup.ClubID = ClubID;
                raceCategoryGroup.EntryID = EntryID;
                raceCategoryGroup.MemberID = MemberID;
                raceCategoryGroup.RaceCategoryGroupName = this.cmbCategoryList.Text;
                SetCategoryList(raceCategoryGroup.AddEntryCategory());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RemoveCategory()
        {
            try
            {
                raceCategoryGroup = new BIZ.RaceCategoryGroup();
                raceCategoryGroup.ClubID = ClubID;
                raceCategoryGroup.EntryID = EntryID;
                raceCategoryGroup.MemberID = MemberID;
                raceCategoryGroup.RaceCategoryGroupName = this.cmbCategoryList.Text;
                SetCategoryList(raceCategoryGroup.RemoveEntryCategory());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void GetCategoryList()
        {
            try
            {
                raceCategoryGroup = new BIZ.RaceCategoryGroup();
                raceCategoryGroup.ClubID = ClubID;
                raceCategoryGroup.EntryID = EntryID;
                raceCategoryGroup.MemberID = MemberID;
                raceCategoryGroup.RaceCategoryGroupName = this.cmbCategoryList.Text;
                SetCategoryList(raceCategoryGroup.GetEntryCategory());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SetCategoryList(DataSet dtResult)
        {
            try
            {
                lstCategory.Items.Clear();
                cmbCategoryList.Text = "";
                foreach (DataRow item in dtResult.Tables[0].Rows)
                {
                    lstCategory.Items.Add(item["CategoryName"]);
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
