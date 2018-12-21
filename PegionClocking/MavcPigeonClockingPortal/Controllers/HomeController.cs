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
            DAL.Common common = new DAL.Common();

            var dt = lData.ForgotPassword(forgotPasswordData).Tables[0];
            response.Message = LWT.Common.LWTSafeTypes.SafeString(dt.Rows[0]["ErrMsg"]);

            if (response.Message.Contains("Your password will send to your mobile."))
            {
                string[] value = response.Message.Split('|');
                response.Message = value[0];
                var sms = "Your Password: " + value[1];
                //send password via sms
                var ret = itexmo(forgotPasswordData.MobileNumber, sms, "PR-MARKA754822_4H5EX");
                if (ret.ToString() == "0")
                {
                    common.ChargeText(forgotPasswordData.MobileNumber);
                    response.Message = value[0];
                }
                else
                {
                    response.Message = "Error detected while password is sending on your mobile number.";
                }
            }
            else if (response.Message.Contains("OTP"))
            {
                string[] value = response.Message.Split('|');
                var sms = "Your Security Code: " + value[1];
                response.Remarks = "OTP";

                //send security code via sms
                var ret = itexmo(forgotPasswordData.MobileNumber, sms, "PR-MARKA754822_4H5EX");
                if (ret.ToString() == "0")
                {
                    common.ChargeText(forgotPasswordData.MobileNumber);
                    response.Message = "To verify your request, please enter the security code sent on your mobile number.";
                }
                else
                {
                    response.Message = "Error detected while sending security code on your mobile number. Please click [Change Password] button again.";
                }
            }

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

         private object itexmo(string Number, string Message, string API_CODE, Boolean isImportant = false)
        {
            object functionReturnValue = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = "https://www.itexmo.com/php_api/api.php";
                parameter.Add("1", Number);
                parameter.Add("2", Message);
                parameter.Add("3", API_CODE);

                if (isImportant)
                {
                    parameter.Add("5", "HIGH");
                }
                dynamic rpb = client.UploadValues(url, "POST", parameter);
                functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
            }
            return functionReturnValue;
        }
    }
}
