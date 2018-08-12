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
    public partial class frmRFIDRegistration : Form
    {
        public DataTable ClubList { get; set; }
        public frmRFIDRegistration()
        {
            InitializeComponent();
        }

        private void frmRFIDRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Global.IsMainDatabase = true;
                PopulateCombobox();
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
                DataTable dtResult;
                BIZ.Club club = new BIZ.Club();
                dtResult = club.ClubSelectAll();
                ClubList = dtResult;
                foreach (DataRow dr in dtResult.Rows)
                {
                    cmbClub.Items.Add(dr["Club Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void cmbClub_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow item in ClubList.Rows)
                {
                    if (((ComboBox)sender).Text == item["Club Name"].ToString())
                    {
                        this.txtClubID.Text = item["Club ID"].ToString();
                        this.txtClubAbbreviation.Text = item["Club Abbreviation"].ToString();
                        this.txtReaderID.Focus();
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
            try
            {
                BIZ.Member member = new BIZ.Member();
                member.ReaderID = this.txtReaderID.Text;
                member.ClubID = Convert.ToInt64(this.txtClubID.Text);
                member.ClubName = this.cmbClub.Text;
                member.Overwrite = false;
                if (this.checkBox1.Checked) member.Overwrite = true;
                if (member.RFIDRegisterSave())
                {
                    this.checkBox1.Checked = false;
                    txtReaderID.Text = "";
                    txtReaderID.Focus();
                    MessageBox.Show("RFID registration success.");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
    }
}
