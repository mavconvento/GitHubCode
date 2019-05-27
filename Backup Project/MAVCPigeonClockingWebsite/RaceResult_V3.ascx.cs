using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace MAVCPigeonClockingWebsite
{
    public partial class RaceResult_V3 : System.Web.UI.UserControl
    {
        public string Club { get; set; }
        public string Filter { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public string DateRelease { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Club == null)
                {
                    Club = (string)Request.QueryString["CLUB"];
                }

                if (DateRelease == "" || DateRelease == null) DateRelease = Request.QueryString["DATERELEASE"];
                if (Filter == "" || Filter == null) Filter = Request.QueryString["FILTER"];
                if (Category == "" || Category == null) Category = Request.QueryString["CATEGORY"];
                if (Group == "" || Group == null) Group = Request.QueryString["GROUP"];
                if (Session["VERSION"].ToString() == "3") this.Visible = true;
            }
            catch (Exception ex)
            {
                this.RadWindowManager1.RadAlert(ex.Message, 350, 150, "Error", "");
            }

        }
        protected void rgResults_ItemCommand(object sender, GridCommandEventArgs e)
        {

            try
            {
                if (e.Item is GridDataItem)
                {

                }
            }
            catch (Exception ex)
            {
                this.RadWindowManager1.RadAlert(ex.Message, 350, 150, "Error", "");
            }
        }
        protected void rgResults_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {

                ReadTextFile readTextFile = new ReadTextFile();
                DataTable dtResult = new DataTable();

                dtResult.Columns.Add("GroupCategory");
                dtResult.Columns.Add("Rank");
                dtResult.Columns.Add("MemberName");
                dtResult.Columns.Add("Distance");
                dtResult.Columns.Add("BandNo");
                dtResult.Columns.Add("StickerCode");
                dtResult.Columns.Add("ArrivalTime");
                dtResult.Columns.Add("Flight");
                dtResult.Columns.Add("Speed");

                if (DateRelease == null || DateRelease == "")
                {
                    rgResults.DataSource = dtResult;
                }
                else
                {
                    string root = Server.MapPath("~");
                    string Template = root + @"TextFile\RaceResult\" + Club + @"\raceresult" + DateRelease.Replace("-","") + ".txt";
                    if (File.Exists(Template))
                    {
                        rgResults.DataSource = readTextFile.ReadFromTextFile(Template, dtResult, Filter, Category, Group);
                    }
                    else
                    {
                        rgResults.DataSource = dtResult;
                    }
                }

            }
            catch (Exception ex)
            {
                this.RadWindowManager1.RadAlert(ex.Message, 350, 150, "Error", "");
            }
        }
        protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {

                }
            }
            catch (Exception ex)
            {
                this.RadWindowManager1.RadAlert(ex.Message, 350, 150, "Error", "");
            }
        }

        public void parameters(string club, string filter, string category, string group,string dateRelease)
        {
            Club = club;
            Filter = filter;
            Category = category;
            Group = group;
            DateRelease = dateRelease;
        }
        
    }
}