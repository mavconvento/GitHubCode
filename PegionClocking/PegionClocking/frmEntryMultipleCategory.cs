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
        public String EntryList { get; set; }
        public String BandNumberList { get; set; }
        public String Action { get; set; }
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
                if (!String.IsNullOrEmpty(EntryList))
                {
                    string[] bandlist = BandNumberList.Split('|');
                    foreach (string item in bandlist)
                    {
                        listBox1.Items.Add(item);
                        this.listBox1.Visible = true;
                        this.label2.Visible = false;
                        this.txtBandNumber.Visible = false;
                        this.txtStickerCode.Visible = false;
                    }
                }
                else
                {
                    txtBandNumber.Text = BandNumber;
                    txtStickerCode.Text = StickerCode;
                    this.listBox1.Visible = false;
                }
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
                if (this.cmbCategoryList.Text != "")
                {
                    raceCategoryGroup = new BIZ.RaceCategoryGroup();
                    raceCategoryGroup.ClubID = ClubID;
                    raceCategoryGroup.EntryID = EntryID;
                    raceCategoryGroup.MemberID = MemberID;
                    raceCategoryGroup.RaceCategoryGroupName = this.cmbCategoryList.Text;
                    raceCategoryGroup.EntryList = EntryList;
                    Action = "ADD";
                    SetCategoryList(raceCategoryGroup.AddEntryCategory());
                }
                
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
                if (this.cmbCategoryList.Text != "")
                {
                    Action = "REMOVE";
                    raceCategoryGroup = new BIZ.RaceCategoryGroup();
                    raceCategoryGroup.ClubID = ClubID;
                    raceCategoryGroup.EntryID = EntryID;
                    raceCategoryGroup.MemberID = MemberID;
                    raceCategoryGroup.RaceCategoryGroupName = this.cmbCategoryList.Text;
                    raceCategoryGroup.EntryList = EntryList;
                    SetCategoryList(raceCategoryGroup.RemoveEntryCategory());
                }
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
                if (EntryList != "")
                {
                    if (lstCategory.Items.IndexOf(cmbCategoryList.Text) > -1)
                    {
                        if (Action == "REMOVE") lstCategory.Items.Remove(cmbCategoryList.Text); 
                    }
                    else
                    {
                        if (Action == "ADD") lstCategory.Items.Add(cmbCategoryList.Text);
                    }
                }
                else
                {
                    lstCategory.Items.Clear();
                    foreach (DataRow item in dtResult.Tables[0].Rows)
                    {
                        lstCategory.Items.Add(item["CategoryName"]);
                    }
                }

                cmbCategoryList.Text = "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
