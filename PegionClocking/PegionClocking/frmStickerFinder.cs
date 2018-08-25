using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PegionClocking
{
    public partial class frmStickerFinder : Form
    {
        public String StickerNumber { get; set; }
        public frmStickerFinder()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BIZ.RaceResult raceresult = new BIZ.RaceResult();
                DataSet dtresult = new DataSet();
                raceresult.StickerCode = txtStickerCode.Text;
                dtresult = raceresult.GetSticker();
                if (dtresult.Tables.Count > 0)
                {
                    if (dtresult.Tables[0].Rows.Count > 0)
                    {
                        StickerNumber = dtresult.Tables[0].Rows[0]["FullStickerNo"].ToString();
                        this.Close();
                    }
                }

                if (string.IsNullOrEmpty(StickerNumber))
                {
                    MessageBox.Show("Invalid Sticker Code. No record found.", "No Record");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
