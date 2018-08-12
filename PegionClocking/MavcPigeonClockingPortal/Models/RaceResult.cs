using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MavcPigeonClockingPortal.Models
{
    public class Menu
    {
        public String menuName { get; set; }
        public String url { get; set; }
    }
    public class RaceResult
    {
        public List<Menu> GetMenu()
        {
            try
            {
                List<Menu> lMenus = new List<Menu>();

                Menu iMenu3 = new Menu();
                iMenu3.menuName = "My Profile";
                iMenu3.url = "/Main/MyProfile";
                lMenus.Add(iMenu3);

                Menu iMenu1  = new Menu();
                iMenu1.menuName = "Race Result";
                iMenu1.url = "/Main/RaceResult";
                lMenus.Add(iMenu1);

                Menu iMenu2 = new Menu();
                iMenu2.menuName = "SMS Logs";
                iMenu2.url = "/Main/ViewLogs";
                lMenus.Add(iMenu2);

                //Menu iMenu4 = new Menu();
                //iMenu4.menuName = "Check Balance";
                //iMenu4.url = "/Main/LoanBalance";
                //lMenus.Add(iMenu4);

                //Menu iMenu5 = new Menu();
                //iMenu5.menuName = "Change Password";
                //iMenu5.url = "/Main/ChangePassword";
                //lMenus.Add(iMenu5);

                return lMenus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}