using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using excel = Microsoft.Office.Interop.Excel;

namespace PegionClocking
{
    public partial class frmStickerGeneration : Form
    {
        public frmStickerGeneration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            fd.Filter = "All Files|*.*|XLS|*.xls|XLSx|*.xlsx";
            fd.FilterIndex = 2;
            this.txtTemplate.Text = fd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.checkBox2.Checked)
                {
                    GenerateQRCodeSticker();
                }
                else
                    GenerateSticker();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void GenerateSticker()
        {
            try
            {
                string format = "EXCEL";

                if (this.radioButton2.Checked)
                {
                    format = "PDF";
                }

                DAL.StickerNumber stickerNumber = new DAL.StickerNumber();
                if (this.txtFileCount.Text != "" && this.txtDestination.Text != "" && this.txtFilename.Text != "" && this.txtTemplate.Text != "")
                {
                    Int64 recordCount = Convert.ToInt64(this.txtFileCount.Text);
                    Int64 index = 1;
                    string path = "";
                    while (index <= recordCount)
                    {
                        path = this.txtDestination.Text + "\\" + this.txtFilename.Text + "_" + index + ".xls";
                        System.IO.File.Copy(this.txtTemplate.Text, path, true);
                        GenerateNow(stickerNumber.StickerSelectAll(), path, index, recordCount,format, this.txtDestination.Text, this.txtFilename.Text + "_" + index);
                        index += 1;
                    }
                    MessageBox.Show("Sticker Generated sucessfully", "Sticker Generation");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateQRCodeSticker()
        {
            try
            {
                string format = "EXCEL";

                if (this.radioButton2.Checked)
                {
                    format = "PDF";
                }

                DAL.StickerNumber stickerNumber = new DAL.StickerNumber();

                if (this.txtFileCount.Text != "" && this.txtDestination.Text != "" && this.txtFilename.Text != "" && this.txtTemplate.Text != "")
                {
                    Int64 recordCount = Convert.ToInt64(this.txtFileCount.Text);
                    Int64 index = 1;
                    string path = "";
                    while (index <= recordCount)
                    {
                        path = this.txtDestination.Text + "\\" + this.txtFilename.Text + "_" + index + ".xlsx";
                        System.IO.File.Copy(this.txtTemplate.Text, path, true);
                        GenerateNow(stickerNumber.QRCodeStickerSelectAll(), path, index, recordCount, format, this.txtDestination.Text, this.txtFilename.Text + "_" + index);
                        index += 1;
                    }
                    MessageBox.Show("Sticker Generated sucessfully", "Sticker Generation");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateNow(DataSet dt, string Template, Int64 index, Int64 recordcount, string format,string path, string filename)
        {
            try
            {
                int rowCounter = 0;
                
                excel.Application excelApp = new excel.Application();
                excel.Workbook wb;
                excel.Worksheet ws;

                wb = excelApp.Workbooks.Open(Template);
                ws = wb.Sheets[2];

                DataTable data = new DataTable();

                data = dt.Tables[0];

                //Left Table
                rowCounter = 2;
                foreach (DataRow dtrow in data.Rows)
                {
                    ws.Cells[rowCounter, 2] = dtrow["Outter"].ToString();
                    Thread.Sleep(300);
                    ws.Cells[rowCounter, 3] = dtrow["Inner"].ToString();
                    Thread.Sleep(300);
                    rowCounter += 1;
                }

                wb.Save();
                //MessageBox.Show("Report Generated sucessfully", "Report Generation");
                if (index == recordcount)
                {
                    excelApp.Visible = true;
                }

                if (format == "PDF")
                {
                    if (!Directory.Exists(path + "\\PDF"))
                    {
                        Directory.CreateDirectory(path + "\\PDF\\");
                    }
                    ws = wb.Sheets[1];
                    ws.ExportAsFixedFormat(excel.XlFixedFormatType.xlTypePDF, path + "\\PDF\\" + filename + ".pdf");

                    if (checkBox1.Checked)
                    {
                        ws = wb.Sheets[3];
                        ws.ExportAsFixedFormat(excel.XlFixedFormatType.xlTypePDF, path + "\\PDF\\" + filename + "_1.pdf");
                    }
                }

                
                wb.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GenerateCard()
        {
            try
            {
                DAL.StickerNumber stickerNumber = new DAL.StickerNumber();
                if (this.txtFileCount.Text != "" && this.txtDestination.Text != "" && this.txtFilename.Text != "" && this.txtTemplate.Text != "")
                {
                    Int64 recordCount = Convert.ToInt64(this.txtFileCount.Text);
                    Int64 index = 1;
                    string path = "";
                    while (index <= recordCount)
                    {
                        path = this.txtDestination.Text + "\\" + this.txtFilename.Text + "_" + index + ".xls";
                        System.IO.File.Copy(this.txtTemplate.Text, path, true);
                        GenerateCardNow(stickerNumber.CardSelectAll(), path, index, recordCount);
                        index += 1;
                    }
                    MessageBox.Show("Sticker Generated sucessfully", "Sticker Generation");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       private void GenerateCardNow(DataSet dt, string Template, Int64 index, Int64 recordcount)
        {
            try
            {
                int rowCounter = 0;

                excel.Application excelApp = new excel.Application();
                excel.Workbook wb;
                excel.Worksheet ws;

                wb = excelApp.Workbooks.Open(Template);
                ws = wb.Sheets[1];

                DataTable data = new DataTable();

                data = dt.Tables[0];

                //Left Table
                rowCounter = 2;
                foreach (DataRow dtrow in data.Rows)
                {
                    ws.Cells[rowCounter, 1] = dtrow["PinNumber"].ToString();
                    ws.Cells[rowCounter, 2] = dtrow["CardNumber"].ToString();
                    rowCounter += 1;
                }

                wb.Save();
                //MessageBox.Show("Report Generated sucessfully", "Report Generation");
                if (index == recordcount)
                {
                    excelApp.Visible = true;
                }
                wb.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmStickerGeneration_Load(object sender, EventArgs e)
        {
            Common.Global.IsMainDatabase = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                GenerateCard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
    }
}
