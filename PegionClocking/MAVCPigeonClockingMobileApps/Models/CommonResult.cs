using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAVCPigeonClockingMobileApps;
using System.Text.RegularExpressions;

namespace MAVCPigeonClockingMobileApps.Models
{
    public class CommonResult
    {
        public String Value { get; set; }

        //public static string EncodedMultiLineText(this HtmlHelper helper, string text)
        //{
        //    if (String.IsNullOrEmpty(text))
        //    {
        //        return String.Empty;
        //    }
        //    return Regex.Replace(helper.Encode(text), Environment.NewLine, "<br/>");
        //}

        public static CommonResult GetCommonReturnItem(DataRow dataRow)
        {
            try
            {
                return new CommonResult()
                {
                    Value = LWT.Common.LWTSafeTypes.SafeString(dataRow["Value"]),
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}