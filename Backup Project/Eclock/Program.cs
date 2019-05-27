using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Eclock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 



        [STAThread]
        static void Main()
        {
            if (!BIZ.Common.CheckSystemConfig("SystemConfig"))
            {
                if (!BIZ.Common.GetSystemConfig("SystemConfig"))
                {
                    MessageBox.Show("Please insert your MAVC Eclock SD Card.");
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (BIZ.Common.GetSystemConfigValue("systemType") == "Application Administrator")
            {
                Application.Run(new frmMainMenuAdmin());
            }
            else
            {
                Application.Run(new frmMainMenuMember());
            }

        }

        
    }
}
