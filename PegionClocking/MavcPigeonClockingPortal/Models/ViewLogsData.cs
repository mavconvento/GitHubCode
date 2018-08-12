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
        public DataTable GetSMSLogs(string ClubID,String MobileNumber, String Keyword, DateTime DateFrom, DateTime DateTo)
        {
            DAL.ViewLogs viewLogs = new DAL.ViewLogs();
            DataSet dsResult = viewLogs.InboxView(ClubID,MobileNumber, Keyword, DateFrom, DateTo);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }
    }
}