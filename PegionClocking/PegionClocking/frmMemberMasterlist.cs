using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PegionClocking
{
    public partial class frmMemberMasterlist : Form
    {
        #region Variable
        BIZ.Member member;
        //BIZ.ReportGeneration reportGeneration;
        #endregion

        #region Properties
        public String ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }

        public string MemberID { get; set; }
        public string MemberIDNo { get; set; }
        //public string Name { get; set; }
        public string MobileNumber { get; set; }

        #endregion

        #region Events
        public frmMemberMasterlist()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void frmMemberMasterlist_Load(object sender, EventArgs e)
        {
            MemberDetailsSelectAll();
        }
        #endregion

        #region Private Methods
        private void MemberDetailsSelectAll()
        {
            member = new BIZ.Member();
            PopulateBussinessLayer();
            member.MemberDetailsSelectAll(this.dataGridView1);
            this.lblRecordCount.Text = dataGridView1.Rows.Count.ToString();
        }
        private void PopulateBussinessLayer()
        {
            try
            {
                member.ClubID = ClubID;
                member.MemberIDNo = txtMemberID.Text;
                member.ID =Convert.ToString(ID);
                member.MobileNumber = txtMobileNumber.Text;
                member.Name = txtMemberName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    member = new BIZ.Member();
                    index = datagrid.CurrentRow.Index;
                    ID = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                    if ( Convert.ToInt64(ID) > 0)
                    {
                        DataTable dtresult = new DataTable();
                        DataTable dtResultSMS = new DataTable();
                        PopulateBussinessLayer();
                        dtresult = member.MemberDetailsSearchByKey().Tables[0];
                        dtResultSMS = member.MemberDetailsSearchByKey().Tables[1];

                        if (dtresult.Rows.Count > 0)
                        {
                            frmMemberDataEntry memberDataentry = new frmMemberDataEntry();
                            memberDataentry.ClubID = ClubID;
                            memberDataentry.UserID = UserID;
                            memberDataentry.RecordSearched = dtresult;
                            memberDataentry.RecordSearchedSMS = dtResultSMS;
                            memberDataentry.IsEdit = true;
                            memberDataentry.PopulateControl();
                            memberDataentry.ShowDialog();
                            MemberDetailsSelectAll(); //Refresh value of data grid
                        }
                        else
                        {
                            MessageBox.Show("No record is found", "Search");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateControl(DataTable dt)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dataGridView1.DataSource;
                reportGeneration.Type = "Masterlist";
                reportGeneration.dtRecord = dt;
                reportGeneration.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ID=txtID.Text;
            MemberDetailsSelectAll();
        }
        
    }
}
