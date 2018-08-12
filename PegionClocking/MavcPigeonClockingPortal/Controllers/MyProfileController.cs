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
    public class MyProfileController : Controller
    {
        //
        // GET: /MyProfile/
        public ActionResult GetMobileList(string mobilenumber)
        {
            MyProfileData lData = new MyProfileData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetRegisterMobileList(mobilenumber), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult UnregMobileNumber(string ClubID, string MobileNumber)
        {
            MyProfileData lData = new MyProfileData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.UnregMobileNumber(ClubID, MobileNumber), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult LoadMavcCard(string ClubID, string MobileNumber, string PinNumber)
        {
            MyProfileData lData = new MyProfileData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.LoadMavcCard(ClubID, MobileNumber, PinNumber), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult Pasaload(string MobileNumberFrom, string MobileNumberTo, string Amount)
        {
            MyProfileData lData = new MyProfileData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.Pasaload(MobileNumberFrom, MobileNumberTo, Amount), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public ActionResult GetMemberDistance(string ClubID, String MemberID)
        {
            MyProfileData lData = new MyProfileData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetMemberDistance(ClubID, MemberID), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }
    }
}
