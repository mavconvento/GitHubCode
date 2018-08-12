using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Common;
using System.Web.Security;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MavcPigeonClockingPortal.Models;
using MavcPigeonClockingPortal.Constants;
using MavcPigeonClockingPortal.Filter;


namespace MavcPigeonClockingPortal.Controllers
{
   
    public class RaceResultController : Controller
    {
        //
        // GET: /RaceResult/

        public JsonResult GetInitialValues()
        {
            String ClubID = @ViewBag.ClubID;
            return Json(new { ClubID = ClubID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClubList()
        {
            GetAutehnCookies();
            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetClubList(LWT.Common.LWTSafeTypes.SafeString(@ViewBag.cookiesUserName)), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult GetBirdCategory(String ClubID)
        {

            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetBirdCategory(ClubID), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult GetGroupCategory(String ClubID)
        {

            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetGroupCategory(ClubID), settings),
                ContentType = "application/json"
            };

            return jsonResult;

           
        }

        public ActionResult GetRaceDetails(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName)
        {
            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetRaceDetails(ClubID, BirdCategory, RaceCategory, ReleaseDate, SearchName,GetUserID()), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }


        public ActionResult GetRaceResult(String ClubID,String BirdCategory,String RaceCategory,DateTime ReleaseDate,String SearchName)
        {
            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetRaceResult(ClubID, BirdCategory, RaceCategory, ReleaseDate, SearchName), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult GetRaceEntry(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName)
        {
            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetRaceEntry(ClubID, BirdCategory, RaceCategory, ReleaseDate, SearchName, GetUserID()), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult SendSticker(string ClubID, string MobileNumber, string StickerNumber)
        {
            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.SendSticker(ClubID, MobileNumber, StickerNumber), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }


        public ActionResult Forecast(string ClubID, string MobileNumber)
        {
            RaceResultData lData = new RaceResultData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.Forecast(ClubID, MobileNumber), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        //public ActionResult GetLocation()
        //{
        //    RaceResultData lData = new RaceResultData();
        //    var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        //    var jsonResult = new ContentResult
        //    {
        //        Content = JsonConvert.SerializeObject(lData.Forecast(ClubID, MobileNumber), settings),
        //        ContentType = "application/json"
        //    };

        //    return jsonResult;
        //}

        private void GetAutehnCookies()
        {
            HttpCookie cookieUserName = HttpContext.Request.Cookies.Get("MavcPortalUserName");
            HttpCookie cookiePassword = HttpContext.Request.Cookies.Get("MavcPortalPassword");
            if (cookieUserName != null)
            {
                string username = cookieUserName.Value;
                string password = cookiePassword.Value;
                @ViewBag.cookiesUserName = username;
                @ViewBag.cookiesPassword = password;
            }
        }

        private string GetUserID()
        {
            HttpCookie cookieUserName = HttpContext.Request.Cookies.Get("MavcPortalUserName");
            string username = "";
            if (cookieUserName != null)
            {
                username = cookieUserName.Value;
            }

            return username;
        }
    }
}
