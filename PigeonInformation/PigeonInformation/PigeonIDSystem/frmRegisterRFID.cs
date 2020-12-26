using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmRegisterRFID : Form
    {
        public frmRegisterRFID()
        {
            InitializeComponent();
        }

        private void txtrfid_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.txtrfid.Focus();
            frmReadRFID readRFID = new frmReadRFID();
            readRFID.ShowDialog();
            this.txtrfid.Text = readRFID.RFIDTags;
            if (this.txtrfid.Text.ToString().Length == 8)
            {
                Save();
            } 
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Save(); 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Save()
        {
            try
            {
                if (txtrfid.Text != "" && txtrfid.Text != "0")
                {
                    DataSet ds = new DataSet();
                    BusinessLayer.Common common = new BusinessLayer.Common();
                    ds = common.RfidSave(txtrfid.Text);

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "Success")
                            {
                                MessageBox.Show("RFID Tags Save.", "Error");
                            }
                            else
                            {
                                MessageBox.Show(ds.Tables[0].Rows[0][0].ToString(), "Invalid");
                            }
                            this.txtrfid.Text = "";
                            this.btnRead.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
