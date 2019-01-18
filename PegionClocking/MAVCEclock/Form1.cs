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

namespace MAVCEclock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SerialPort comPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            try
            {
                comPort.Open();
                DateTime now = new DateTime();
                now = DateTime.Now.AddSeconds(2);
                string Time = "$TiMe$" + now.Year.ToString().Substring(2).PadLeft(2, '0') + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0');
                Time = Time + "w" + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0') + "x#";

                foreach (char item in Time)
                {
                    comPort.Write(item.ToString());
                }
                comPort.Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
