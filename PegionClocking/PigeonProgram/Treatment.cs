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
    public partial class Treatment : Form
    {

        #region Properties
        public Int64 PigeonID { get; set; }
        public Int64 TreatmentID { get; set; }
        public String PigeonName { get; set; }
        #endregion

        public Treatment()
        {
            InitializeComponent();
            dtTreatmentList.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void Treatment_Load(object sender, EventArgs e)
        {
            try
            {
                txtPigeonName.Text = PigeonName;
                LoadTreatmentList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
         
        }
        private void LoadTreatmentList()
        {
            try
            {
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.PigeonID = PigeonID;
                dtTreatmentList.DataSource = pigeonDetails.TreatmentGetAll().Tables[0];

                dtTreatmentList.Columns[0].Visible = false;
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(Font, FontStyle.Bold);
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
                pigeonDetails.TreatmentID = TreatmentID;
                pigeonDetails.Treatment = txtTreatment.Text;
                pigeonDetails.TreatmentDate = dtpTreatMentDate.Value;
                pigeonDetails.Illness = txtIllness.Text;
                pigeonDetails.Remarks = txtRemarks.Text;
                pigeonDetails.TreatmentSave();

                //load list
                LoadTreatmentList();

                //clear
                ClearControl();

                MessageBox.Show("Treatment successfully save.", "Save");
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
                pigeonDetails.TreatmentID = TreatmentID;
                pigeonDetails.TreatmentDelete();
                //MessageBox.Show("Treatment Successfully Deleted", "Delete Record");
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
                pigeonDetails.TreatmentID = TreatmentID;
                dtResult = pigeonDetails.TreatmentGetByKey();

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        txtTreatment.Text = dtResult.Tables[0].Rows[0]["Treatment"].ToString();
                        this.dtpTreatMentDate.Value = (DateTime)dtResult.Tables[0].Rows[0]["TreatmentDate"];
                        txtIllness.Text = dtResult.Tables[0].Rows[0]["Illness"].ToString();
                        txtRemarks.Text = dtResult.Tables[0].Rows[0]["Remarks"].ToString();
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
                txtTreatment.Text = "";
                this.dtpTreatMentDate.Value = DateTime.Now;
                txtIllness.Text = "";
                txtRemarks.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
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

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dtTreatmentList;
                DataSet dtResult = new DataSet();
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    BIZ.PigeonDetails pigenDetails = new BIZ.PigeonDetails();
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "EDIT")
                    {
                        TreatmentID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (TreatmentID > 0)
                        {
                            GetDetailsByKey();
                        }
                    }
                    else if ((string)datagrid.CurrentCell.Value.ToString() == "DELETE")
                    {
                        TreatmentID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (TreatmentID > 0)
                        {
                            DialogResult dialogResult = new DialogResult();
                            dialogResult = MessageBox.Show("Are you sure you want to delete this Treatment?", "Error", MessageBoxButtons.YesNo);

                            if (dialogResult == DialogResult.Yes)
                            {
                                Delete(); ;
                                LoadTreatmentList();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
