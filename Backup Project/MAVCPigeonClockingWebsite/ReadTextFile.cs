using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.IO;


namespace MAVCPigeonClockingWebsite
{
    public class ReadTextFile
    {
        public DataTable ReadFromTextFile(string path, DataTable dt, string filter, string category, string group)
        {
            try
            {
                String line = "";
                using (StreamReader sr = new StreamReader(path))
                {
                    line = sr.ReadToEnd();
                }

                return ParseString(dt, line, filter, category, group);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean UpdateLatest(string source, string destination)
        {
            try
            {
                bool status = false;
                if (File.Exists(source))
                {
                    File.Copy(source, destination, true);
                    status = true;
                }
                return status;
            }
            catch (Exception ex) { throw ex; }
        }

        public void CheckUpdate(string root, string Club)
        {
            try
            {
                string StatusTemplate = root + @"TextFile\RaceResult\" + Club + @"\RaceResultStatus.txt";
                string ResetStatusTemplate = root + @"TextFile\RaceResult\" + Club + @"\RaceResultResetStatus.txt";
                string LatestRaceRelease = root + @"TextFile\RaceResult\" + Club + @"\RaceDateReleaseLatest.txt";
                string LatestRaceResult = root + @"TextFile\RaceResult\" + Club + @"\RaceResultLatest.txt";
                string LatestRaceDetails = root + @"TextFile\RaceResult\" + Club + @"\RaceDetailsLatest.txt";
                string RaceDateRelease = root + @"TextFile\RaceResult\" + Club + @"\RaceDateRelease";
                string RaceResult = root + @"TextFile\RaceResult\" + Club + @"\RaceResult";
                string RaceDetails = root + @"TextFile\RaceResult\" + Club + @"\RaceDetails";

                if (File.Exists(StatusTemplate))
                {
                    String lines = "";
                    string[] line;
                    string[] date;
                    using (StreamReader sr = new StreamReader(StatusTemplate))
                    {
                        lines = sr.ReadToEnd();
                    }
                    lines = lines.Replace("\r\n", "").ToString();
                    line = lines.Split(new string[] { "::" }, System.StringSplitOptions.None);
                    if (line.Length > 0)
                    {
                        if (line[0] == "0")
                        {
                            date = line[1].Split(new string[] { "-" }, System.StringSplitOptions.None);
                            if (UpdateLatest(LatestRaceRelease, RaceDateRelease + ".txt") && UpdateLatest(LatestRaceResult, RaceResult + date[0].PadLeft(2, '0').ToString() + date[1].PadLeft(2, '0').ToString() + date[2].ToString() + ".txt") && UpdateLatest(LatestRaceDetails, RaceDetails + date[0].PadLeft(2, '0').ToString() + date[1].PadLeft(2, '0').ToString() + date[2].ToString() + ".txt"))
                            {
                                UpdateLatest(ResetStatusTemplate, StatusTemplate);
                            }

                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        private DataTable ParseString(DataTable dt, String stringText, string filter, string category, string group)
        {
            try
            {
                string[] contents = stringText.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
                string categoryGroup = "";
                int counter = 1;

                if (filter == null) filter = "";
                if (category == null || category == "All") category = "";
                if (group == null || group == "All") group = "";
                categoryGroup = category + "/" + group;

                foreach (string content in contents)
                {
                    if (content == "")
                    {
                        continue;
                    }

                    if (dt.Columns.Contains("GroupCategory") && (category != "" || group != ""))
                    {
                        if (!(content.ToString().ToUpper().Contains(categoryGroup.ToUpper().ToString())))
                        {
                            continue;
                        }
                    }

                    DataRow dtrow;
                    int index = 0;
                    string[] columns = content.Split(new string[] { "::" }, System.StringSplitOptions.None);
                    dtrow = dt.NewRow();
                    foreach (string column in columns)
                    {

                        if (dt.Columns.IndexOf("Rank") == index)
                        {
                            dtrow[index] = counter;
                            counter += 1;
                            index = index + 1;
                            continue;
                        }

                        dtrow[index] = column;
                        index = index + 1;
                    }
                    if (dt.Columns.Contains("MemberName") && (filter != "" || category != "" || group != ""))
                    {
                        if (dtrow["MemberName"].ToString().ToUpper().Contains(filter.ToString().ToUpper()))
                        {

                            if (dt.Columns.Contains("GroupCategory") && (category != "" || group != ""))
                            {
                                if ((dtrow["GroupCategory"].ToString().ToUpper().Contains(categoryGroup.ToUpper().ToString())))
                                {
                                    dt.Rows.Add(dtrow);
                                }
                            }
                            else
                            {
                                dt.Rows.Add(dtrow);
                            }
                        }
                    }
                    else
                    {
                        dt.Rows.Add(dtrow);
                    }
                }




                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}