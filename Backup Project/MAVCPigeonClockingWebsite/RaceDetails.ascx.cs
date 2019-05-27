using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MAVCPigeonClockingWebsite;
using System.Data;
using System.IO;
using Telerik.Web.UI;

namespace MAVCPigeonClockingWebsite
{

    public partial class RaceDetails : System.Web.UI.UserControl
    {

        ReadTextFile readTextFile;

        public string Club { get; set; }
        public string FullName { get; set; }
        public string Version { get; set; }
        public string DateRelease { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public string Filter { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.Logo.Src = "Images/ClubLogo/" + Club + ".png";
                    this.lblClubName.Text = FullName;
                    PopulateComboBox();
                }
                GetDetails();
            }
            catch (Exception ex)
            {
                this.RadWindowManager1.RadAlert(ex.Message, 350, 150, "Error", "");
            }
        }

        public void Parameters(string club, string fullname,string dateRelease,string category,string group,string filter)
        {
            Club = club;
            FullName = fullname;
            DateRelease = dateRelease;
            Category = category;
            Group = group;
            Filter = filter;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RaceResult.aspx?CLUB=" + Club + "&CLUBFULLNAME=" + FullName + "&DATERELEASE=" + rcbDate.SelectedValue.ToString() + "&CATEGORY=" + rcbCategory.SelectedValue.ToString() + "&GROUP=" + rcbGroup.SelectedValue.ToString(), false);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OverAllRaceResult.aspx?CLUB=" + Club + "&CLUBFULLNAME=" + FullName + "&DATERELEASE=" + rcbDate.SelectedValue.ToString() + "&CATEGORY=" + rcbCategory.SelectedValue.ToString() + "&GROUP=" + rcbGroup.SelectedValue.ToString(), false);
        }

        protected void rbtnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/RaceResult.aspx?CLUB=" + Club + "&CLUBFULLNAME=" + FullName + "&DATERELEASE=" + rcbDate.SelectedValue.ToString() + "&CATEGORY=" + rcbCategory.SelectedValue.ToString() + "&GROUP=" + rcbGroup.SelectedValue.ToString() + "&FILTER=" + this.txtName.Text.ToString(), false);
            }
            catch
            { }
        }
        private void PopulateComboBox()
        {
            try
            {
                DataTable dtResult = new DataTable();

                this.rcbCategory.Items.Clear();
                this.rcbGroup.Items.Clear();
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = "All";
                item.Value = "All";
                this.rcbCategory.Items.Add(item);

                item = new RadComboBoxItem();
                item.Text = "All";
                item.Value = "All";
                this.rcbGroup.Items.Add(item);

                item = new RadComboBoxItem();
                item.Text = "None";
                item.Value = "None";
                this.rcbGroup.Items.Add(item);

                item = new RadComboBoxItem();
                item.Text = "None";
                item.Value = "None";
                this.rcbCategory.Items.Add(item);

                item = new RadComboBoxItem();
                item.Text = "Young Bird(s)";
                item.Value = "Young Bird(s)";
                this.rcbCategory.Items.Add(item);

                item = new RadComboBoxItem();
                item.Text = "Old Bird(s)";
                item.Value = "Old Bird(s)";
                this.rcbCategory.Items.Add(item);

                item = new RadComboBoxItem();
                item.Text = "Super Set";
                item.Value = "Super Set";
                this.rcbGroup.Items.Add(item);

                item = new RadComboBoxItem();
                item.Text = "2K Special";
                item.Value = "2K Special";
                this.rcbGroup.Items.Add(item);

                if (Category != "") this.rcbCategory.SelectedValue = Category;
                if (Group != "") this.rcbGroup.SelectedValue = Group;

                dtResult = GetReleaseDateCollection();

                if (dtResult.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        item = new RadComboBoxItem();
                        item.Text = dr["DateRelease"].ToString();
                        item.Value = dr["DateRelease"].ToString();
                        this.rcbDate.Items.Add(item);
                    }
                }

                if (DateRelease != null)
                {
                    this.rcbDate.SelectedValue = DateRelease;
                }

                if (Filter != "") this.txtName.Text = Filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetReleaseDateCollection()
        {
            
            try
            {
                DataTable dtResult = new DataTable();
                DataRow dr=dtResult.NewRow();

                dtResult.Columns.Add("DateRelease");

                dr["DateRelease"]="";
                dtResult.Rows.Add(dr);

                string root = Server.MapPath("~");
                readTextFile = new ReadTextFile();
                string Template = root + @"TextFile\RaceResult\" + Club + @"\RaceDateRelease.txt";

                dtResult = readTextFile.ReadFromTextFile(Template, dtResult, "", "", "");
                 return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }
        private void GetDetails()
        {
            try
            {
                ReadTextFile readTextFile = new ReadTextFile();
                DataTable dtResult = new DataTable();
                DataTable dtDetails = new DataTable();

                dtResult.Columns.Add("Location");
                dtResult.Columns.Add("Coordinates");
                dtResult.Columns.Add("Lap");
                dtResult.Columns.Add("DateReleased");
                dtResult.Columns.Add("TimeReleased");
                dtResult.Columns.Add("BirdEntry");
                dtResult.Columns.Add("TotalSMS");
                dtResult.Columns.Add("Version");
                dtResult.Columns.Add("StopTime");
                dtResult.Columns.Add("MinSpeed");

                string root = Server.MapPath("~");
                string Template = root + @"TextFile\RaceResult\" + Club + @"\RaceDetails" + DateRelease.Replace("-","") + ".txt";

                if (File.Exists(Template))
                {
                    dtDetails = readTextFile.ReadFromTextFile(Template, dtResult, "", "", "");

                    if (dtDetails.Rows.Count > 0)
                    {
                        this.lblLocation.Text = dtDetails.Rows[0]["Location"].ToString();
                        this.lblCoordinates.Text = dtDetails.Rows[0]["Coordinates"].ToString();
                        this.lblLap.Text = dtDetails.Rows[0]["Lap"].ToString();
                        this.lblTimeReleased.Text = dtDetails.Rows[0]["DateReleased"].ToString() + " " + dtDetails.Rows[0]["TimeReleased"].ToString();
                        this.lblTotalBirdEntry.Text = dtDetails.Rows[0]["BirdEntry"].ToString();
                        this.lblTotalSMS.Text = dtDetails.Rows[0]["TotalSMS"].ToString();
                        this.lblStopTimeDetails.Text = dtDetails.Rows[0]["StopTime"].ToString();
                        this.lblMinimumSpeed.Text = dtDetails.Rows[0]["MinSpeed"].ToString();
                        Session["VERSION"] = dtDetails.Rows[0]["Version"].ToString();
                        if (Session["VERSION"].ToString() == "1")
                        {
                            this.rcbCategory.Enabled = false;
                            this.rcbGroup.Enabled = false;
                        }
                    }
                }
                else
                {
                    this.lblLocation.Text = "";
                    this.lblCoordinates.Text ="";
                    this.lblLap.Text = "";
                    this.lblTimeReleased.Text = "";
                    this.lblTotalBirdEntry.Text = "";
                    this.lblTotalSMS.Text = "";
                    this.lblStopTimeDetails.Text = "";
                }
            }
            catch (Exception ex)
            {
                this.RadWindowManager1.RadAlert(ex.Message, 350, 150, "Error", "");
            }
        }

    }
}