using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Eclock
{
    public partial class frmSetup : Form
    {
        public frmSetup()
        {
            InitializeComponent();
        }
        private void frmSetup_Load(object sender, EventArgs e)
        {
            try
            {
                ReadConfiguration();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void frmSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            BIZ.Common Common = new BIZ.Common();
            Common.CloseSubForm(this);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    if (txtMobileNumber.Text == "")
                    {
                        MessageBox.Show("Please enter mobile number.");
                        return;
                    }

                }
                string sysDir = AppDomain.CurrentDomain.BaseDirectory;
                string configurationPath = sysDir + "SystemConfig.inf";

                //clear club info File
                File.WriteAllText(configurationPath, String.Empty);

                //write club info in clubinfo file
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(configurationPath, true))
                {
                    file.WriteLine("Electronic Bundy Clock");
                    file.WriteLine("Application Member");
                    file.WriteLine("Version 1.0");
                    file.WriteLine(this.txtClubName.Text);
                    file.WriteLine(txtReaderID.Text);
                    file.WriteLine(txtPlayerName.Text);

                    if (this.checkBox1.Checked)
                    {
                        file.WriteLine("SMS Activated");
                        file.WriteLine(txtMobileNumber.Text);
                    }
                    else
                        file.WriteLine("SMS Not Activated");
                };

                MessageBox.Show("Configuration Save.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.checkBox1.Checked)
                {
                    this.txtMobileNumber.Enabled = true;
                    this.txtMobileNumber.Focus();
                }
                else
                {
                    this.txtMobileNumber.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        //private methods...
        private void GetClubInfoList()
        {
            try
            {
                DriveInfo driveInfo = BIZ.Common.GetEclockSDCardDriveInfo();
                if (driveInfo != null)
                {
                    DirectoryInfo di = new DirectoryInfo(driveInfo.RootDirectory.ToString());
                    DirectoryInfo[] directoryList = di.GetDirectories();
                    foreach (DirectoryInfo item in directoryList)
                    {
                        if (item.Name == "ECLOCK")
                        {
                            DirectoryInfo diEclock = new DirectoryInfo(item.Root + item.Name);
                            DirectoryInfo[] directorylistEclock = diEclock.GetDirectories();
                            foreach (DirectoryInfo eclockfolderitem in directorylistEclock)
                            {
                                string eclockInfoFile = eclockfolderitem.Root + item.Name + "\\" + eclockfolderitem.Name + "\\" + "ClubInfo.inf";
                                if (File.Exists(eclockInfoFile))
                                {
                                    TextReader tr = new StreamReader(eclockInfoFile);
                                    using (tr)
                                    {
                                        string[] clubinfo = tr.ReadLine().Split('|');
                                        this.listBox1.Items.Add(clubinfo[1].ToString() + " - " + clubinfo[2].ToString());
                                    }
                                }

                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void ReadConfiguration()
        {
            try
            {
                string sysDir = AppDomain.CurrentDomain.BaseDirectory;
                string fullpath = sysDir + "SystemConfig.inf";
                string systemName = "";
                string systemType = "";
                string systemVersion = "";

                //initial state
                this.checkBox1.Checked = false;
                this.txtMobileNumber.Enabled = false;

                if (File.Exists(fullpath))
                {
                    TextReader tr = new StreamReader(fullpath);
                    using (tr)
                    {
                        this.Text = this.Text + " (" + tr.ReadLine();
                        systemType = tr.ReadLine();
                        this.Text = this.Text + " " + tr.ReadLine() + ")";
                        this.txtClubName.Text = tr.ReadLine();
                        this.txtReaderID.Text = tr.ReadLine();
                        this.txtPlayerName.Text = tr.ReadLine();
                        if (tr.ReadLine() == "SMS Activated")
                        {
                            this.checkBox1.Checked = true;
                            this.txtMobileNumber.Enabled = true;
                            this.txtMobileNumber.Text = tr.ReadLine();
                        }
                    }
                    GetClubInfoList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
