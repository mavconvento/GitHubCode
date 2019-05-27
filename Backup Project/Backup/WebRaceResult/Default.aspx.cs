using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using LWT.Common;
using LWT.Common.UI;
using WebRaceResult.Common;
using WebRaceResult.BIZ;

namespace WebRaceResult
{
    public partial class Default : BaseUI
    {
        String ClubID;
        String Sender;
        String BirdCategory;
        String RaceCategory;
        String SearchName;
        DateTime ReleaseDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialisation();
            FillEmptyControl();
        }

        public override void Initialisation()
        {
            try
            {
                lblClubName.Text = LWTSafeTypes.SafeString(Request.QueryString["ClubName"]);
                ClubID = LWTSafeTypes.SafeString(Request.QueryString["ClubID"]);
                ReleaseDate = DateTime.Now;
                Sender = LWTSafeTypes.SafeString(Request.QueryString["UserID"]);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override void FillEmptyControl()
        {
            try
            {

                BIZ.RaceResult raceResult = new RaceResult();
                Common.Common.FillDropdownList(rcbBirdCategory, raceResult.GetBirdCategory(ClubID).Tables[0], "Description", "Description", true, "All");
                Common.Common.FillDropdownList(rcbGroupCategory, raceResult.GetGroupCategory(ClubID).Tables[0], "RaceGroup", "RaceGroup", true, "All");
            }
            catch (Exception ex)
            {
                
                Common.Common.RaiseMessage(RadWindowManager1, ex.Message, "RaceResult", Page.ToString(), MethodBase.GetCurrentMethod().Name, "", "Mavcpigeonclocking");
            }
        }

        protected void radBtnViewResult_Click(object sender, EventArgs e)
        {
            try
            {
                //string SystemVersion = ConfigurationManager.AppSettings["SystemVersion"];
                //Session["SystemVersion"] = SystemVersion;
                //Session.Add("InstallationID", 0);
                //Session.Add("UserID", 0);
                //LWT.WebService.Login login = new WebService.Login();
                //if (login.AuthenticateLogin("", rtbLoginID.Text.Trim(), rtbPassword.Text.Trim(), SYSTEM_CODE))
                //{
                //    Session["SystemVersion"] = SystemVersion;

                //    DataTable dtGS = GlobalSetting.GetByTagKey(LWTSafeTypes.SafeInt64(Session["InstallationID"]), LWTSafeTypes.SafeInt64(Session["UserID"]), "HOMEPAGE_TMS");

                //    string hp = dtGS.Rows[0]["cValue"].ToString(); //"~/ProcessRateChange/RateChangeMain.aspx"; //"~/Accounts/AccountFacilitySearch.aspx?pagestate=loadinitial"; //"~/AccountFacility/AccountFacility.aspx?action=edit&KeyID=8";

                //    Response.Redirect(hp, false);
                //    //HttpContext.Current.Response.Redirect("~/Management/BBSYRates.aspx", false);
                //    //HttpContext.Current.Response.Redirect("~/Management/TermSheet.aspx?trustid=1&mastertrustID=1&installationFundID=1", false);
                //    //Response.Redirect("/Management/ClassNotes.aspx?TermSheetID=1",false);
                //    //Response.Redirect("~/Funding/FundingSubscription.aspx", false);

                //}
                //else
                //{
                //    throw new Exception(LOGIN_FAILED);
                //}
            }
            catch (Exception ex)
            {
                //CommonMethod.RaiseMessage(RadWindowManager1, ex.Message, "Trust Management", Page.ToString(), "radBtnLogin_Click", "", "Trust Management");
                //txtOrgCd.Text = "";
                // rtbLoginID.Text = "";
                // rtbPassword.Text = "";
                // CustomValidator1.IsValid = false;
                //CustomValidator1.ErrorMessage = ex.Message;
            }
        }

        protected void rgRaceResult_ItemCommand(object sender, GridCommandEventArgs e)
        {

            try
            {
                //RadWindowManager1.Windows[0].VisibleOnPageLoad = false;
                //if (e.Item is GridDataItem)
                //{
                //    GridDataItem item = (GridDataItem)e.Item;
                //    CouponID = LWTSafeTypes.SafeInt64(rgCouponPeriod.MasterTableView.DataKeyValues[LWTSafeTypes.SafeInt(e.CommandArgument)]["CouponID"]);
                //    StartDate = LWTSafeTypes.SafeNULLDateTime(rgCouponPeriod.MasterTableView.DataKeyValues[LWTSafeTypes.SafeInt(e.CommandArgument)]["StartDate"]);
                //    EndDate = LWTSafeTypes.SafeNULLDateTime(rgCouponPeriod.MasterTableView.DataKeyValues[LWTSafeTypes.SafeInt(e.CommandArgument)]["EndDate"]);
                //    switch (e.CommandName)
                //    {
                //        case "CloseItem":
                //            CloseItem();
                //            break;
                //        case "RerunItem":
                //            RerunCouponPeriod();
                //            break;
                //        case "Report":
                //            FormattedDateStartDate = (StartDate != null ? StartDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : "n/a"); //StartDate.ToString("yyyy-MM-dd"));
                //            FormattedDateEndDate = (EndDate != null ? EndDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : "n/a");       //EndDate.ToString("yyyy-MM-dd");


                //            if (ValidateCouponPeriodReport())
                //            {
                //                //{4}{107.46}  
                //                RadWindowManager1.Windows[0].NavigateUrl = "~/Reports/ReportMain.aspx?InstallationID=" + InstallationID + "&UserID=" + UserID +
                //                    "&ReportName=WarehouseRolloverReport" + "&TrustID=" + TrustID +
                //                    "&QueryStringIncFilter=true" + "&DateFrom=" + FormattedDateStartDate + "&DateTo=" + FormattedDateEndDate;
                //                RadWindowManager1.Windows[0].VisibleOnPageLoad = true;
                //            }
                //            else
                //            {
                //                Response.Redirect("~/Reports/ReportMain.aspx?" + "&displaylastactionmsg=" + REPORT_EMPTYRESULTS);
                //            }
                //            break;

                //    }
                //}
            }
            catch (Exception ex)
            {
                Common.Common.RaiseMessage(RadWindowManager1, ex.Message, "RaceResult", Page.ToString(), MethodBase.GetCurrentMethod().Name, "", "Mavcpigeonclocking");
            }

        }
        protected void rgRaceResult_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                //GridDataItem dataItem = e.Item as GridDataItem;
                //ImageButton button = dataItem["CloseItem"].Controls[0] as ImageButton;
                //ImageButton button1 = dataItem["RerunItem"].Controls[0] as ImageButton;
                //ImageButton button2 = dataItem["Report"].Controls[0] as ImageButton;
                //DateTime? dateclosed;
                //DateTime? datererun;

                //button.Attributes["onclick"] = "confirmClose('" + ConfirmClose + "'," + LWTSafeTypes.SafeString(e.Item.ItemIndex) + "); return false;";
                //button1.Attributes["onclick"] = "confirmRerun('" + ConfirmRerun + "'," + LWTSafeTypes.SafeString(e.Item.ItemIndex) + "); return false;";
                //button2.Attributes["onclick"] = "confirmReport(" + LWTSafeTypes.SafeString(e.Item.ItemIndex) + "); return false;";

                //dateclosed = LWTSafeTypes.SafeNULLDateTime(dataItem.GetDataKeyValue("DateClosed"));
                //datererun = LWTSafeTypes.SafeNULLDateTime(dataItem.GetDataKeyValue("RerunDate"));
                //if (dateclosed == null)
                //{
                //    button.ImageUrl = "~/Images/close.jpg";
                //    button.Enabled = true;
                //    button1.ImageUrl = "~/Images/rerun.jpg";
                //    button1.Enabled = true;

                //}
                //else
                //{
                //    dataItem["CloseItem"].Controls[0].Visible = false;
                //    dataItem["CloseItem"].Enabled = false;
                //    dataItem["RerunItem"].Controls[0].Visible = false;
                //    dataItem["RerunItem"].Enabled = false;
                //}

            }

        }
        protected void rgRaceResult_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                BIZ.RaceResult raceresult = new RaceResult();
                rgRaceResult.DataSource = raceresult.GetRaceResultDetails(ClubID, rcbBirdCategory.Text, rcbGroupCategory.Text, ReleaseDate, SearchName, Sender);

                //if (ClubID != "")
                //{
                    
                //}
                //else
                //{
                //    //RadToolBar1.Items[0].Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                Common.Common.RaiseMessage(RadWindowManager1, ex.Message, "RaceResult", Page.ToString(), MethodBase.GetCurrentMethod().Name, "", "Mavcpigeonclocking");
            }
        }

        protected void rgRaceResult_PageIndexChanged(Object sender, GridPageChangedEventArgs e)
        {
            try
            {
                //if (TrustID > 0)
                //{
                //    StoreDataToBusinessLayer();
                //    DataSet ds = payment.GetCouponPeriodDates(InstallationID, UserID);
                //    rgCouponPeriod.DataSource = ds.Tables[1];
                //    if (ds != null)
                //    {
                //        divSection.Style["display"] = "visible";
                //        lblCouponStartDate.InnerText = LWTSafeTypes.SafeString(StartDate);
                //        lblCouponEndDate.InnerHtml = LWTSafeTypes.SafeString(EndDate);

                //    }
                //    RadToolBar1.Items[0].Enabled = true;
                //}
                //else
                //{
                //    RadToolBar1.Items[0].Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                Common.Common.RaiseMessage(RadWindowManager1, ex.Message, "RaceResult", Page.ToString(), MethodBase.GetCurrentMethod().Name, "", "Mavcpigeonclocking");
            }
        }

        protected void rcbGroupCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ////rgResults.Rebind();
                //TrustID = LWTSafeTypes.SafeInt64(rcbTrust.SelectedValue);
                //DataSet dataSet = Biz.ProceedsPeriodDetails.GetBySearch(InstallationID, UserID, 0, TrustID, 0);
                //RadListView1.DataSource = dataSet.Tables[0];
                //RadListView1.Rebind();
            }
            catch (Exception ex)
            {
                Common.Common.RaiseMessage(RadWindowManager1, ex.Message, "RaceResult", Page.ToString(), MethodBase.GetCurrentMethod().Name, "", "Mavcpigeonclocking");
            }
        }

        protected void rcbBirdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ////rgResults.Rebind();
                //TrustID = LWTSafeTypes.SafeInt64(rcbTrust.SelectedValue);
                //DataSet dataSet = Biz.ProceedsPeriodDetails.GetBySearch(InstallationID, UserID, 0, TrustID, 0);
                //RadListView1.DataSource = dataSet.Tables[0];
                //RadListView1.Rebind();
            }
            catch (Exception ex)
            {
                Common.Common.RaiseMessage(RadWindowManager1, ex.Message, "RaceResult", Page.ToString(), MethodBase.GetCurrentMethod().Name, "", "Mavcpigeonclocking");
            }
        }
    }
}