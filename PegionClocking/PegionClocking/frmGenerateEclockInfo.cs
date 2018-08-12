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
    public partial class frmGenerateEclockInfo : Form
    {
        public DataTable ClubList { get; set; }

        public frmGenerateEclockInfo()
        {
            InitializeComponent();
        }

        private void frmGenerateEclockInfo_Load(object sender, EventArgs e)
        {
            try
            {
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
                WriteResultInSDCard();
            }
            catch (Exception ex)
            {

                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void WriteResultInSDCard()
        {
            try
            {
                DriveInfo driveInfo = Common.Common.GetEclockSDCardDriveInfo();
                if (driveInfo != null)
                {
                    DirectoryInfo di = new DirectoryInfo(driveInfo.RootDirectory.ToString());
                    DirectoryInfo[] directoryList = di.GetDirectories();
                    foreach (DirectoryInfo item in directoryList)
                    {
                        if (item.Name == "ECLOCK")
                        {
                            BIZ.Member member = new BIZ.Member();
                            member.Name = this.txtAssignedTo.Text;
                            member.ReaderID = this.txtReaderID.Text;
                            member.ClubID = Convert.ToInt64(this.txtClubID.Text);
                            member.ClubName = this.cmbClub.Text;
                            member.Type = "Player";
                            if (rdbClub.Checked) member.Type = "Club";

                            if (member.ReaderRegistrationSave())
                            {
                                //application race result summary file
                                string systemConfig = item.Root + item.Name + "\\SystemConfig.inf";
                                if (!File.Exists(systemConfig)) File.Create(systemConfig).Close();

                                //clear club info File
                                File.WriteAllText(systemConfig, String.Empty);

                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(systemConfig, true))
                                {
                                    file.WriteLine("Electronic Bundy Clock");
                                    file.WriteLine("Version 1.0");
                                    file.WriteLine(this.cmbClub.Text);
                                    if (rdbClub.Checked)
                                    {
                                        file.WriteLine("Application Administrator");
                                        file.WriteLine(txtClubAbbreviation.Text);
                                        file.WriteLine(txtClubID.Text);
                                    }
                                    else
                                    {
                                        file.WriteLine("Application Member");
                                    }

                                    file.WriteLine(txtReaderID.Text);
                                    file.WriteLine(txtAssignedTo.Text);
                                    file.WriteLine("SMS Not Activated");
                                };

                                this.txtReaderID.Text = "";
                                this.txtAssignedTo.Text = "";
                                this.txtReaderID.Focus();
                                if (rdbClub.Checked)
                                {
                                    this.cmbClub.SelectedIndex = -1;
                                    this.txtClubID.Text = "0";
                                    this.txtClubAbbreviation.Text = "";
                                    this.rdbPlayer.Checked = true;
                                }
                                MessageBox.Show("Reader registration success.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void rdbClub_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Common.Global.IsMainDatabase = true;
            }
        }

        private void rdbPlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                Common.Global.IsMainDatabase = false;
            }
        }

    }
}
