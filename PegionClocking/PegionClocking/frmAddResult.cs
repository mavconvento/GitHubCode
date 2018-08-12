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
    public partial class frmAddResult : Form
    {
        #region Properties
        public Int64 ClubID { get; set; }
        public DateTime DateRelease { get; set; }
        #endregion

        #region Events
        public frmAddResult()
        {
            InitializeComponent();
        }

        private void frmAddResult_Load(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Value = DateRelease;
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
                AddRaceResultFromBackup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.dtArrivalDate.Value = this.dateTimePicker1.Value;
            }
            catch (Exception ex)
            {

                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }

        }
        #endregion

        private void AddRaceResultFromBackup()
        {
            try
            {

                BIZ.RaceResult raceresult = new BIZ.RaceResult();
                raceresult.ClubID = ClubID;
                raceresult.StickerCode = txtStickerCode.Text;
                raceresult.ReleasedDate = DateRelease;
                raceresult.Sender = txtSender.Text;
                raceresult.Arrival = dtArrivalDate.Value.Date.ToShortDateString() + " " + txtArrivalTime.Text;
                raceresult.RaceResultAddFromBackup();
                MessageBox.Show("Race result save.");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
