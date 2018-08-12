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

namespace MavcPigeonClockingPortal.Constants
{
    public class CommonConstants 
    {
        public const string DATABASE_NAME = "Mavc";
    }

    public class lmsConstants
    {
        private XmlDocument ConstantXml;

        public lmsConstants()
        {
            string lmsConstantPath = HttpContext.Current.Server.MapPath("~/Constants/lmsConstants.xml");

            ConstantXml = new XmlDocument();
            ConstantXml.Load(lmsConstantPath);
        }

        public string GetElement(string elementName)
        {
            return XmlTools.NothingToString(ConstantXml.SelectSingleNode("/constants/" + elementName));
        }

        public XmlNodeList GetNodeList(string xPath)
        {
            return ConstantXml.SelectNodes(xPath);
        }

        public string GetCustomLabel(string elementName)
        {
            return XmlTools.NothingToString(ConstantXml.SelectSingleNode("/constants/customlabel/" + elementName));
        }

        public string getIPAddress(HttpRequestBase request)
        {
            string szRemoteAddr = request.UserHostAddress;
            string szXForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];
            string szIP = "";

            if (szXForwardedFor == null)
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;
                if (szIP.IndexOf(",") > 0)
                {
                    string[] arIPs = szIP.Split(',');

                    foreach (string item in arIPs)
                    {
                        return item;
                        //if (!isPrivateIP(item))
                        //{
                        //    return item;
                        //}
                    }
                }
            }
            return szIP;
        }
    }
}