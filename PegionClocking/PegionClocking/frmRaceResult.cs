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
    public partial class frmRaceResult : Form
    {

        #region Constant
        #endregion

        #region Variable
        BIZ.RaceCategory raceCategory;
        BIZ.RaceResult raceResult;
        //BIZ.ReportGeneration reportGeneration;
        #endregion

        #region Properties
        public Int64 ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public String RaceCategoryName { get; set; }
        public String RaceCategoryGroupName { get; set; }
        public DateTime DateReleased { get; set; }
        public String StickerCode { get; set; }
        public String PigeonID { get; set; }
        public String Name { get; set; }
        #endregion

        #region "Events"
        public frmRaceResult()
        {
            InitializeComponent();
           dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void frmRaceResult_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
            ClearControl();
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbCategory.Text != "" && cmbCategoryGroup.Text != "")
            {
                ViewResult();
            }
            else
            {
                MessageBox.Show("Please set your Category and Group", "Error");
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dataGridView1.DataSource;
                reportGeneration.Type = "RaceResult";
                reportGeneration.dtRecord = dt;
                reportGeneration.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddResult addResult = new frmAddResult();
                addResult.DateRelease = dateTimePicker1.Value.Date;
                addResult.ClubID = ClubID;
                addResult.Source = "Back-up";
                addResult.UserID = UserID;
                addResult.ShowDialog();
                ViewResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

        #region "Private Methods"
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                Int64 index;
                Int64 colIndex;
                String value = "";
                if (datagrid.RowCount > 0)
                {
                    raceResult = new BIZ.RaceResult();
                    index = datagrid.CurrentRow.Index;
                    colIndex = datagrid.CurrentCell.ColumnIndex;
                    value = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[10].Value);
                    if (value.ToUpper() == "DELETE")
                    {
                        StickerCode = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value);
                        PigeonID = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value);
                        if (StickerCode != "")
                        {
                            if (MessageBox.Show("Are you sure you would like to delete this transaction?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                raceResult.ClubID = ClubID;
                                raceResult.PigeonID = PigeonID;
                                raceResult.StickerCode = StickerCode;
                                raceResult.UserID = UserID;
                                raceResult.RaceResultDelete();
                                MessageBox.Show("Race result succesfully deleted", "Delete Record");
                                ViewResult();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateBusinessLayer(Common.Common.RaceResult Type)
        {
            try
            {
                switch (Type)
                {
                    case Common.Common.RaceResult.RaceCategory:
                        //raceCategory.ID = ID;
                        raceCategory.UserID = UserID;
                        raceCategory.ClubID = ClubID;
                        break;
                    case Common.Common.RaceResult.RaceResult:
                        //raceCategory.ID = ID;
                        raceResult.UserID = UserID;
                        raceResult.ClubID = ClubID;
                        raceResult.ReleasedDate = DateReleased;
                        raceResult.RaceCategoryGroupName = RaceCategoryGroupName;
                        raceResult.RaceCategoryName = RaceCategoryName;
                        raceResult.Name = Name;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void PopulateCombobox()
        {
            try
            {
                raceCategory = new BIZ.RaceCategory();
                PopulateBusinessLayer(Common.Common.RaceResult.RaceCategory);

                //Race Schedule
                DataTable dtRaceCategory;
                DataTable dtRaceCategoryGroup;

                dtRaceCategory = raceCategory.RaceCategoryGetByKey().Tables[0];
                dtRaceCategoryGroup = raceCategory.RaceCategoryGetByKey().Tables[1];

                if (dtRaceCategory.Rows.Count > 0)
                {
                    cmbCategory.Items.Add("All");
                    cmbCategoryGroup.Items.Add("All");
                    foreach (DataRow dtrow in dtRaceCategory.Rows)
                    {
                        cmbCategory.Items.Add(dtrow["Description"].ToString());
                    }
                }
                if (dtRaceCategoryGroup.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dtRaceCategoryGroup.Rows)
                    {
                        cmbCategoryGroup.Items.Add(dtrow["RaceCategoryGroupName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void GetControlValue()
        {
            DateReleased = this.dateTimePicker1.Value;
            RaceCategoryGroupName = cmbCategoryGroup.Text;
            RaceCategoryName = cmbCategory.Text;
            Name = this.textBox1.Text;
        }
        private void ClearControl()
        {
            lblLocation.Text = "";
            lblCorrdinates.Text = "";
            lblLap.Text = "";
            lblTotalBirds.Text = "";
            lblTotalSMSCount.Text = "";
            label6.Text = "";
        }
        private void ViewResult()
        {
            try
            {
                DataSet dtResult = new DataSet();
                raceResult = new BIZ.RaceResult();
                String version = "";

                ClearControl();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceResult.RaceResult);
                dtResult = raceResult.RaceResultGetByKey();
                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        lblLocation.Text = dtResult.Tables[0].Rows[0]["LocationName"].ToString();
                        lblCorrdinates.Text = dtResult.Tables[0].Rows[0]["Coordinates"].ToString();
                        lblLap.Text = dtResult.Tables[0].Rows[0]["Lap"].ToString();
                        lblTotalBirds.Text = dtResult.Tables[0].Rows[0]["TotalBird"].ToString();
                        lblTotalSMSCount.Text = dtResult.Tables[0].Rows[0]["SMSCount"].ToString();
                        label6.Text = "0 %";
                        if (lblTotalBirds.Text != "0" && lblTotalBirds.Text != "" && lblTotalSMSCount.Text != "")
                        {
                            label6.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Decimal.Parse(lblTotalSMSCount.Text) / Decimal.Parse(lblTotalBirds.Text) * 100),2)) + " %";
                        }
                        version = dtResult.Tables[0].Rows[0]["Version"].ToString();
                    }

                    this.dataGridView1.DataSource = dtResult.Tables[1];
                    if (version != "1")
                    {
                        if (dtResult.Tables[1].Rows.Count > 0)
                        {
                            dataGridView1.Columns[6].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
                            //dataGridView1.Columns[2].DefaultCellStyle.Format = "#,##0.00";
                            dataGridView1.Columns[2].DefaultCellStyle.Format = "#,##0.00";
                            dataGridView1.Columns[3].DefaultCellStyle.Format = "#,##0.00";
                            dataGridView1.Columns[0].Visible = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            frmAddResult addResult = new frmAddResult();
            addResult.DateRelease = dateTimePicker1.Value.Date;
            addResult.ClubID = ClubID;
            addResult.Source = "Bundy Clock";
            addResult.Text = "Add Race Result From Bundy Clock";
            addResult.ShowDialog();
            ViewResult();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dtResult = new DataSet();
                raceResult = new BIZ.RaceResult();

                ClearControl();
                GetControlValue();
                PopulateBusinessLayer(Common.Common.RaceResult.RaceResult);
                dtResult = raceResult.ReComputeResult();

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show(dtResult.Tables[0].Rows[0]["Remarks"].ToString(), "Re-Compute Result");
                    } 
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
    }
}
