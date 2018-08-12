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

namespace MAVCPigeonClockingMobileApps.Constants
{
    
    public class CommonConstants
    {
        public const string DATABASE_NAME = "LoanServ";

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
    }

	public class IconMappings
	{
		XmlNode iconNode;
		public IconMappings ( string IconName ){
			XmlDocument ConstantXml;
			string lmsConstantPath = HttpContext.Current.Server.MapPath("~/Constants/IconMappings.xml");
			ConstantXml = new XmlDocument();
			ConstantXml.Load(lmsConstantPath);
			iconNode = ConstantXml.SelectSingleNode("/icons/icon[@name='" + IconName + "']");
		}
 
		public string GetIcon(){
			return XmlTools.GetNodeAttributeValue(iconNode, "icon");
		}

		public string GetClass()
		{
			return XmlTools.GetNodeAttributeValue(iconNode, "class");
		}

	}

	public class AddressLine
	{		
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
	}
    //[1][107.20]
    public class FundingSource
    {
        public static string LMSSource = "LMS";
        public static string PortalSource = "Portal";
    }

    public enum FundingStatusType
    {
        NotFunded,
        Pending,
        Funded,
        Settled
    }
    //[1][107.20]

}