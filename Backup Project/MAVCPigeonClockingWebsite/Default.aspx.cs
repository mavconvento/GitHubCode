using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MAVCPigeonClockingWebsite
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    GetClubList();
            //}
            //Response.Redirect("http://www.mavcpigeonclocking.com",false);
        }

        protected void btnViewResult_OnClick(object sender, EventArgs e)
        {
            //Session["CLUB"] = cmbClubName.SelectedValue.ToString();
            //Session["CLUBFULLNAME"] = cmbClubName.SelectedItem.ToString();
            Session["Version"] = "";
            Response.Redirect("~/RaceResult.aspx?CLUB=" + cmbClubName.SelectedValue.ToString() + "&CLUBFULLNAME=" + cmbClubName.SelectedItem.ToString(), false);
        }

        protected void btnViewPrevious_OnClick(object sender, EventArgs e)
        {

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Session["Version"] = "";
            Response.Redirect("~/RaceResult.aspx?CLUB=" + cmbClubName.SelectedValue.ToString() + "&CLUBFULLNAME=" + cmbClubName.SelectedItem.ToString(), false);
        }

        protected void cmbClubName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClubName.SelectedValue != "")
            {
                //this.btnViewPrevious.Enabled = true;
                //this.btnViewRaceResult.Enabled = true;
                this.btnGo.Enabled = true;
                //SetHfValue();
            }
            else
            {
                //this.btnViewPrevious.Enabled = false;
                //this.btnViewRaceResult.Enabled = false;
                this.btnGo.Enabled = false;
            }
        }

        private void GetClubList()
        {
            try
            {
                string connectionString = "";
                connectionString = Server.MapPath("~/TextFile/ClubList.txt");
                string content = "";
                string[] contentArray;
                string[] items;

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        content = tr.ReadToEnd().Replace("\r\n", "");
                        contentArray = content.Split(';');
                        for (int a = 0; a < contentArray.Length; a++)
                        {
                            items = contentArray.GetValue(a).ToString().Split('-');
                            ListItem i = new ListItem();
                            if (items.Length != 1)
                            {
                                i.Text = items.GetValue(0).ToString();
                                i.Value = items.GetValue(1).ToString();
                                cmbClubName.Items.Add(i);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

    }
}