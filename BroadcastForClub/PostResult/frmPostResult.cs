using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using excel = Microsoft.Office.Interop.Excel;
using WebCamService;
using System.IO;

namespace PostResult
{
    public partial class frmPostResult : Form
    {
        //private const string ADDRESS = "96.43.137.44";
        //private const string USERNAME = "cmarkadams";
        //private const string PASSWORD = "t3chn1c@l";
        //private const string SOURCE = @":\MAVCCLOCKINGRACERESULTS\RaceResults\";
        //private const string TARGET = @"www.pigeonclocking.somee.com/TextFile/RaceResult";

        //private const string ADDRESS = "208.94.246.100";
        //private const string USERNAME = "mavcclocking";
        //private const string PASSWORD = "t3chn1c@l";
        //private const string SOURCE = @":\MAVCCLOCKINGRACERESULTS\RaceResults\";
        //private const string TARGET = @"www.mavcpigeonclocking.somee.com/TextFile/RaceResult";

        private string ADDRESS = "";
        private string USERNAME = "";
        private string PASSWORD = "";
        private string SOURCE = "";
        private string TARGET = "";

        BIZ.ResultPosting resultPosting;
        //BIZ.GenerateReport generateReport;
        //excel.Application excelApp = new excel.Application();
        //DateTime dateRelease;
        String dateReleaseResult;
        String clubAbbreviation = "PHILF";
        //String clubName;
        Int64 recordCountCurrent = 0;
        Double recordCountPrevious = -1;

        public frmPostResult()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetRaceResult(this.comboBox1.Text);
        }
        private void frmPostResult_Load(object sender, EventArgs e)
        {
            GetClubList();
            //this.Visible = false;
            //this.timer1.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetRaceResult(this.comboBox1.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            SetWebsiteHost();
            this.timer1.Enabled = true;
        }
        private void SetWebsiteHost()
        {
            if (rbPrimary.Checked)
            {
                ADDRESS = "198.58.81.85";
                USERNAME = "pigeon1";
                PASSWORD = "06242009mavc";
                SOURCE = @":\MAVCCLOCKINGRACERESULTS\RaceResults\";
                TARGET = @"mavcpigeonclocking.com/wwwroot/TextFile/RaceResult";
            }

            else
            {
                ADDRESS = "208.94.246.100";
                USERNAME = "mavcclocking";
                PASSWORD = "t3chn1c@l";
                SOURCE = @":\MAVCCLOCKINGRACERESULTS\RaceResults\";
                TARGET = @"www.mavcpigeonclocking.somee.com/TextFile/RaceResult";
            }
        }
        private void GetClubList()
        {
            resultPosting = new BIZ.ResultPosting();
            DataSet dtResult = new DataSet();

            dtResult = resultPosting.GetClubList();

            if (dtResult.Tables.Count > 0)
            {
                if (dtResult.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dtResult.Tables[0].Rows)
                    {
                        this.comboBox1.Items.Add(dr["ClubAbbreviation"]);
                    }
                }
            }
        }
        private void GetRaceResult(string club)
        {
            try
            {
                DataTable dtDateRelease;
                DataTable dtHeader;
                DataTable dtData;
                DataSet dtTable = new DataSet();
                int version = 0;
                dateReleaseResult = "";
                //generateReport = new BIZ.GenerateReport();

                resultPosting = new BIZ.ResultPosting();
                dtTable = resultPosting.RaceResult(club);
                if (dtTable.Tables.Count > 0)
                {
                    if (dtTable.Tables[0].Rows.Count > 0)
                    {
                        version = (int)dtTable.Tables[0].Rows[0]["Version"];
                        clubAbbreviation = (string)dtTable.Tables[0].Rows[0]["Club"];
                    }
                    dtDateRelease = dtTable.Tables[1];
                    dtHeader = dtTable.Tables[2];
                    dtData = dtTable.Tables[3];
                    GenerateReports(dtDateRelease, dtHeader, dtData, version);
                }
            }
            catch { }
        }

        #region ReportGeneration
        private void GenerateReports(DataTable dtDateRelease, DataTable dtHeader, DataTable dtData, int version)
        {
            try
            {
                recordCountCurrent = dtData.Rows.Count;
                if (recordCountCurrent != 0 || dtHeader.Rows.Count != 0 || recordCountPrevious == -1)
                {
                    if (recordCountCurrent != recordCountPrevious) ReportGenerationDefault(dtDateRelease, dtHeader, dtData);
                    recordCountPrevious = recordCountCurrent;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ReportGenerationDefault(DataTable dtDateRelease, DataTable dtHeader, DataTable dt)
        {
            try
            {
                //GetRaceDateRelease
                ReadRaceDateRelease(dtDateRelease);

                //GetRaceDetails
                ReadRaceDetails(dtHeader);

                //GetRaceResult
                ReadRaceResults(dt);

                //PostInQueing
                PostResultToQue(AppDomain.CurrentDomain.BaseDirectory + @"RaceResult\");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void ReportGenerationDefault_Excel(string fileName, string templateName, DataTable dtHeader, DataTable dt, int startCol, int version)
        //{
        //    try
        //    {
        //        //excel.Application excelApp = new excel.Application();
        //        excel.Workbook wb;
        //        excel.Worksheet ws;

        //        GetTemplate(fileName, templateName);
        //        wb = excelApp.Workbooks.Open(fileName + ".xls");
        //        ws = wb.Sheets[1];

        //        ////GetRaceDetails
        //        if (version == 3)
        //        {
        //            ws.Cells[5, 2] = dtHeader.Rows[0]["LocationName"].ToString();
        //            ws.Cells[6, 2] = dtHeader.Rows[0]["Coordinates"].ToString();
        //            ws.Cells[7, 2] = dtHeader.Rows[0]["Lap"].ToString();
        //            ws.Cells[5, 7] = dtHeader.Rows[0]["ReleaseTime"].ToString();
        //            ws.Cells[6, 7] = dtHeader.Rows[0]["TotalBird"].ToString();
        //            ws.Cells[7, 7] = dtHeader.Rows[0]["SMSCount"].ToString();
        //            dateRelease = Convert.ToDateTime(dtHeader.Rows[0]["DateRelease"].ToString());
        //            clubAbbreviation = dtHeader.Rows[0]["ClubAbbreviation"].ToString();
        //            clubName = dtHeader.Rows[0]["ClubName"].ToString();
        //        }
        //        //Copy to Excel
        //        int rowCounter = 10;
        //        for (int colheader = 0; colheader < (dt.Columns.Count - startCol); colheader++)
        //        {
        //            ws.Cells[rowCounter - 1, colheader + 1] = dt.Columns[colheader + startCol].ColumnName.ToString();
        //            ws.Columns[colheader + 1].AutoFit();
        //        }

        //        foreach (DataRow dtrow in dt.Rows)
        //        {
        //            for (int col = 0; col < (dt.Columns.Count - startCol); col++)
        //            {
        //                ws.Cells[rowCounter, col + 1] = dtrow[startCol + col].ToString();
        //                ws.Columns[col + 1].AutoFit();
        //            }
        //            rowCounter += 1;
        //        }
        //        wb.Save();
        //        wb.ExportAsFixedFormat(excel.XlFixedFormatType.xlTypePDF, fileName + ".pdf", Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard, true, true);
        //        //MessageBox.Show("Report Generated sucessfully", "Report Generation");
        //        //excelApp.Visible = true;

        //        wb.Close();
        //        PostResultToDropbox(fileName);

        //        //excelApp.Quit();
        //        //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void GetTemplate(string fileName, string templateName)
        {
            try
            {
                string templatePath = "";
                templatePath = AppDomain.CurrentDomain.BaseDirectory + @"Template\" + templateName + ".xls";
                System.IO.File.Copy(templatePath, fileName + ".xls", true);
                System.IO.File.Delete(fileName + ".pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetTemplateTextFile(string source, string destination)
        {
            try
            {
                System.IO.File.Copy(source, destination, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PostResultToDropbox(string fileName)
        {
            try
            {
                if (clubAbbreviation == "") clubAbbreviation = "PHILF";
                System.IO.File.Copy(fileName + ".pdf", @"C:\Dropbox\RaceResult\" + clubAbbreviation + @"\RaceResult.pdf", true);
                //System.IO.File.Copy(fileName + ".pdf", @"C:\Dropbox\" + clubAbbreviation + @"\RaceResult\Previous\RaceResult_" + string.Format("{0:yyyy-MM-dd}",dateRelease) + ".pdf", true);
                //System.IO.File.Delete(fileName + ".pdf");
                UploadFTP();
                System.Threading.Thread.Sleep(30000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PostResultToQue(string fileName)
        {
            try
            {
                if (clubAbbreviation == "") clubAbbreviation = "PHILF";
                System.IO.File.Copy(fileName + clubAbbreviation + "\\RaceDateRelease.txt", this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceDateReleaseLatest.txt", true);
                System.IO.File.Copy(fileName + clubAbbreviation + "\\RaceDetails.txt", this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceDetailsLatest.txt", true);
                System.IO.File.Copy(fileName + clubAbbreviation + "\\RaceResult.txt", this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceResultLatest.txt", true);
                System.IO.File.Copy(fileName + clubAbbreviation + "\\RaceResultStatus.txt", this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceResultStatus.txt", true);
                UpdateRaceResultStatus(this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceResultStatus.txt", dateReleaseResult);
                UploadFTP();
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UploadFTP()
        {
            FtpClient ftp = new FtpClient(ADDRESS, USERNAME, PASSWORD, TARGET + @"/" + clubAbbreviation);
            ftp.Login();
            ftp.Upload(this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceDateReleaseLatest.txt");
            ftp.Upload(this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceDetailsLatest.txt");
            ftp.Upload(this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceResultLatest.txt");
            System.Threading.Thread.Sleep(5000);
            ftp.Upload(this.textBox1.Text + SOURCE + clubAbbreviation + @"\RaceResultStatus.txt");
            ftp.Close();
        }
        private void UpdateRaceResultStatus(string url, string dateRelease)
        {
            try
            {
                //string Source = AppDomain.CurrentDomain.BaseDirectory + @"TextFile\Template.txt";
                //string Destination = AppDomain.CurrentDomain.BaseDirectory + @"RaceResult\" + clubAbbreviation + @"\RaceDetails.txt";

                //GetTemplateTextFile(Source, Destination);

                if (File.Exists(url))
                {
                    StreamWriter tr = new StreamWriter(url);
                    using (tr)
                    {
                        tr.WriteLine
                            (
                            "0" + "::" +
                            dateRelease.Replace("/", "-").ToString()
                            );
                    }
                }
            }
            catch { }
        }
        private void ReadRaceDateRelease(DataTable dt)
        {
            try
            {
                string Source = AppDomain.CurrentDomain.BaseDirectory + @"TextFile\Template.txt";
                string Destination = AppDomain.CurrentDomain.BaseDirectory + @"RaceResult\" + clubAbbreviation + @"\RaceDateRelease.txt";
                string ReadRaceDateRelease = "";

                GetTemplateTextFile(Source, Destination);

                if (File.Exists(Destination))
                {

                    StreamWriter tr = new StreamWriter(Destination);
                    using (tr)
                    {
                        foreach (DataRow dtrow in dt.Rows)
                        {
                            ReadRaceDateRelease = "";
                            ReadRaceDateRelease = dtrow["DateRelease"].ToString();
                            tr.WriteLine(ReadRaceDateRelease);
                        }
                    }
                }
            }
            catch { }
        }
        private void ReadRaceDetails(DataTable dt)
        {
            try
            {
                string Source = AppDomain.CurrentDomain.BaseDirectory + @"TextFile\Template.txt";
                string Destination = AppDomain.CurrentDomain.BaseDirectory + @"RaceResult\" + clubAbbreviation + @"\RaceDetails.txt";

                GetTemplateTextFile(Source, Destination);

                if (File.Exists(Destination))
                {

                    StreamWriter tr = new StreamWriter(Destination);
                    string[] date = dt.Rows[0]["DateRelease"].ToString().Split(' ');
                    dateReleaseResult = date[0].ToString();
                    using (tr)
                    {
                        tr.WriteLine
                            (
                            dt.Rows[0]["LocationName"].ToString() + "::" +
                            dt.Rows[0]["Coordinates"].ToString() + "::" +
                            dt.Rows[0]["Lap"].ToString() + "::" +
                            dateReleaseResult + "::" +
                            dt.Rows[0]["ReleaseTime"].ToString() + "::" +
                            dt.Rows[0]["TotalBird"].ToString() + "::" +
                            dt.Rows[0]["SMSCount"].ToString() + "::" +
                            dt.Rows[0]["Version"].ToString() + "::" +
                            dt.Rows[0]["StopTime"].ToString() + "::" +
                            dt.Rows[0]["MinSpeed"].ToString()
                            );
                    }
                }
            }
            catch { }
        }
        private void ReadRaceResults(DataTable dt)
        {
            try
            {
                string Source = AppDomain.CurrentDomain.BaseDirectory + @"TextFile\Template.txt";
                string Destination = AppDomain.CurrentDomain.BaseDirectory + @"RaceResult\" + clubAbbreviation + @"\RaceResult.txt";
                string ReadRaceResult = "";
                GetTemplateTextFile(Source, Destination);

                if (File.Exists(Destination))
                {

                    StreamWriter tr = new StreamWriter(Destination);
                    using (tr)
                    {
                        foreach (DataRow dtrow in dt.Rows)
                        {
                            ReadRaceResult = "";
                            for (int col = 0; col < (dt.Columns.Count); col++)
                            {
                                if (ReadRaceResult != "")
                                {
                                    ReadRaceResult = ReadRaceResult + "::" + dtrow[col].ToString();
                                }
                                else
                                {
                                    ReadRaceResult = dtrow[col].ToString();
                                }
                            }
                            tr.WriteLine(ReadRaceResult);

                        }
                    }
                }
            }
            catch { }
        }
        #endregion
    }
}
