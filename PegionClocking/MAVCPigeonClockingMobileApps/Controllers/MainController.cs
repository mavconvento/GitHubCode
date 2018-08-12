using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAVCPigeonClockingMobileApps;
using MAVCPigeonClockingMobileApps.Models;
using System.Data;

namespace MAVCPigeonClockingMobileApps.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/


        [HttpGet]
        public ActionResult MainPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Race()
        {
            ViewData["clubname"] = tools.GetClubList(LWT.Common.LWTSafeTypes.SafeString(Session["UserID"]), "");
            return View();
        }

        [HttpGet]
        public ActionResult Result()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ClubResult()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MyDetails()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CheckBalance()
        {
            return View();
        }

        public ActionResult Forecast()
        {
            ViewBag.Result = GetCommonResultItems("ForeCast");
            return GetForecastDetails();
        }


        [HttpGet]
        public ActionResult BirdClock()
        {

            return View();
        }

        [HttpGet]
        public ActionResult LoadMavcCard()
        {

            return View();
        }

        //public JsonResult GetCommonResult()
        //{
        //    return this.Json(GetCustomerFacilityList(), JsonRequestBehavior.AllowGet);
        //}

        private ActionResult GetForecastDetails()
        {
            return View();
        }

        public JsonResult RaceDetails(String ClubID, DateTime ReleaseDate)
        {
            try
            {
                SetSessionValue(ClubID, ReleaseDate);
                MAVCPigeonClockingMobileApps.Models.MainPage mainPage = new MAVCPigeonClockingMobileApps.Models.MainPage();
                mainPage.ClubID = ClubID;
                mainPage.ReleaseDate = LWT.Common.LWTSafeTypes.SafeDateTime(ReleaseDate);
                return Json(mainPage.GetRaceDetails());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult SendStickerNumber(String StickerNumber, String ClubName)
        {
            try
            {
                MAVCPigeonClockingMobileApps.Models.MainPage mainPage = new MAVCPigeonClockingMobileApps.Models.MainPage();
                mainPage.StickerNumber = StickerNumber;
                mainPage.ClubName = ClubName;
                mainPage.MobileNumber = LWT.Common.LWTSafeTypes.SafeString(Session["UserID"]);
                return Json(mainPage.SendStickerNumber());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult SendForecast(String ClubName)
        {
            try
            {
                MAVCPigeonClockingMobileApps.Models.MainPage mainPage = new MAVCPigeonClockingMobileApps.Models.MainPage();
                mainPage.StickerNumber = "Forecast";
                mainPage.ClubName = LWT.Common.LWTSafeTypes.SafeString(Session["ClubName"]);
                mainPage.MobileNumber = LWT.Common.LWTSafeTypes.SafeString(Session["UserID"]);
                return Json(mainPage.SendStickerNumber());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String GetCommonResultItems(String Keyword)
        {
            MAVCPigeonClockingMobileApps.Models.MainPage mainPage = new MAVCPigeonClockingMobileApps.Models.MainPage();
            mainPage.StickerNumber = Keyword;
            mainPage.ClubName = LWT.Common.LWTSafeTypes.SafeString(Session["ClubName"]);
            mainPage.ReleaseDate = LWT.Common.LWTSafeTypes.SafeDateTime(Session["ReleaseDate"]);
            mainPage.MobileNumber = LWT.Common.LWTSafeTypes.SafeString(Session["UserID"]);
            return mainPage.SendStickerNumber().ResultMessage;
        }

        public JsonResult GetTestResult()
        {
            MAVCPigeonClockingMobileApps.Models.MainPage mainPage = new MAVCPigeonClockingMobileApps.Models.MainPage();
            return this.Json(mainPage.GetForecastTable(), JsonRequestBehavior.AllowGet);
        }
        private void SetSessionValue(String clubName, DateTime releaseDate)
        {
            Session["ClubName"] = clubName;
            Session["ReleaseDate"] = releaseDate;
        }
    }
}
