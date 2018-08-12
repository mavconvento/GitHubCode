using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Diagnostics;
using MAVCPigeonClockingMobileApps.Models;

namespace MAVCPigeonClockingMobileApps.Filter
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
						{ "Controller", "PortalHome" },{ "Action", "Index" }
						});
            }
            else
            {
                if (ctx.Session["CustomerID"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {  
						{ "Controller", "PortalHome" },{ "Action", "TimeoutRedirect" }
						});
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

}