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
    public class MobileRegistrationController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult MobileRegistration()
        {
            ViewData["clubname"] = tools.GetClubList(LWT.Common.LWTSafeTypes.SafeString("+639173540062"), "");
            return View();
        }

        [HttpPost]
        public JsonResult NewMemberRegistrationStep1(String ClubName, String MemberID, String MobileNo)
        {
            MAVCPigeonClockingMobileApps.Models.MobileRegistration mobileRegistration = new MAVCPigeonClockingMobileApps.Models.MobileRegistration();
            mobileRegistration.ClubName = ClubName;
            mobileRegistration.MemberID = MemberID;
            mobileRegistration.MobileNumber = MobileNo;
            return Json(mobileRegistration.MobileRegistrationStep1(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewMemberRegistrationStep2(String Password)
        {
            MAVCPigeonClockingMobileApps.Models.MobileRegistration mobileRegistration = new MAVCPigeonClockingMobileApps.Models.MobileRegistration();
            mobileRegistration.Password = Password;
            return Json(mobileRegistration.MobileRegistrationStep2(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewMemberRegistrationStep3(String ActivationCode)
        {
            MAVCPigeonClockingMobileApps.Models.MobileRegistration mobileRegistration = new MAVCPigeonClockingMobileApps.Models.MobileRegistration();
            mobileRegistration.ActivationCode = ActivationCode;
            return Json(mobileRegistration.MobileRegistrationStep3(), JsonRequestBehavior.AllowGet);
        }

    }
}
