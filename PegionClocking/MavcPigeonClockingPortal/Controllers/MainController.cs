using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MavcPigeonClockingPortal.Controllers
{
    [Filter.CheckSessionOut]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class MainController : Controller
    {
        //
        // GET: /Main/
        public ActionResult RaceResult()
        {
            GetAuthenCookies();
            return View();
        }


        public ActionResult Maps()
        {
            return View();
        }

        public ActionResult ViewLogs()
        {
            GetAuthenCookies();
            return View();
        }

        public ActionResult MyProfile()
        {
            GetAuthenCookies();
            return View();
        }

        public ActionResult MemberDistance()
        {
            ViewBag.ClubID =LWT.Common.LWTSafeTypes.SafeString(Request.QueryString["ClubID"]);
            var memberid = LWT.Common.LWTSafeTypes.SafeString(Request.QueryString["MemberIDNo"]).Replace('|', '#');
            ViewBag.MemberIDNo = memberid;
            GetAuthenCookies();
            return View();
        }

        private void GetAuthenCookies()
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
    }

}
