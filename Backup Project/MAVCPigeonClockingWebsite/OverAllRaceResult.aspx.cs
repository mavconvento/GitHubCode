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
    public partial class OverAllRaceResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

                //ReadTextFile readTextFile = new ReadTextFile();
                DataTable dtResult = new DataTable();


                dtResult.Columns.Add("Rank");
                dtResult.Columns.Add("MemberID");
                dtResult.Columns.Add("MemberName");
                dtResult.Columns.Add("BandNo");
                dtResult.Columns.Add("Points");
                dtResult.Columns.Add("Lap1");
                dtResult.Columns.Add("Lap2");
                dtResult.Columns.Add("Lap3");
                dtResult.Columns.Add("Lap4");
                dtResult.Columns.Add("Lap5");
                dtResult.Columns.Add("Lap6");
                dtResult.Columns.Add("Lap7");
                dtResult.Columns.Add("Lap8");
                dtResult.Columns.Add("Total");
                dtResult.Columns.Add("GroupCategory");

                //if (DateRelease == null || DateRelease == "")
                //{
                rgResults.DataSource = dtResult;
                //}
                //else
                //{
                //    string root = Server.MapPath("~");
                //    string Template = root + @"TextFile\RaceResult\" + Club + @"\raceresult" + DateRelease.Replace("-", "") + ".txt";
                //    if (File.Exists(Template))
                //    {
                //        rgResults.DataSource = readTextFile.ReadFromTextFile(Template, dtResult, Filter, Category, Group);
                //    }
                //    else
                //    {
                //        rgResults.DataSource = dtResult;
                //    }
                //}

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
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/RaceResult.aspx?CLUB=" + Club + "&CLUBFULLNAME=" + FullName + "&DATERELEASE=" + rcbDate.SelectedValue.ToString() + "&CATEGORY=" + rcbCategory.SelectedValue.ToString() + "&GROUP=" + rcbGroup.SelectedValue.ToString(), false);
        }

    }
}