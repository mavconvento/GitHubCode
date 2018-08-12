using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using excel=Microsoft.Office.Interop.Excel;

namespace PegionClocking.BIZ
{
    class ReportGeneration
    {
        public void GenerateReport(Common.Common.ReportGeneration type,DataTable dt,ProgressBar progBar)
        {
            try
            {
                string fileName = "";

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files (*.xls)|*.xls";
                saveDialog.ShowDialog();
                fileName = saveDialog.FileName;

                if (fileName != "")
                {
                    switch (type)
                    {
                        case Common.Common.ReportGeneration.Masterlist:
                            ReportGenerationDefault(fileName, "Masterlist", dt, 1,progBar);
                            break;
                        case Common.Common.ReportGeneration.ResultSummary:
                            ReportGenerationDefault(fileName, "Template", dt, 4,progBar);
                            break;
                        case Common.Common.ReportGeneration.RaceResult:
                            ReportGenerationDefault(fileName, "RaceResult", dt, 0,progBar);
                            break;
                        case Common.Common.ReportGeneration.ScheduleDetails:
                            ReportGenerationDefault(fileName, "ScheduleDetails", dt, 0,progBar);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Filename", "Error");
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ReportGenerationDefault(string fileName, string templateName, DataTable dt, int startCol,ProgressBar progBar)
        {
            try
            {
                excel.Application excelApp = new excel.Application();
                excel.Workbook wb;
                excel.Worksheet ws;

                GetTemplate(fileName, templateName);
                wb = excelApp.Workbooks.Open(fileName);
                ws = wb.Sheets[1];
                
                //Copy to Excel
                int rowCounter = 2;
                for (int colheader = 0; colheader < (dt.Columns.Count - startCol); colheader++)
                {
                    ws.Cells[1, colheader + 1] = dt.Columns[colheader + startCol].ColumnName.ToString();
                    ws.Columns[colheader + 1].AutoFit();
                }

                Int64 Counter = 0;
                progBar.Maximum = dt.Rows.Count;
                foreach (DataRow dtrow in dt.Rows)
                {
                    for (int col = 0; col < (dt.Columns.Count - startCol); col++)
                    {
                        ws.Cells[rowCounter, col + 1] = dtrow[startCol + col].ToString();
                        ws.Columns[col + 1].AutoFit();
                    }
                    Counter += 1;
                    progBar.Value =Convert.ToInt32(Counter /Convert.ToInt64(dt.Rows.Count));
                    rowCounter += 1;
                }
                wb.Save();
                MessageBox.Show("Report Generated sucessfully", "Report Generation");
                //excelApp.Visible = true;

                //wb.ExportAsFixedFormat(excel.XlFixedFormatType.xlTypePDF, fileName, Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard, true, true);
                wb.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetTemplate(string fileName,string templateName)
        {
            try
            {
                string templatePath = "";
                templatePath = AppDomain.CurrentDomain.BaseDirectory + @"Template\" + templateName + ".xls";
                System.IO.File.Copy(templatePath, fileName, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
