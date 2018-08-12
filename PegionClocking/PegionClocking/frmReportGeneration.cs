using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using excel = Microsoft.Office.Interop.Excel;

namespace PegionClocking
{
    public partial class frmReportGeneration : Form
    {
        public DataTable dtRecord { get; set; }
        public DataSet dtMemberDistance { get; set; }
        public String FileName { get; set; }
        public String TemplateName { get; set; }
        public String Type { get; set; }
        public int startCol { get; set; }
        public double percent { get; set; }
        public DataTable dtBillingStatement { get; set; }

        public frmReportGeneration()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }
        private void frmReportGeneration_Load(object sender, EventArgs e)
        {
            Start();
        }
        public bool GenerateReport()
        {
            try
            {
                bool status = false;
                //string fileName = "";

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files (*.xls)|*.xls";
                saveDialog.ShowDialog();
                FileName = saveDialog.FileName;

                if (FileName != "")
                {
                    switch (Type)
                    {
                        case "Masterlist":
                            TemplateName = "Masterlist";
                            startCol = 1;
                            break;
                        case "ResultSummary":
                            TemplateName = "Template";
                            startCol = 4;     
                            break;
                        case "RaceResult":
                            TemplateName = "RaceResult";
                            startCol = 0;
                            break;
                        case "ScheduleDetails":
                            TemplateName = "ScheduleDetails";
                            startCol = 2;
                            break;
                        case "Entry":
                            TemplateName = "Template";
                            startCol = 0;
                            break;
                        case "TransactionDetails":
                            TemplateName = "Template";
                            startCol = 0;
                            break;
                        case "TransactionHistory":
                            TemplateName = "Template";
                            startCol = 0;
                            break;
                        case "PaymentHistory":
                            TemplateName = "Template";
                            startCol = 0;
                            break;
                        case "MemberDistance":
                            TemplateName = "MemberDistance";
                            startCol = 0;
                            break; 
                        case "BillingStatement":
                            TemplateName = "BillingStatement";
                            startCol = 0;
                            break;
                    }
                    status = true;
                }
                else
                {
                    MessageBox.Show("Invalid Filename", "Error");
                }
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetTemplate()
        {
            try
            {
                string templatePath = "";
                templatePath = AppDomain.CurrentDomain.BaseDirectory + @"Template\" + TemplateName + ".xls";
                System.IO.File.Copy(templatePath, FileName, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region BackgroundWorker
        private void Start()
        {
            try
            {
                if (bw.IsBusy != true)
                {
                    if (dtRecord.Rows.Count > 0)
                    {
                        this.progressBar1.Maximum = 100;
                        if (GenerateReport()) bw.RunWorkerAsync();
                    }
                    
                    else
                    {
                        MessageBox.Show("No Record Found", "Error");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void Stop()
        {
            if (bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
            }
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;
            int rowCounter = 0;
            Double Counter = 0;
            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
            }
            else
            {
                excel.Application excelApp = new excel.Application();
                excel.Workbook wb;
                excel.Worksheet ws;

                GetTemplate();
                wb = excelApp.Workbooks.Open(FileName);
                ws = wb.Sheets[1];

                if (Type == "BillingStatement")
                {

                    ws.Cells[30,5] = dtBillingStatement.Rows[0]["TotalAmount"].ToString();
                    ws.Cells[24, 9] = dtBillingStatement.Rows[0]["TotalAmount"].ToString();
                    ws.Cells[31, 5] = dtBillingStatement.Rows[0]["TotalPayment"].ToString();
                    ws.Cells[32, 5] = dtBillingStatement.Rows[0]["Balance"].ToString();
                    ws.Cells[6, 4] = dtBillingStatement.Rows[0]["BillingNo"].ToString();
                    ws.Cells[7, 4] = dtBillingStatement.Rows[0]["ClubName"].ToString();
                    ws.Cells[6, 9] = dtBillingStatement.Rows[0]["DateNow"].ToString();
                    ws.Cells[30, 9] = dtBillingStatement.Rows[0]["PreviousBalance"].ToString();
                    ws.Cells[31, 9] = dtBillingStatement.Rows[0]["TotalBalance"].ToString();

                    Counter = 0;
                    rowCounter =12;
                    foreach (DataRow dtrow in dtRecord.Rows)
                    {
                        ws.Cells[rowCounter, 1] = dtrow[startCol + 1].ToString();
                        ws.Columns[0 + 1].AutoFit();

                        ws.Cells[rowCounter, 5] = dtrow[startCol + 2].ToString();
                        ws.Columns[0 + 5].AutoFit();

                        ws.Cells[rowCounter, 7] = dtrow[startCol + 3].ToString();
                        ws.Columns[0 + 7].AutoFit();

                        ws.Cells[rowCounter, 9] = dtrow[startCol + 4].ToString();
                        ws.Columns[0 + 9].AutoFit();

                        Counter += 1;
                        rowCounter += 1;
                        percent = (Counter / dtRecord.Rows.Count) * 100;
                        //System.Threading.Thread.Sleep(500);
                        worker.ReportProgress(Convert.ToInt32(percent));
                    }
                }

                else if (Type != "MemberDistance")
                {
                    //Copy to Excel
                    rowCounter = 2;
                    for (int colheader = 0; colheader < (dtRecord.Columns.Count - startCol); colheader++)
                    {
                        ws.Cells[1, colheader + 1] = dtRecord.Columns[colheader + startCol].ColumnName.ToString();
                        ws.Columns[colheader + 1].AutoFit();
                    }

                    Counter = 0;
                    foreach (DataRow dtrow in dtRecord.Rows)
                    {
                        for (int col = 0; col < (dtRecord.Columns.Count - startCol); col++)
                        {
                            ws.Cells[rowCounter, col + 1] = dtrow[startCol + col].ToString();
                            ws.Columns[col + 1].AutoFit();
                        }
                        Counter += 1;
                        rowCounter += 1;
                        percent = (Counter / dtRecord.Rows.Count) * 100;
                        //System.Threading.Thread.Sleep(500);
                        worker.ReportProgress(Convert.ToInt32(percent));
                    }
                }
                else
                {
                    DataTable dtmemberdetails = new DataTable();
                    DataTable dtleftTable = new DataTable();
                    DataTable dtrightTable = new DataTable();

                    dtmemberdetails = dtMemberDistance.Tables[0];
                    dtleftTable = dtMemberDistance.Tables[1];
                    dtrightTable = dtMemberDistance.Tables[2];

                    //MemberDetails
                    ws.Cells[3, 2] =dtmemberdetails.Rows[0]["Name"];
                    ws.Cells[4, 2] = dtmemberdetails.Rows[0]["Coordinates"];
                    ws.Cells[3, 6] = dtmemberdetails.Rows[0]["LoftName"];
                    ws.Cells[4, 6] = DateTime.Now;

                    //Left Table
                    Counter = 0;
                    rowCounter = 8;
                    startCol = 0;
                    foreach (DataRow dtrow in dtleftTable.Rows)
                    {
                        for (int col = 0; col < (dtleftTable.Columns.Count - startCol); col++)
                        {
                            ws.Cells[rowCounter, col + 1] = dtrow[startCol + col].ToString();
                            ws.Columns[col + 1].AutoFit();
                        }
                        Counter += 1;
                        rowCounter += 1;
                        percent = (Counter / (dtleftTable.Rows.Count + dtrightTable.Rows.Count)) * 100;
                        //System.Threading.Thread.Sleep(500);
                        worker.ReportProgress(Convert.ToInt32(percent));
                    }

                    //Right Table
                    rowCounter = 8;
                    startCol = 0;
                    foreach (DataRow dtrow in dtrightTable.Rows)
                    {
                        for (int col = 0; col < (dtrightTable.Columns.Count - startCol); col++)
                        {
                            ws.Cells[rowCounter, col + 5] = dtrow[startCol + col].ToString();
                            ws.Columns[col + 5].AutoFit();
                        }
                        Counter += 1;
                        rowCounter += 1;
                        percent = (Counter / (dtleftTable.Rows.Count + dtrightTable.Rows.Count)) * 100;
                        //System.Threading.Thread.Sleep(500);
                        worker.ReportProgress(Convert.ToInt32(percent));
                    }
                }
                wb.Save();
                MessageBox.Show("Report Generated sucessfully", "Report Generation");
                excelApp.Visible=true;
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                bw.CancelAsync();
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                
                this.Close();
            }

            else if (!(e.Error == null))
            {
                MessageBox.Show(e.Error.Message, "Error");
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }
        #endregion
    }
}
