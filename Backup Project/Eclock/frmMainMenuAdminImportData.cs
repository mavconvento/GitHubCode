using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Eclock.BIZ;

namespace Eclock
{
    public partial class frmMainMenuAdminImportData : Form
    {
        #region Variable
        string ActionType;
        string Action;
        int DataRowCount;
        #endregion

        #region Properties
        public frmMainMenuAdmin Parent { get; set; }
        #endregion

        #region Events
        public frmMainMenuAdminImportData()
        {
            InitializeComponent();
        }
        private void frmMainMenuAdminImportData_FormClosed(object sender, FormClosedEventArgs e)
        {
            BIZ.Common common = new BIZ.Common();
            common.CloseSubForm(this);
        }
        private void btnStartImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker1.IsBusy != true)
                {
                    ImportData();
                }
                else
                {
                    MessageBox.Show("Importing Data in progress...");
                }

            }
            catch (Exception ex)
            {
                 MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorker1.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorker1.CancelAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Private Methods
        private void ImportData()
        {

            try
            {
                pbMemberList.Maximum = 100;
                pbMemberList.Step = 1;
                pbMemberList.Value = 0;

                pbRegisterRFID.Maximum = 100;
                pbRegisterRFID.Step = 1;
                pbRegisterRFID.Value = 0;

                pbRegisterBandNumber.Maximum = 100;
                pbRegisterBandNumber.Step = 1;
                pbRegisterBandNumber.Value = 0;

                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region BackGroundWorker
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var backgroundWorker = sender as BackgroundWorker;

                BIZ.ImportData importData = new BIZ.ImportData();
                importData.ClubID = Parent.ClubID;
                importData.UserID = Parent.UserID;

                //Import Member Masterlist
                DataSet dsMemberList = new DataSet();
                dsMemberList = importData.GetMemberList();

                if (dsMemberList.Tables.Count > 0)
                {
                    if (dsMemberList.Tables[0].Rows.Count > 0)
                    {
                        ActionType = "MemberList";
                        BackgroundWorker_DoworkProcess(backgroundWorker, dsMemberList);
                    }
                }

                //Import Register RFID
                DataSet dsRegisterRFID = new DataSet();
                dsRegisterRFID = importData.GetRegisterRFID();
                if (dsRegisterRFID.Tables.Count > 0)
                {
                    if (dsRegisterRFID.Tables[0].Rows.Count > 0)
                    {
                        ActionType = "RegisterRFID";
                        BackgroundWorker_DoworkProcess(backgroundWorker, dsRegisterRFID);
                    }
                }

                //Import Register Band Number with RFID
                DataSet dsRegisterBandNumber = new DataSet();
                dsRegisterBandNumber = importData.GetRegisterBandNumberWithRFID();
                if (dsRegisterBandNumber.Tables.Count > 0)
                {
                    if (dsRegisterBandNumber.Tables[0].Rows.Count > 0)
                    {
                        ActionType = "RegisterBandNumber";
                        BackgroundWorker_DoworkProcess(backgroundWorker, dsRegisterBandNumber);
                    }
                }

                MessageBox.Show("Importing Data Complete.");
            }
            catch (Exception ex)
            {
                 MessageBox.Show(BIZ.Common.CustomError(ex.Message), "Error");
            }
        }

        private void BackgroundWorker_DoworkProcess(BackgroundWorker myWorker, DataSet dsResult)
        {
            try
            {
                string ApplicationDirectory = Common.GetApplicationDirectory();
                string fullpath = "";

                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        DataRowCount = dsResult.Tables[0].Rows.Count;
                        int Index = 1;
                        if (ActionType == "MemberList")
                        {
                            fullpath = ApplicationDirectory + "\\DataCollection\\Admin\\MemberList.inf";
                            File.WriteAllText(fullpath, String.Empty);
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                            {
                                foreach (DataRow item in dsResult.Tables[0].Rows)
                                {
                                    file.WriteLine(item["MemberID"] + "|" +
                                        item["MemberIDNo"] + "|" +
                                        item["MemberName"] + "|" +
                                        item["LoftName"] + "|" +
                                        item["DistanceLatDegree"] + "|" +
                                        item["DistanceLatMinutes"] + "|" +
                                        item["DistanceLatSeconds"] + "|" +
                                        item["DistanceLatSign"] + "|" +
                                        item["DistanceLongDegree"] + "|" +
                                        item["DistanceLongMinutes"] + "|" +
                                        item["DistanceLongSeconds"] + "|" +
                                        item["DistanceLongSign"] + "," +
                                        item["Coordinates"] + "|" +
                                        item["DeactivateMember"]
                                        );

                                    if (myWorker.CancellationPending)
                                    {
                                        MessageBox.Show("Importing Data was cancelled by the user.");
                                        return;
                                    }
                                    myWorker.ReportProgress((Index * 100) / DataRowCount);
                                    Index = Index + 1;
                                }
                            };
                        }

                        else if (ActionType == "RegisterRFID")
                        {
                            fullpath = ApplicationDirectory + "\\DataCollection\\Admin\\RegisterRFID.inf";
                            File.WriteAllText(fullpath, String.Empty);
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                            {
                                foreach (DataRow item in dsResult.Tables[0].Rows)
                                {
                                    file.WriteLine(item["ClubID"] + "|" +
                                        Common.Encrypt(item["RFIDSerialNo"].ToString())
                                        );

                                    if (myWorker.CancellationPending)
                                    {
                                        MessageBox.Show("Importing Data was cancelled by the user.");
                                        return;
                                    }
                                    myWorker.ReportProgress((Index * 100) / DataRowCount);
                                    Index = Index + 1;
                                }
                            };
                        }
                        else if (ActionType == "RegisterBandNumber")
                        {
                            fullpath = ApplicationDirectory + "\\DataCollection\\Admin\\RegisterBandNumber.inf";
                            File.WriteAllText(fullpath, String.Empty);
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullpath, true))
                            {
                                foreach (DataRow item in dsResult.Tables[0].Rows)
                                {
                                    file.WriteLine(item["ClubID"] + "|" +
                                        item["MemberID"] + "|" +
                                        Common.Encrypt(item["RFIDSerialNo"].ToString()) + "|" +
                                        item["BandID"] + "|" +
                                        item["BandNumber"]
                                        );

                                    if (myWorker.CancellationPending)
                                    {
                                        MessageBox.Show("Importing Data was cancelled by the user.");
                                        return;
                                    }
                                    myWorker.ReportProgress((Index * 100) / DataRowCount);
                                    Index = Index + 1;
                                }
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ActionType == "MemberList") pbMemberList.Value = e.ProgressPercentage;
            if (ActionType == "RegisterRFID") pbRegisterRFID.Value = e.ProgressPercentage;
            if (ActionType == "RegisterBandNumber") pbRegisterBandNumber.Value = e.ProgressPercentage;
        }

        #endregion

        private void frmMainMenuAdminImportData_Load(object sender, EventArgs e)
        {

        }

    }
}
