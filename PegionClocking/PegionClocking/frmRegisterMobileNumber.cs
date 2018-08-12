using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PegionClocking
{
    public partial class frmRegisterMobileNumber : Form
    {

        BIZ.Transaction transaction;

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Boolean IsAdmin { get; set; }
        #endregion

        #region Events
        public frmRegisterMobileNumber()
        {
            InitializeComponent();
        }

        private void frmRegisterMobileNumber_Load(object sender, EventArgs e)
        {
            Common.Global.IsMainDatabase = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = new DataSet();
                transaction = new BIZ.Transaction();
                transaction.ClubID = ClubID;
                transaction.UserID = UserID;

                if (txtMobileNumber.Text.Length != 11)
                {
                    MessageBox.Show("Invalid Mobile Number");
                }
                else if (txtPinNumber.Text.Length == 0)
                {
                    MessageBox.Show("Invalid MemberID No.");
                }
                else
                {
                    transaction.MobileNumber = txtMobileNumber.Text;
                    transaction.PinNumber = txtPinNumber.Text;
                    ds = transaction.RegisterMobileNumber();

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(ds.Tables[0].Rows[0]["ReplyMessage"].ToString(), "Message");

                            if (ds.Tables[0].Rows[0]["IsValid"].ToString() == "1")
                            {
                                txtMobileNumber.Text = "";
                                txtPinNumber.Text = "";
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Common.CustomError(ex.Message), "Error");
            }
        }
        #endregion
    }
}
