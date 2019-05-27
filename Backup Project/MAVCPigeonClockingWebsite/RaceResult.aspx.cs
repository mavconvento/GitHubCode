using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace MAVCPigeonClockingWebsite
{
    public partial class RaceResult : System.Web.UI.Page
    {
        public string Club { get; set; }
        public string ClubFullName { get; set; }
        public string Filter { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public string DateRelease { get; set; }

        ReadTextFile readTextFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Club = (string)Request.QueryString["CLUB"];
                ClubFullName = (string)Request.QueryString["CLUBFULLNAME"];
                Filter = (string)Request.QueryString["FILTER"];
                Category = (string)Request.QueryString["CATEGORY"];
                Group = (string)Request.QueryString["GROUP"];
                DateRelease = (string)Request.QueryString["DATERELEASE"];

                if (Filter == null)
                {
                    Filter = "";
                }

                if (Category == null)
                {
                    Category = "";
                }

                if (Group == null)
                {
                    Group = "";
                }

                if (DateRelease == null)
                {
                    DateRelease = "";
                }

                if (Club == null)
                {
                    Club = "";
                }

                if (ClubFullName == null)
                {
                    ClubFullName = "";
                }

                string ClubName = Club;

                raceDetails.Parameters(ClubName, ClubFullName, DateRelease, Category, Group, Filter);
                raceResultV1.parameters(ClubName, Filter,DateRelease);
                raceResultV3.parameters(ClubName, Filter, Category, Group, DateRelease);

                string root = Server.MapPath("~");
                readTextFile = new ReadTextFile();
                readTextFile.CheckUpdate(root, ClubName);


                //if (Club == "") Club = (string)Request.QueryString["CLUB"];
                //if (Club == "") ClubFullName = (string)Request.QueryString["CLUBFULLNAME"];
            }
            catch
            { }
        }

    }
}