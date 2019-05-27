using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PigeonProgram
{
    public partial class Offspring : Form
    {
        public Int64 PigeonID { get; set; }
        public String PigeonName { get; set; }
        public Int64 UserID { get; set; }

        public Offspring()
        {
            InitializeComponent();
            this.dataGridView1.DoubleClick += new EventHandler(grid_DoubleClick);
        }

        private void Offspring_Load(object sender, EventArgs e)
        {
            try
            {
                GetPigeonOffspringList();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            } 
        }

        private void GetPigeonOffspringList()
        {
            try
            {
                BIZ.PigeonDetails pigeonDetails = new BIZ.PigeonDetails();
                pigeonDetails.PigeonID = PigeonID;

                DataSet ds = new DataSet();
                ds = pigeonDetails.GetPigeonOffspring();

                if (ds.Tables.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(Font, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataGridView datagrid = this.dataGridView1;
                DataSet dtResult = new DataSet();
                Int64 index;
                if (datagrid.RowCount > 0)
                {
                    BIZ.PigeonDetails pigenDetails = new BIZ.PigeonDetails();
                    index = datagrid.CurrentRow.Index;
                    if ((string)datagrid.CurrentCell.Value.ToString() == "VIEW DETAILS")
                    {
                        PigeonID = Convert.ToInt64(datagrid.Rows[Convert.ToInt32(index)].Cells[0].Value);
                        if (PigeonID > 0)
                        {
                            PigeonProgram.PigeonDetails frmPigeonDetails = new PigeonDetails();
                            frmPigeonDetails.PigeonID = PigeonID;
                            frmPigeonDetails.UserID = UserID;
                            frmPigeonDetails.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
