using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PigeonProgram
{
    public partial class Race_Result : Form
    {
        #region Properties
        public Int64 PigeonID { get; set; }
        public Int64 RaceResultID { get; set; }
        public String PigeonName { get; set; }
        #endregion

        public Race_Result()
        {
            InitializeComponent();
            dtRaceResult.DoubleClick += new EventHandler(grid_DoubleClick);
        }
        private void Race_Result_Load(object sender, EventArgs e)
        {
            try
            {
                txtPigeonName.Text = PigeonName;
                LoadRaceResultList();
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtRaceResult;
                DataSet dtResult = new DataSet();
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    BIZ.PigeonDetails pigenDetails = new BIZ.PigeonDetails();
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                    {
                        RaceResultID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (RaceResultID > 0)
                        {
                            GetDetailsByKey();
                        }
                    }
                    else if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                    {
                        RaceResultID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (RaceResultID > 0)
                        {
                            DialogResult dialogResult = new DialogResult();
                            dialogResult = MessageBox.Show("Are you sure you want to delete this Result?", "Error", MessageBoxButtons.YesNo);

                            if (dialogResult == DialogResult.Yes)
                            {
                                Delete(); ;
                                LoadRaceResultList();
                                ClearControl();
                                MessageBox.Show("Record has been deleted", "Delete Record");
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

        private void LoadRaceResultList()
        {
            try
            {
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.PigeonID = PigeonID;
                dtRaceResult.DataSource = pigeonDetails.RaceResultGetAll().Tables[0];

                dtRaceResult.Columns[0].Visible = false;

                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(Font, FontStyle.Bold);
                //dtRaceResult.Columns[7].DefaultCellStyle = style;
                //dtRaceResult.Columns[8].DefaultCellStyle = style;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Save()
        {
            try
            {
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.PigeonID = PigeonID;
                pigeonDetails.ReleasePoint = txtReleasePoint.Text;
                pigeonDetails.ReleaseDate = dtpRaceDate.Value;
                pigeonDetails.WeatherCondition = txtWeatherCondition.Text;
                pigeonDetails.BirdEntry = txtBirdEntry.Text;
                pigeonDetails.BirdClock = txtBirdClock.Text;
                pigeonDetails.Rank = txtRank.Text;
                pigeonDetails.Distance = txtDistance.Text;
                pigeonDetails.Flight = txtFlight.Text;
                pigeonDetails.Speed = txtSpeed.Text;
                pigeonDetails.Remarks = txtRemarks.Text;
                pigeonDetails.RaceResultSave();

                //load list
                LoadRaceResultList();

                //clear control
                ClearControl();

                MessageBox.Show("Race Result Save", "Record Save");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Delete()
        {
            try
            {
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.RaceResultID = RaceResultID;
                pigeonDetails.RaceResultDelete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetDetailsByKey()
        {
            try
            {
                DataSet dtResult = new DataSet();
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.RaceResultID = RaceResultID;
                dtResult = pigeonDetails.RaceResultGetByKey();

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        txtDistance.Text = dtResult.Tables[0].Rows[0]["Distance"].ToString();
                        txtBirdClock.Text = dtResult.Tables[0].Rows[0]["BirdClock"].ToString();
                        txtBirdEntry.Text = dtResult.Tables[0].Rows[0]["BirdEntry"].ToString();
                        txtFlight.Text = dtResult.Tables[0].Rows[0]["Flight"].ToString();
                        dtpRaceDate.Value =(DateTime)dtResult.Tables[0].Rows[0]["ReleaseDate"];
                        txtRank.Text = dtResult.Tables[0].Rows[0]["Rank"].ToString();
                        txtReleasePoint.Text = dtResult.Tables[0].Rows[0]["ReleasePoint"].ToString();
                        txtRemarks.Text = dtResult.Tables[0].Rows[0]["Remarks"].ToString();
                        txtSpeed.Text = dtResult.Tables[0].Rows[0]["Speed"].ToString();
                        txtWeatherCondition.Text = dtResult.Tables[0].Rows[0]["WeatherCondition"].ToString();
                    }
                } 
            }
            catch (Exception ex)
            { 
                throw ex;
            }

        }
        private void ClearControl()
        {
            try
            {
                //PigeonID = 0;
                //txtPigeonName.Text = "";
                txtReleasePoint.Text = "";
                dtpRaceDate.Value = DateTime.Now;
                txtWeatherCondition.Text = "";
                txtBirdEntry.Text = "";
                txtBirdClock.Text = "";
                txtRank.Text = "";
                txtSpeed.Text = "";
                txtFlight.Text = "";
                txtRemarks.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Record");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
