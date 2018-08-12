using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MAVCPigeonClockingMobileApps.Constants;
using MAVCPigeonClockingMobileApps.Models;
using System.Xml;

namespace MAVCPigeonClockingMobileApps.Controllers
{
     [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class HomeController : Controller
    {
          lmsConstants oConst = new lmsConstants();

        public HomeController()
        {
			//if ( Session.Contents.i != null  ){
			//    if (LWT.Common.LWTSafeTypes.SafeString(Session["CustomerID"]) != "" ) {
			//        tools.CustomerFileNoteSave(LWT.Common.LWTSafeTypes.SafeInt(Session["CustomerID"]), "Customer Logged out");
			//        FormsAuthentication.SignOut();
			//        Session.Clear();
			//    }
			//}
			if ( Session != null )Session.Clear();


            ViewBag.phone = oConst.GetElement("companycontactphone");
            ViewBag.email = oConst.GetElement("companycontactemail");
            ViewBag.link = oConst.GetElement("companywebsiteurl");
            ViewBag.privacylink = oConst.GetElement("companywebsiteurlprivacy");
            ViewBag.companyname = oConst.GetElement("companyname");
            ViewBag.help = oConst.GetElement("homehelp");
            ViewBag.logoSrcSml = oConst.GetElement("logoSrcSml");
            ViewBag.termsAndConditions = oConst.GetElement("termsandconditions");
            ViewBag.textformat = oConst.GetElement("companytextformaturl");
        }

        //[AllowAnonymous]
        public ActionResult Index()
        {
            var items = new List<SocialMedia>();
            foreach (XmlNode node in oConst.GetNodeList("/constants/SocialMediaIcons/icon"))
            {
                SocialMedia sm = new SocialMedia();
                sm.src = Url.Content(XmlTools.NothingToString(node.SelectSingleNode("src")));
				sm.url = XmlTools.NothingToString(node.SelectSingleNode("url"));
                items.Add(sm);
            }
            ViewBag.smlist = items;

            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult SignedOut()
        {
            return View();
        }

        public ActionResult NeedAssistance()
        {
            return View();
        }

        public ActionResult TimeoutRedirect()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ContactUs()
        {
            return View();
        }
    }
}
