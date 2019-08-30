using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigeonIDSystem
{
    public partial class frmAssignMobileNumber : Form
    {
        public String MemberID { get; set; }
        public frmAssignMobileNumber()
        {
            InitializeComponent();
        }

        private void frmAssignMobileNumber_Load(object sender, EventArgs e)
        {
            try
            {
                GetPigeonList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void GetPigeonList()
        {
            try
            {
                DataTable pigeonList = new DataTable();

                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = " ";

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "  ";

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "SeqNo";

                DataColumn dc4 = new DataColumn();
                dc4.ColumnName = "BandNumber";


                DataColumn dc5 = new DataColumn();
                dc5.ColumnName = "TagID";

                DataColumn dc6 = new DataColumn();
                dc6.ColumnName = "Name";

                DataColumn dc7 = new DataColumn();
                dc7.ColumnName = "MobileNumner";

                //DataColumn dc8 = new DataColumn();
                //dc8.ColumnName = "Color";

                pigeonList.Columns.Add(dc1);
                pigeonList.Columns.Add(dc2);
                pigeonList.Columns.Add(dc3);
                pigeonList.Columns.Add(dc4);
                pigeonList.Columns.Add(dc5);
                pigeonList.Columns.Add(dc6);
                pigeonList.Columns.Add(dc7);
                //pigeonList.Columns.Add(dc8);

                string path = ReadText.ReadFilePath("datapath");
                string filepath = path + "\\pigeonlist\\" + MemberID + ".txt";
                if (File.Exists(filepath))
                {
                    string[] pigeonCollection = ReadText.ReadTextFile(filepath);
                    int seqNumber = 1;
                    foreach (string item in pigeonCollection)
                    {
                        string[] value = item.Split('|');
                        DataRow dr = pigeonList.NewRow();
                        dr[" "] = "ADD";
                        dr["  "] = "REMOVE";
                        dr["SeqNo"] = seqNumber.ToString();
                        dr["BandNumber"] = value[1].ToString();
                        dr["TagID"] = value[2].ToString();

                        //string path = ReadText.ReadFilePath("datapath");
                        string pigeonMobileListPath = path + "\\PigeonMobileList\\" + value[2].ToString() + ".txt";
                        if (File.Exists(pigeonMobileListPath))
                        {
                            string[] pigeonMobileCollection = ReadText.ReadTextFile(pigeonMobileListPath);
                            string[] values = pigeonMobileCollection[0].ToString().Split('|');
                            dr["Name"] = values[0].ToString().Trim();
                            dr["MobileNumner"] = values[1].ToString().Trim();
                        }

                        pigeonList.Rows.Add(dr);
                        seqNumber++;
                    }

                }

                dataGridView1.DataSource = pigeonList;
                lblcount.Text = "Total Birds: " + pigeonList.Rows.Count.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.ColumnIndex;
            int rowindex = e.RowIndex;
            this.dataGridView1.Update();
            DataTable dt = (DataTable)this.dataGridView1.DataSource;
            string value = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            string bandValue = this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() + " | " + this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (value == "REMOVE")
            {
                if (this.listBox1.Items.IndexOf(bandValue) > -1)
                {
                    this.listBox1.Items.Remove(bandValue);
                }
            }
            else if (value == "ADD")
            {
                if (this.listBox1.Items.IndexOf(bandValue) == -1)
                {
                    this.listBox1.Items.Add(bandValue);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count > 0)
            {
                string path = ReadText.ReadFilePath("datapath");
                //string filepath = path + "\\pigeonlist\\" + MemberID + ".txt";
                foreach (string item in this.listBox1.Items)
                {
                    string[] col = item.Split('|');
                    string pigeonMobileListPath = path + "\\PigeonMobileList\\" + col[0].ToString().Trim() + ".txt";
                    if (this.txtName.Text != "" && this.txtMobileNumber.Text != "")
                    {
                        string[] mvalue = { this.txtName.Text + "|" + this.txtMobileNumber.Text };
                        System.IO.File.WriteAllLines(pigeonMobileListPath, mvalue); //memberpigeonlist
                    }

                }
                MessageBox.Show("Record(s) Save.");
                this.listBox1.Items.Clear();
                GetPigeonList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to delete this record(s)?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string path = ReadText.ReadFilePath("datapath");
                //string filepath = path + "\\pigeonlist\\" + MemberID + ".txt";
                foreach (string item in this.listBox1.Items)
                {
                    string[] col = item.Split('|');
                    string pigeonMobileListPath = path + "\\PigeonMobileList\\" + col[0].ToString().Trim() + ".txt";

                    if (File.Exists(pigeonMobileListPath))
                    {
                        File.Delete(pigeonMobileListPath);
                    }

                }
                MessageBox.Show("Record(s) Deleted");
                this.listBox1.Items.Clear();
                GetPigeonList();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
        }
    }
}
