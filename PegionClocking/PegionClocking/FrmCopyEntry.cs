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
    public partial class FrmCopyEntry : Form
    {
        #region Variables
        BIZ.Entry entry;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String RaceScheduleName { get; set; }
        public String RaceScheduleCategoryName { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 EntryID { get; set; }
        #endregion

        #region Events
        public FrmCopyEntry()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(DTEntryList_DoubleClick);
        }

        private void FrmCopyEntry_Load(object sender, EventArgs e)
        {
            try
            {
                GetLastEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDuplicateEntry();
                GetLastEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

        #region Methods
        private void DTEntryList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    //member = new BIZ.Member();
                    index = datagrid.CurrentRow.Index;
                    EntryID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    if (EntryID > 0)
                    {
                        if ((string)datagrid.CurrentCell.Value.ToString() == " + ")
                        {
                            txtBandNumber.Text = datagrid.Rows[Convert.ToInt32(index)].Cells[1].Value.ToString();
                            txtStickerCode.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetLastEntry()
        {
            try
            {
                DataSet dtResult = new DataSet();
                entry = new BIZ.Entry();
                entry.RaceScheduleCategoryName = RaceScheduleCategoryName;
                entry.MemberID = MemberID;
                entry.ClubID = ClubID;
                entry.RaceReleasePointID = RaceReleasePointID;
                dtResult = entry.GetLastEntry();

                if (dtResult.Tables.Count > 0)
                {
                    dataGridView1.DataSource = dtResult.Tables[0];
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SaveDuplicateEntry()
        {
            try
            {
                if (txtBandNumber.Text != "" && txtStickerCode.Text != "")
                {
                    entry = new BIZ.Entry();
                    entry.ClubID = ClubID;
                    entry.UserID = UserID;
                    entry.EntryID = EntryID;
                    entry.MemberID = MemberID;
                    entry.RaceScheduleName = RaceScheduleName;
                    entry.RaceScheduleCategoryName = RaceScheduleCategoryName;
                    entry.RaceReleasePointID = RaceReleasePointID;
                    entry.StickerCode = txtStickerCode.Text;
                    entry.RingNumber = txtBandNumber.Text;
                    entry.SaveDuplicateEntry();
                }
                else
                {
                    string error = "";
                    if (txtBandNumber.Text == "")
                    {
                        error = "No entry is selected!";
                    }
                    else
                    {
                        error = "Please enter stickercode";
                    }
                    MessageBox.Show(error, "Duplicate Error");
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
