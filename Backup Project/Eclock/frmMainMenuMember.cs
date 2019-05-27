using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Eclock
{
    public partial class frmMainMenuMember : Form
    {
        public String MobileNumber { get; set; }
        public String SMSActivated { get; set; }

        public frmMainMenuMember()
        {
            InitializeComponent();
        }

        private void btnTraining_Click(object sender, EventArgs e)
        {
            frmMainMenuMemberTrainingMode form = new frmMainMenuMemberTrainingMode();
            OpenForm(form);
        }

        private void btnRace_Click(object sender, EventArgs e)
        {
            frmMainMenuMemberRaceMode form = new frmMainMenuMemberRaceMode();
            OpenForm(form);
        }

        private void OpenForm(Form form)
        {
            BIZ.Common Common = new BIZ.Common();
            Common.OpenForm(form,this);
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            try
            {
                ReadFile();
            }
            catch (Exception ex)
            {

                 MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void ReadFile()
        {
            try
            {
                string[] fileType = new string[] {"PigeonRegistration","Entry","RaceResult"};
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

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            frmSetup form = new frmSetup();
            OpenForm(form);
        }

        private void frmMainMenuMember_Load(object sender, EventArgs e)
        {

        }

    }
}
