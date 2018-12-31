using Microsoft.VisualBasic;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using LWT.Common;
using LWT.Common.Biz;
using Telerik.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace WebRaceResult.Common
{
    public class Common
    {
        public static void FillDropdownList(RadComboBox rcbMe, DataTable contents, string textField, string valueField, bool AddBlankItem, string valueSelected)
        {
            RadComboBoxItem itemEmpty = new RadComboBoxItem();

            rcbMe.Items.Clear();
            if (AddBlankItem == true)
            {
                itemEmpty.Text = "All";
                itemEmpty.Value = "All";
                rcbMe.Items.Add(itemEmpty);
                itemEmpty.DataBind();
            }

            foreach (DataRow dataRow in contents.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = (string)dataRow[textField];
                item.Value = dataRow[valueField].ToString();
                rcbMe.Items.Add(item);
                item.DataBind();
            }

            if (valueSelected != null)
            {
                rcbMe.SelectedValue = valueSelected;
            }
        }

        public static void RaiseMessage(RadWindowManager radWindowManager, string msg, string System, string Page,
               string Method, string Message, string title, int width = 350, int height = 150, string callBackFnName = "")
        {
            // Update the error to database
            //LogUIError(System, Page, Method, Message.Trim(), msg);

            //PD: To use this method, RadWindowManager must be present in your page...
            // Display the msg to screen
            msg = GetJavaSafeString(msg);
            radWindowManager.RadAlert(msg, width, height, title, callBackFnName);
        }

        public static string GetJavaSafeString(string msg)
        {
            msg = msg.Replace("\n", " ");
            msg = msg.Replace("\r", " ");
            msg = msg.Replace("\\", "");
            msg = msg.Replace("'", "");

            return msg;
        }
    }
}