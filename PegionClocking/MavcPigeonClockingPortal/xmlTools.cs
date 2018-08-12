using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
//using System.Security.Cryptography;
//using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Security;
using System.Net.Security;
//using System.Security.Cryptography.X509Certificates;
using System.Xml;
using LWT.Common;

/// <summary>
/// </summary>
/// <remarks></remarks>
public class XmlTools
{
	public class LIXI
	{
		public static Nullable<System.DateTime> GetLixiDate(XmlElement oDateXmlElement)
		{
			try
			{
				System.DateTime rtnDate = default(System.DateTime);
				string cDateValue = XmlTools.NothingToString(oDateXmlElement, "Day");
				cDateValue = cDateValue + "/" + XmlTools.NothingToString(oDateXmlElement, "Month");
				cDateValue = cDateValue + "/" + XmlTools.NothingToString(oDateXmlElement, "Year");

				if (System.DateTime.TryParse(cDateValue, out rtnDate))
				{
					return rtnDate;
				}
				else
				{
					return (new Nullable<System.DateTime>());
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
	
	public static double GetNullableDoubleValue(Nullable<double> oValue)
	{
		if (oValue.HasValue)
		{
			return oValue.Value;
		}
		else
		{
			return 0;
		}
		//Return IIf(oValue.HasValue, oValue.Value, DBNull.Value)
	}

	public static object GetNullableValue(Nullable<int> oValue)
	{
		if (oValue.HasValue)
		{
			return oValue.Value;
		}
		else
		{
			return DBNull.Value;
		}
		//Return IIf(oValue.HasValue, oValue.Value, DBNull.Value)
	}

	public static object GetNullableValue(Nullable<double> oValue)
	{
		if (oValue.HasValue)
		{
			return oValue.Value;
		}
		else
		{
			return DBNull.Value;
		}
	}

	public static object GetNullableValue(Nullable<System.DateTime> oValue)
	{
		if (oValue.HasValue)
		{
			return oValue.Value;
		}
		else
		{
			return DBNull.Value;
		}
	}

	public static object GetNullableValue(string cValue)
	{
		if (!string.IsNullOrEmpty(cValue))
		{
			return cValue;
		}
		else
		{
			return DBNull.Value;
		}
	}


	public static double GetNodeListTotal(XmlNodeList oNodeList, string cNodeName)
	{
		try
		{
			double fTotal = 0;
			if ((oNodeList != null))
			{
				foreach (XmlNode oNode in oNodeList)
				{
					fTotal = fTotal + XmlTools.NothingToZeroDouble(oNode.SelectSingleNode(cNodeName));
				}
			}

			return fTotal;
		}
		catch (Exception ex)
		{
			return 0;
		}
	}

	//public static Nullable<int> GetNodeLookupID(XmlNode oNode, int iLookupCatID, bool bLixi = false)
	//{
	//    try
	//    {
	//        string cValue = tools.NothingToString(oNode);

	//        return GetLookupNullableValue(iLookupCatID, cValue, bLixi);

	//    }
	//    catch (Exception ex)
	//    {
	//        throw ex;
	//    }
	//}

	//public static Nullable<int> GetLookupNullableValue(int iLookupCatId, string cLookupItemName, bool bLixi)
	//{
	//    try
	//    {
	//        int iRtnValue = 0;

	//        if (!string.IsNullOrEmpty(cLookupItemName))
	//        {
	//            iRtnValue = GetLookupValue(iLookupCatId, cLookupItemName, bLixi);
	//            if (iRtnValue == -1)
	//            {
	//                return (new Nullable<int>());
	//            }
	//            else
	//            {
	//                return iRtnValue;
	//            }
	//        }
	//        else
	//        {
	//            return (new Nullable<int>());
	//        }
	//    }
	//    catch (Exception ex)
	//    {
	//        throw ex;
	//    }

	//}

	//public static Nullable<int> GetNodeLookupID(XmlNode oNode, string oAttributeName, int iLookupCatID, bool bLixi = false)
	//{
	//    try
	//    {
	//        string cValue = tools.NothingToString(oNode, oAttributeName, bLixi);
	//        return GetLookupNullableValue(iLookupCatID, cValue, bLixi);

	//    }
	//    catch (Exception ex)
	//    {
	//        throw ex;
	//    }
	//}

	public static int GetNodeYesNoToInt(XmlNode oNode)
	{
		try
		{
			string cValue = XmlTools.NothingToString(oNode);
			if (!string.IsNullOrEmpty(cValue))
			{
				if (cValue.ToUpper() == "YES"){
					return 1;
				}
				else
				{
					return 0;
				}
			}
			else
			{
				return 0;
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public static Nullable<int> GetNodeYesNoToNullableInt(XmlNode oNode)
	{
		try
		{
			string cValue = XmlTools.NothingToString(oNode);
			if (!string.IsNullOrEmpty(cValue))
			{
				if (cValue.ToUpper() == "YES")
				{
					return 1;
				}
				else
				{
					return 0;
				}
			}
			else
			{
				return new Nullable<int>();
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
	
	public static bool IsNullableType(Type myType)
	{
		return (myType.IsGenericType) && (object.ReferenceEquals(myType.GetGenericTypeDefinition(), typeof(Nullable<>)));
	}


	public static string EmptyToString(XmlNode oNode)
	{
		if ((oNode == null))
		{
			return "";
		}
		else
		{
			return oNode.InnerText;
		}
	}

	   
	    
	public static string NothingToString(XmlNode oNode, bool bModifySingleQuote = false)
	{
		if ((oNode == null))
		{
			return "";
		}
		else
		{
			return GetSafeStringFromXML(oNode.InnerText, bModifySingleQuote);
		}
	}

	public static string NothingToString(XmlNode oNode, string cAttributeName, bool bModifySingleQuote = false)
	{
		string cRtn = null;
		if ((oNode == null))
		{
			return "";
		}
		else
		{
			cRtn = GetNodeAttributeValue(oNode, cAttributeName);
			return GetSafeStringFromXML(cRtn, bModifySingleQuote);
		}
	}

	public static Nullable<System.DateTime> NothingToNullableDate(XmlNode oNode)
	{
		if ((oNode == null) || string.IsNullOrEmpty(oNode.InnerText))
		{
			return (new Nullable<System.DateTime>());
		}
		else
		{
			System.DateTime dRtnDate = default(System.DateTime);
			if (System.DateTime.TryParse(oNode.InnerText, out dRtnDate))
			{
				return dRtnDate;
			}
			else
			{
				return (new Nullable<System.DateTime>());
			}
		}
	}

	public static Nullable<System.DateTime> NothingToNullableDate(XmlNode oNode, string cAttributeName)
	{
		if ((oNode == null))
		{
			return (new Nullable<System.DateTime>());
		}
		else
		{
			System.DateTime dRtnDate = default(System.DateTime);
			string cDateStr = null;
			if ((oNode.Attributes.GetNamedItem(cAttributeName) == null))
			{
				return (new Nullable<System.DateTime>());
			}
			else
			{
				cDateStr = oNode.Attributes.GetNamedItem(cAttributeName).InnerText;
				if (System.DateTime.TryParse(cDateStr, out dRtnDate))
				{
					return dRtnDate;
				}
				else
				{
					return (new Nullable<System.DateTime>());
				}
			}
		}
	}

	/// <summary>
	/// handles True/False or 1/0
	/// </summary>
	/// <param name="oNode"></param>
	/// <returns></returns>
	/// <remarks></remarks>
	public static Nullable<bool> NothingToNullableBoolean(XmlNode oNode)
	{
		if ((oNode == null) || string.IsNullOrEmpty(oNode.InnerText))
		{
			return (new Nullable<bool>());
		}
		else
		{
			switch (oNode.InnerText.ToUpper())
			{
				case "1":
				case "TRUE":
					return true;
				case "0":
				case "FALSE":
					return false;
				default:
					return (new Nullable<bool>());
			}
		}
	}


	public static Nullable<double> NothingToNullableDouble(XmlNode oNode)
	{
		if ((oNode == null) || string.IsNullOrEmpty(oNode.InnerText))
		{
			return (new Nullable<double>());
		}
		else
			return NothingToNullabledouble(oNode.InnerText);
	}

	public static string GetNodeAttributeValue(XmlNode oNode, string cAttributeName)
	{
		try
		{
			if ((oNode == null))
			{
				return "";
			}
			else
			{
				if ((oNode.Attributes.GetNamedItem(cAttributeName) == null))
				{
					return "";
				}
				else
				{
					return oNode.Attributes.GetNamedItem(cAttributeName).InnerText;
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public static Nullable<double> NothingToNullabledouble(string cvalue)
	{
		double rtnvalue;
		if (double.TryParse(cvalue, out rtnvalue))
			return rtnvalue;
		else
			return (new Nullable<double>());
	}


	public static Nullable<double> NothingToNullableDouble(XmlNode oNode, string cAttributeName)
	{
		try
		{
			string cValue = GetNodeAttributeValue(oNode, cAttributeName);
			if (string.IsNullOrEmpty(cValue))
			{
				return (new Nullable<double>());
			}
			else
				return NothingToNullabledouble(cValue);
		}
		catch (Exception ex)
		{
			throw ex;
		}

	}

	public static Nullable<int> NothingToNullableInt(XmlNode oNode, string cAttributeName)
	{
		string cValue = GetNodeAttributeValue(oNode, cAttributeName);
		
		if (string.IsNullOrEmpty(cValue))
		{
			return (new Nullable<int>());
		}
		else
		{
			 return (NothingToNullableInt(cValue));
		}
	}

	public static Nullable<int> NothingToNullableInt(string cvalue){
		int rtnvalue;
		if (int.TryParse(cvalue, out rtnvalue))
			return rtnvalue;
		else
			return (new Nullable<int>());
	}

	public static Nullable<int> NothingToNullableInt(XmlNode oNode)
	{
		if ((oNode == null) || string.IsNullOrEmpty(oNode.InnerText))
		{
			return (new Nullable<int>());
		}
		else
		{
			return (NothingToNullableInt(oNode.InnerText));			
		}
	}


	public static int NothingToZeroInt(XmlNode oNode, string cAttributeName)
	{
		string cValue = GetNodeAttributeValue(oNode, cAttributeName);
		if (string.IsNullOrEmpty(cValue))
		{
			return 0;
		}
		else
		{
			return NothinoToZeroInt(cValue);
		}
	}

	public static int NothingToZeroInt(XmlNode oNode)
	{
		if ((oNode == null) || string.IsNullOrEmpty(oNode.InnerText))
		{
			return 0;
		}
		else
		{
			return NothinoToZeroInt(NothingToString(oNode));
		}
	}

	public static int NothinoToZeroInt(string cValue)
	{
		int iOutput = 0;
		if (int.TryParse(cValue, out iOutput))
		{
			return iOutput;
		}
		else
		{
			return 0;
		}
	}

	
	public static double NothingToZeroDouble(XmlNode oNode)
	{
		try
		{
			if ((oNode == null) || string.IsNullOrEmpty(oNode.InnerText))
			{
				return 0;
			}
			else
			{
				double rtnvalue;
				if ( double.TryParse(oNode.InnerText, out rtnvalue ) )
					return rtnvalue;
				else
					return 0;
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public static double NothingToZeroDouble(XmlNode oNode, string cAttributeName)
	{
		try
		{
			string cValue = GetNodeAttributeValue(oNode, cAttributeName);
			double fRtnValue = 0;

			if (string.IsNullOrEmpty(cValue))
			{
				return 0;
			}
			else
			{
				if (double.TryParse(cValue, out fRtnValue))
				{
					return fRtnValue;
				}
				else
				{
					return 0;
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	 
	public static string GetQualifiedNullString(string strXMLString, bool FormatAsString)
	{
		try
		{
			string rtnValue = strXMLString;

			if (string.IsNullOrEmpty(strXMLString.Trim()))
			{
				rtnValue = "NULL";
			}
			else
			{
				if (FormatAsString == true)
					rtnValue = "'" + strXMLString + "'";
			}

			return rtnValue;

		}
		catch (Exception ex)
		{
			throw ex;
		}

	}

	public static string GetSafeStringFromXML(string strXMLString, bool bModifySingleQuote = true)
	{
		try
		{
			if (strXMLString.Length == 0)
			{
				strXMLString = "";
			}
			else
			{
				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "&amp;slash", "/");
				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "&slash", "/");
				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "slash", "/");
				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "&amp;", "&");
				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "&apos;", "'");

				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "&lt;", "<");
				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "&gt;", ">");
				strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "&quot;", "\"");

				if (bModifySingleQuote == true)
				{
					strXMLString = Strings.ReplaceWithRecurisive(strXMLString, "'", "''");
					//--- for sql string and sql string in SP
				}

			}

			//  If strXMLString = "" Then strXMLString = """" 'Add "" to value to persist field has a blank value
			return strXMLString;
		}
		catch (Exception ex)
		{
			throw ex;
			return "";
		}
	}

	public static string GetBooleanString(string sValue)
	{

		try
		{
			bool b = false;
			switch (sValue.ToUpper())
			{
				case "YES":
					b = true;
					break;
				case "NO":
					b = false;
					break;
				default:
					b = Convert.ToBoolean(sValue);
					break;
			}
			if (b)
			{
				return "1";
			}
			else
			{
				return "0";
			}
		}
		catch (Exception ex)
		{
			return "NULL";
		}

	}
	 

	public static string XMLDocumentToXMLString(ref XmlDocument oXML)
	{
		try
		{
			StringWriter sw = new StringWriter();
			XmlTextWriter xw = new XmlTextWriter(sw);
			oXML.WriteTo(xw);
			return sw.ToString();

		}
		catch (Exception ex)
		{
			throw ex;
		}

	}
	 
}