using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PegionClocking.DAL;

namespace PegionClocking
{
    public partial class Inbox_View : Form
    {
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }

        public Inbox_View()
        {
            InitializeComponent();
            dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void Inbox_View_Load(object sender, EventArgs e)
        {
            GetInbox();
        }
        private void GetInbox()
        {
            Inbox inbox = new Inbox();
            this.dataGridView1.DataSource = inbox.GetInbox(this.textBox1.Text,this.dateTimePicker1.Value,this.dateTimePicker2.Value,this.textBox2.Text,ClubID).Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetInbox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetInbox();
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    //member = new BIZ.Member();
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "ADD TO RESULT")
                    {
                        frmAddResult addresult = new frmAddResult();
                        addresult.StickerNumber = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[3].Value);
                        addresult.MobileNumber = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[4].Value);
                        addresult.DateRelease = Convert.ToDateTime(datagrid.Rows[Convert.ToInt32(index)].Cells[5].Value);
                        addresult.Time = Convert.ToString(datagrid.Rows[Convert.ToInt32(index)].Cells[6].Value);
                        addresult.ClubID = ClubID;
                        addresult.CallFrom = "INBOX";
                        addresult.ShowDialog();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportGeneration reportGeneration = new frmReportGeneration();
                DataTable dt = new DataTable();
                dt = (DataTable)this.dataGridView1.DataSource;
                reportGeneration.Type = "Masterlist";
                reportGeneration.dtRecord = dt;
                reportGeneration.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
    }
}

