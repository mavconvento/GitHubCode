using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Configuration;
//[1][107.20] Edson 05/05/2014 : CR 17 : when called by LMS value = 'LMS' and CustomerPortal = 'Portal' lmsCustomerPortalAccountRedraw. added new parameter FundingStatus and RequestSource

//using System;
//using System.Collections.Generic;
using System.Data;
//using System.Linq;
using System.Reflection;
//using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Web.Security;

namespace MavcPigeonClockingPortal.Constants
{
    public class CookieStore
    {
        //public static void SetCookie(string key, string value, TimeSpan expires)
        //{
        //    HttpCookie encodedCookie = HttpSecureCookie.Encode(new HttpCookie(key, value));

        //    if (HttpContext.Current.Request.Cookies[key] != null)
        //    {
        //        var cookieOld = HttpContext.Current.Request.Cookies[key];
        //        cookieOld.Expires = DateTime.Now.Add(expires);
        //        cookieOld.Value = encodedCookie.Value;
        //        HttpContext.Current.Response.Cookies.Add(cookieOld);
        //    }
        //    else
        //    {
        //        encodedCookie.Expires = DateTime.Now.Add(expires);
        //        HttpContext.Current.Response.Cookies.Add(encodedCookie);
        //    }
        //}
        //public static string GetCookie(string key)
        //{
        //    string value = string.Empty;
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

        //    if (cookie != null)
        //    {
        //        // For security purpose, we need to encrypt the value.
        //        HttpCookie decodedCookie = HttpSecureCookie.Decode(cookie);
        //        value = decodedCookie.Value;
        //    }
        //    return value;
        //}

    }
}