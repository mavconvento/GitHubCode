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
using MAVCPigeonClockingMobileApps.Constants;
using MAVCPigeonClockingMobileApps.Models;

namespace MAVCPigeonClockingMobileApps.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Index

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        
        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ValidateLogin(string UserName, string Password, bool RememberMe, string Uniquewindowid)
        {
            JsonResponse response = new JsonResponse();
            response.Success = false;
            Boolean firstTimeLogin = false;

            try
            {
                response.Message = "Sign in error. The Mobile Np. you entered is registered. Please try again.";

                if (UserName == "")
                {
                    response.Message = "Mobile No. is required";
                    return Json(new { response = response }, JsonRequestBehavior.AllowGet);
                }

                if (Password == "")
                {
                    response.Message = "Password is required";
                    return Json(new { response = response }, JsonRequestBehavior.AllowGet);
                }

                Member member = new Member();
                DataSet userInfo = member.AuthenticateLogin(UserName, Password, "Mobile");

                if (userInfo != null)
                {
                    DataTable userDataRows = userInfo.Tables[0];
                    if (userDataRows.Rows.Count > 0)
                    {
                        if (userDataRows.Columns.Contains("UserID"))
                        {
                            FormsAuthentication.SetAuthCookie(UserName, RememberMe);

                            Session.Add("UserID", LWT.Common.LWTSafeTypes.SafeString(userDataRows.Rows[0]["UserID"]));
                            //Session.Add("FirstName", LWT.Common.LWTSafeTypes.SafeString(userDataRows.Rows[0]["FirstName"]));
                            //Session.Add("LastName", LWT.Common.LWTSafeTypes.SafeString(userDataRows.Rows[0]["LastName"]));
                            Session.Add("Uniquewindowid", Uniquewindowid);
                            //firstTimeLogin = !(LWT.Common.LWTSafeTypes.SafeBool(userDataRows.Rows[0]["FirstLogin"]));
                            response.Success = true;
                            response.Message = "";
                        }
                        //else if (userDataRows.Columns.Contains("LoginLimit"))
                        //{
                        //    response.Remarks = LWT.Common.LWTSafeTypes.SafeString(userDataRows.Rows[0]["Remarks"]);
                        //    response.Message = LWT.Common.LWTSafeTypes.SafeString(userDataRows.Rows[0]["LoginLimit"]);
                        //}
                        //else
                        //{
                        //    response.Remarks = LWT.Common.LWTSafeTypes.SafeString(userDataRows.Rows[0]["Remarks"]);
                        //    response.Message = LWT.Common.LWTSafeTypes.SafeString(userDataRows.Rows[0]["Error"]);
                        //}
                    }
                }

                return Json(new { response = response, firstTimeLogin = firstTimeLogin }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                lmsConstants oConst = new lmsConstants();
                if (oConst.GetElement("debugon") == "1")
                    response.Message = "System Error" + ex.ToString();
                else
                    response.Message = "System Error";


                return Json(new { response = response }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
