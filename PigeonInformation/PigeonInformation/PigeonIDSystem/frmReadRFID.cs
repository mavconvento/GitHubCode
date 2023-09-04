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
        public String comPortNumber { get; set; }
        SerialPort comPort;

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
            string[] ports = SerialPort.GetPortNames();

            Eclock eclock = new Eclock();
            string serialPort = eclock.GetPort();

            foreach (var item in ports)
            {
                if (serialPort.Contains(item)) comPortNumber = item;
            }
            if (!String.IsNullOrEmpty(comPortNumber))
            {
                comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);

                //var value = ReadID();
                //RFIDTags = value;

                if (backgroundWorker1.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show("Club Reader not detected.", "Error");
                this.Close();
            }
        }

        private String ReadID()
        {
            try
            {

                Eclock eclock = new Eclock();
                //if (!comPort.IsOpen) comPort.Open();
                if (!String.IsNullOrEmpty(comPortNumber))
                {
                    //String entryCollection = "12345678|14220397|12345678|12345678|12345678|12345678|12345678|12345678|15212437";
                    //String[] entryList = entryCollection.Split('|');
                    bool transmit = false;
                    while (!transmit)
                    {
                        //eclock.SendData("$Race$" + item + "#", commPort);
                        String inComingData = eclock.ReceiveData(comPort);
                        if (inComingData != "")
                        {
                            return PrintData(inComingData);
                        }
                        else
                        {
                            //this.Close();
                            transmit = true;
                            backgroundWorker1.CancelAsync();
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
            String[] value = data.Split('|');
            int index = 0;

            foreach (var item in value)
            {
                if (item.Contains("datastart"))
                {
                    string rfid = value[index + 1];
                    if (rfid != "noresult" && rfid != "0")
                    {
                        return rfid;
                    }
                    break;
                }
                index++;
            }

            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
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
                e.Cancel = true;
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

        //private void frmReadRFID_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(comPortNumber))
        //    {
        //        //if (this.comPort.IsOpen) comPort.Close();
        //        comPort.Dispose();
        //    }

        //}
    }
}
