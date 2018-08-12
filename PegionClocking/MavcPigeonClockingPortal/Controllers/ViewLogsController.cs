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
using MavcPigeonClockingPortal.Filter;


namespace MavcPigeonClockingPortal.Controllers
{
    public class ViewLogsController : Controller
    {
        //
        // GET: /ViewLogs/
        public ActionResult GetViewLogs(String ClubID,String MobileNumber,String Keyword,DateTime DateFrom, DateTime DateTo)
        {

            ViewLogsData lData = new ViewLogsData();
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(lData.GetSMSLogs(ClubID, MobileNumber, Keyword, DateFrom, DateTo), settings),
                ContentType = "application/json"
            };

            return jsonResult;
        }

    }
}
