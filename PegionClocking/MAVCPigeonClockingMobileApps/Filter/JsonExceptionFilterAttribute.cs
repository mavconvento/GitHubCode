using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MAVCPigeonClockingMobileApps.Constants;

namespace MAVCPigeonClockingMobileApps.Filter
{
	public class JsonExceptionFilterAttribute : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
			{
				filterContext.HttpContext.Response.StatusCode = 500;
				filterContext.ExceptionHandled = true;
				lmsConstants oConst = new lmsConstants();				
				filterContext.Result = new JsonResult
				{
					Data = new
					{
						// obviously here you could include whatever information you want about the exception
						// for example if you have some custom exceptions you could test
						// the type of the actual exception and extract additional data
						// For the sake of simplicity let's suppose that we want to
						// send only the exception message to the client
						errorMessage = oConst.GetElement("jsonerrormessage") //  filterContext.Exception.Message
					},
					JsonRequestBehavior = JsonRequestBehavior.AllowGet
				};				
			}
		}
	}
}