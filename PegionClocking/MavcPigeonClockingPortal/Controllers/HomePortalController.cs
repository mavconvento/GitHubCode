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
    public class HomePortalController : Controller
    {
        //
        // GET: /HomePortal/

        public ActionResult Index()
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

            return View();
        }

        public ActionResult TimeoutRedirect()
        {
            return View();
        }

    }
}
