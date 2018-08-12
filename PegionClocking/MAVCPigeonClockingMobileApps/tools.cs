using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Data;
using MAVCPigeonClockingMobileApps.DAL;
using System.Web.Mvc;
using MAVCPigeonClockingMobileApps.Constants;

namespace MAVCPigeonClockingMobileApps
{
    public class tools
    {

        public static SelectList GetClubList(string userID,string SelectedKey)
        {
            DataTable oState = ToolsDAL.GetClubList(userID).Tables[0];

            var items = new List<SelectListItem>();

            foreach (DataRow dataRow in oState.Rows)
            {
                items.Add(new SelectListItem { Value = dataRow["Club ID"].ToString(), Text = dataRow["Club Abbreviation"].ToString() });
            }

            var selectList = new SelectList(items, "Value", "Text", SelectedKey);

            return selectList;
        }
    }
}