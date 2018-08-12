using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using MavcPigeonClockingPortal.DAL;

namespace MavcPigeonClockingPortal.Models
{
    public class ViewLogsData
    {
        public DataTable GetSMSLogs(String MobileNumber, String Keyword, DateTime DateFrom, DateTime DateTo)
        {
            DAL.ViewLogs viewLogs = new DAL.ViewLogs();
            DataSet dsResult = viewLogs.InboxView(MobileNumber, Keyword, DateFrom, DateTo);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }
    }
}