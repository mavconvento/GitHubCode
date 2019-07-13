using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmReadRFID : Form
    {
        public String RFIDTags { get; set; }
        public frmReadRFID()
        {
            InitializeComponent();
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
        }

        private void ReadRFID_Load(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private String ReadID()
        {
            try
            {
                Eclock eclock = new Eclock();
                string serialPort = eclock.GetPort();
                string[] ports = SerialPort.GetPortNames();
                string commPort = "";
                foreach (var item in ports)
                {
                    if (serialPort.Contains(item)) commPort = item;
                }

                if (!String.IsNullOrEmpty(commPort))
                {

                    //String entryCollection = "12345678|14220397|12345678|12345678|12345678|12345678|12345678|12345678|15212437";
                    //String[] entryList = entryCollection.Split('|');

                    bool transmit = false;
                    while (!transmit)
                    {
                        //eclock.SendData("$Race$" + item + "#", commPort);
                        String inComingData = eclock.ReceiveData(commPort);
                        if (inComingData != "")
                        {
                            return PrintData(inComingData);
                        }
                    }

                }
                return "";
            }
            catch (Exception)
            {

                throw;
            }  
        }

        private static String PrintData(string data)
        {
            Console.WriteLine();
            String[] value = data.Split('|');
            if (value[1] != "noresult")
            {
                return value[1];
            }

            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
                this.Close();
                //backgroundWorker1.ReportProgress(100);
            }
        }

        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                    var value = ReadID();
                    RFIDTags = value;
                    if (value != "") e.Cancel = true;
            }

        }

        // This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                this.Close();
            }
            else if (e.Error != null)
            {
                this.Close();
            }
        }
    }
}
