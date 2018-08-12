using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LWT.Common;
using MAVCPigeonClockingMobileApps;

namespace MAVCPigeonClockingMobileApps.Constants
{
    public class Error
    {
        DAL.Error error;
        public Int64 iErrCode { get; set; }
        public string cErrCode { get; set; }
        public string cErrMsg { get; set; }
        public string cAction { get; set; }

        public string GetErrorListSearch()
        {
            DataTable dataTable = null;

            try
            {
                cErrMsg = "";
                StoreDataToDataLayer();
                dataTable = error.GetErrorListSearch();
                if (dataTable.Rows.Count == 1)
                {
                    cErrCode = LWTSafeTypes.SafeString(dataTable.Rows[0]["cErrCode"].ToString());
                    cErrMsg = LWTSafeTypes.SafeString(dataTable.Rows[0]["cErrMsg"].ToString());
                    cAction = LWTSafeTypes.SafeString(dataTable.Rows[0]["cAction"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (cErrMsg);
        }

        public void StoreDataToDataLayer()
        {
            error = new DAL.Error();
            error.IErrCode = LWT.Common.LWTSafeTypes.SafeInt64(iErrCode);
        }

        public string ConcatenateError(string message, string errormessage)
        {
            if (message == "")
            {
                message = errormessage;
            }
            else
            {
                message = message + "</br>" + errormessage;
            }
            return message;
        }
    }
}