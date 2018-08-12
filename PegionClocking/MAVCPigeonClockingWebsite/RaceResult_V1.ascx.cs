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
    public partial class RaceResult_V1 : System.Web.UI.UserControl
    {

        public string Club { get; set; }
        public string Filter { get; set; }
        public string DateRelease { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Club == null)
                {
                    Club = (string)Request.QueryString["CLUB"];
                }
                if (Filter == "" || Filter == null) Filter = Request.QueryString["FILTER"];
                if (Session["VERSION"].ToString() == "1") this.Visible = true;
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


                dtResult.Columns.Add("Rank");
                dtResult.Columns.Add("Sender");
                dtResult.Columns.Add("MemberName");
                dtResult.Columns.Add("StickerCode");
                dtResult.Columns.Add("ArrivalTime");

                string root = Server.MapPath("~");
                string RaceResult = root + @"TextFile\RaceResult\" + Club + @"\RaceResult" + DateRelease.Replace("-","")  + ".txt";
               
                if (File.Exists(RaceResult))
                {
                    rgResults.DataSource = readTextFile.ReadFromTextFile(RaceResult, dtResult, Filter,"","");
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

        public void parameters(string club,string filter,string dateRelease)
        {
            Club = club;
            Filter = filter;
            DateRelease = dateRelease;

        }
    }
}
