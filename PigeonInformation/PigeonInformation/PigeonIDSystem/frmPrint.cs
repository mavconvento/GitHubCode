using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmPrint : Form
    {
        private Font verdana10Font;
        private StreamReader reader;
        private string FileContents;
        public DataTable DataForPrint { get; set; }
        public String ListType { get; set; }
        public String PlayerName { get; set; }

        public frmPrint()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            // Find all of the installed printers.
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cboPrinter.Items.Add(printer);
            }

            // Find and select the default printer.
            try
            {
                PrinterSettings settings = new PrinterSettings();

                cboPrinter.Text = settings.PrinterName;
            }
            catch
            {
            }

            verdana10Font = new Font("Verdana", 10);

            PrintLayout();
            // Initially select the source code file.
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "print.txt";
            txtFile.Text = file_path;
        }

        private void PrintLayout()
        {
            String filepathList = AppDomain.CurrentDomain.BaseDirectory + "print.txt";


            if (File.Exists(filepathList))
            {
                File.Delete(filepathList);
            }
            List<String> dataPrint = new List<string>();

            //Header
            string header = "Name: " + PlayerName;
            string title = "List of " + ListType;
            string count = "Total Record :" + DataForPrint.Rows.Count;

            dataPrint.Add(header);
            dataPrint.Add(count);
            dataPrint.Add("Date: " + DateTime.Today.ToLongDateString());
            dataPrint.Add(Environment.NewLine);
            dataPrint.Add("-------------------------------------------------------------------------------------");
            dataPrint.Add(title);
            dataPrint.Add("-------------------------------------------------------------------------------------");
            foreach (DataRow item in DataForPrint.Rows)
            {
                string line = "";
                foreach (DataColumn col in DataForPrint.Columns)
                {
                    if (item[col.ColumnName].ToString().ToUpper() != "DELETE" && item[col.ColumnName].ToString().ToUpper() != "EDIT" && col.ColumnName.ToString().ToUpper() != "TAGID")
                    {
                        line = line == "" ? item[col.ColumnName].ToString().ToUpper() : line + " | " + item[col.ColumnName].ToString().ToUpper();
                    }
                }
                dataPrint.Add("-------------------------------------------------------------------------------------");
                line = line + Environment.NewLine;
                dataPrint.Add(line);
            }


            foreach (string item in dataPrint)
            {
                if (File.Exists(filepathList))
                {
                    using (StreamWriter sw = File.AppendText(filepathList))
                    {
                        sw.WriteLine(item);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(filepathList))
                    {
                        sw.WriteLine(item);
                    }
                }
            }
        }

        private void AddSpaceInString(string value)
        {
            int count = value.Length;

            while (count <= 50)
            {
                value = value + " ";
            }
        }

        private void cboPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrinterSettings settings = new PrinterSettings();
            myPrinters.SetDefaultPrinter(this.cboPrinter.Text);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            ofdTextFile.FileName = txtFile.Text;
            if (ofdTextFile.ShowDialog() == DialogResult.OK)
                txtFile.Text = ofdTextFile.FileName;
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            // Read the file's contents.
            try
            {
                FileContents = File.ReadAllText(txtFile.Text).Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file " + txtFile.Text +
                    ".\n" + ex.Message);
                return;
            }

            string filename = txtFile.Text.ToString();
            //Create a StreamReader object  
            reader = new StreamReader(filename);

            // Display the print preview dialog.
            ppdTextFile.ShowDialog();
        }


        private void pdocTextFile_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            //Get the Graphics object  
            Graphics g = e.Graphics;
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            //Read margins from PrintPageEventArgs  
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font  
            linesPerPage = e.MarginBounds.Height / verdana10Font.GetHeight(g);
            //Now read lines one by one, using StreamReader  
            while (count < linesPerPage && ((line = reader.ReadLine()) != null))
            {
                //Calculate the starting position  
                yPos = topMargin + (count * verdana10Font.GetHeight(g));
                //Draw text  
                g.DrawString(line, verdana10Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                //Move to next line  

                count++;
            }
            //If PrintPageEventArgs has more pages to print  
            if (line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
    }

    public static class myPrinters
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);

    }
}
