using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MAVCPigeonClockingMobileApps.Constants;

namespace MAVCPigeonClockingMobileApps
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filter.JsonExceptionFilterAttribute());

            //filters.Add(new System.Web.Mvc.AuthorizeAttribute()); //applys Authorize Attribute to site, use [AllowAnonymous] to allow Anonymous access
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/

            //routes.MapRoute(
            // "Cuisine",
            // "cuisine/{name}",  name is the parameter
            //new { Controller = "Cuisine", action = "Search", name = "" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            //GlobalConfiguration.Configuration.Filters.Add(new System.Web.Http.AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterApplicationVariables();
        }

        public static void RegisterApplicationVariables()
        {
            lmsConstants oConst = new lmsConstants();
            HttpContext.Current.Application["installationid"] = oConst.GetElement("installation");
            HttpContext.Current.Application["includethirdparty"] = oConst.GetElement("includethirdparty");

        }

    }
}