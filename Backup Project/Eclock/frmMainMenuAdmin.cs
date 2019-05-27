using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Eclock
{
    public partial class frmMainMenuAdmin : Form
    {
        #region Variable
        string ApplicationDirectory;
        string FullPath;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public String ClubAbbreviation { get; set; }
        public Int64 UserID { get; set; }
        #endregion

        #region Events
        public frmMainMenuAdmin()
        {
            InitializeComponent();
        }

        private void frmMainMenuAdmin_Load(object sender, EventArgs e)
        {
            if (!BIZ.Common.CheckSystemConfig("OwnerInfo"))
            {
                if (!BIZ.Common.GetSystemConfig("OwnerInfo"))
                {
                    MessageBox.Show("Please insert your MAVC Eclock SD Card.");
                    this.Close();
                }
            }
            //Get Owner Info
            GetOwnerInfo();
        }

        private void btnTraining_Click(object sender, EventArgs e)
        {
            frmMainMenuAdminRegistration form = new frmMainMenuAdminRegistration();
            form.Parent = this;
            OpenForm(form);
        }

        private void btnRace_Click(object sender, EventArgs e)
        {
            frmMainMenuAdminEntry form = new frmMainMenuAdminEntry();
            form.Parent = this;
            OpenForm(form);
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            try
            {
                frmMainMenuAdminImportData frmImportData = new frmMainMenuAdminImportData();
                frmImportData.Parent = this;
                OpenForm(frmImportData);
            }
            catch (Exception ex)
            {

                MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion

        #region Private Methods
        private void OpenForm(Form form)
        {
            BIZ.Common Common = new BIZ.Common();
            Common.OpenForm(form, this);
        }

        private void GetOwnerInfo()
        {
            try
            {
                ApplicationDirectory = BIZ.Common.GetApplicationDirectory();
                FullPath = ApplicationDirectory + "\\OwnerInfo.inf";

                if (File.Exists(FullPath))
                {
                    TextReader tr = new StreamReader(FullPath);
                    using (tr)
                    {
                        this.ClubName = tr.ReadLine();
                        this.Text = this.Text + " : " + this.ClubName;
                        this.ClubAbbreviation = tr.ReadLine();
                        this.ClubID =  Convert.ToInt64(tr.ReadLine());
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void ReadFile()
        {
            try
            {
                string[] fileType = new string[] { "PigeonRegistration", "Entry", "RaceResult" };
                BIZ.ReadWriteFile readWriteFile = new BIZ.ReadWriteFile();

                for (int i = 0; i < fileType.Length - 1; i++)
                {
                    readWriteFile.ReadConnecntionStringFile(fileType[i]);
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
