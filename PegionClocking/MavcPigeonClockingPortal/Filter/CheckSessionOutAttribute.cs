using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Diagnostics;
using MavcPigeonClockingPortal.Models;

namespace MavcPigeonClockingPortal.Filter
{
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpContext ctx = HttpContext.Current;

            // If the browser session or authentication session has expired...
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {  
						{ "Controller", "Home" },{ "Action", "Login" }
						});
            }
            //else
            //{
            //    if (ctx.Session["UserID"] == null)
            //    {
            //        FormsAuthentication.SignOut();
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {  
            //            { "Controller", "Home" },{ "Action", "Login" }
            //            });
            //    }
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}