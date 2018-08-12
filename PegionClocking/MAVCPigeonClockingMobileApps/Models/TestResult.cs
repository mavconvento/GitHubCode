using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using MAVCPigeonClockingMobileApps;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
namespace MAVCPigeonClockingMobileApps.Models
{
    public class TestResult
    {
        public Int64 ID { get; set; }
        public String Description { get; set; }


        public TestResult GetTestReult(DataRow dataRow)
        {
            try
            {

               
                return new TestResult()
                {
                    ID = LWT.Common.LWTSafeTypes.SafeInt64(dataRow["ID"]),
                    Description = LWT.Common.LWTSafeTypes.SafeString(dataRow["Description"]),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}