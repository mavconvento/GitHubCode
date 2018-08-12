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

namespace MavcPigeonClockingPortal.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OurProducts()
        {
            return View();
        }

        public ActionResult PigeonProducts()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Tutorials()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            ClearAuthenCookies();
            return View();
        }

        public JsonResult UserLogin(LoginData loginData)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                LoginData lData = new LoginData();
                //DataSet dt = new DataSet();
                lmsConstants lmsConstant = new lmsConstants();

                var dt = lData.ValidateLogin(loginData).Tables[0];
                if (LWT.Common.LWTSafeTypes.SafeInt64(dt.Rows[0]["loginID"]) > 0)
                {
                    FormsAuthentication.SetAuthCookie(loginData.UserName, true);
                    HttpCookie cookieUserName = new HttpCookie("MavcPortalUserName");
                    HttpCookie cookiePassword = new HttpCookie("MavcPortalPassword");

                    HttpCookie Username = new HttpCookie("Username");
                    cookieUserName.Value = LWT.Common.LWTSafeTypes.SafeString(dt.Rows[0]["UserID"]);
                    cookieUserName.Expires = DateTime.Now.AddDays(365);

                    cookiePassword.Value = loginData.Password;
                    cookiePassword.Expires = DateTime.Now.AddDays(365);

                    Response.Cookies.Add(cookieUserName);
                    Response.Cookies.Add(cookiePassword);

                    response.Message = "Success";
                }
                return Json(new { data = response }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Remarks = ex.Message;
                return Json(new { data = response }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAdvertisement()
        {
            LoginData lData = new LoginData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetAdvertisement(), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public JsonResult GetMemberSessionID()
        {
            GetAutehnCookies();
            String memberSessionID = LWT.Common.LWTSafeTypes.SafeString(@ViewBag.cookiesUserName);
            return Json(new { MemberSessionID = memberSessionID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMenus()
        {
            RaceResult lData = new RaceResult();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetMenu(), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

        public JsonResult GetForgotPassword(ForgotPasswordData forgotPasswordData)
        {
            JsonResponse response = new JsonResponse();
            LoginData lData = new LoginData();

            var dt = lData.ForgotPassword(forgotPasswordData).Tables[0];
            response.Message = LWT.Common.LWTSafeTypes.SafeString(dt.Rows[0]["ErrMsg"]);

            return Json(new { data = response }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrentLoginName()
        {
            GetAutehnCookies();
            String UserName = LWT.Common.LWTSafeTypes.SafeString(@ViewBag.cookiesUserName);
            String SubMemberName = "";
            String SubMemberID = "";
            Boolean IsShowSubMemberSection = true;

            if (LWT.Common.LWTSafeTypes.SafeString(Session["subMemberID"]) != "")
            {
                SubMemberID = LWT.Common.LWTSafeTypes.SafeString(Session["subMemberID"]);
                IsShowSubMemberSection = false;
                SubMemberName = LWT.Common.LWTSafeTypes.SafeString(Session["subMemberName"]);
            }

            return Json(new { UserName = UserName, IsShowSubMemberSection = IsShowSubMemberSection, SubMemberName = SubMemberName, SubMemberID = SubMemberID }, JsonRequestBehavior.AllowGet);
        }

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

        private void ClearAuthenCookies()
        {
            HttpCookie cookieUserName = new HttpCookie("MavcPortalUserName");
            HttpCookie cookiePassword = new HttpCookie("MavcPortalPassword");

            HttpCookie Username = new HttpCookie("Username");
            //cookieUserName.Value = LWT.Common.LWTSafeTypes.SafeString(dt.Rows[0]["UserID"]);
            cookieUserName.Expires = DateTime.Now.AddDays(-1);

            //cookiePassword.Value = loginData.Password;
            cookiePassword.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(cookieUserName);
            Response.Cookies.Add(cookiePassword);
        }

    }
}
