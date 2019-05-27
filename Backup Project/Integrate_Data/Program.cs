using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Integrate_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportInbox();
        }

        public static void ImportInbox()
        {
            try
            {
                DataSet dt = GetFileNotesFromServer();
                if (dt.Tables.Count > 0)
                {
                    foreach (DataRow rows in dt.Tables[0].Rows)
                    {
                        ProcessImport(rows["AccountName"].ToString(), rows["AccountID"].ToString(), rows["ClubID"].ToString(), rows["Action"].ToString(), rows["FileNotesID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataSet GetFileNotesFromServer()
        {
            try
            {
                DataSet ds = new DataSet();
                FileNotes filenotes = new FileNotes();
                ds = filenotes.GetFileNotesForImport();
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private static void ProcessImport(String fileType, String primaryID, string clubID, string action, string filenotesID)
        {
            try
            {
                switch (fileType)
                {
                    case "RaceScheduleCategory":
                        RaceScheduleCategory raceScheduleCategory = new RaceScheduleCategory();
                        raceScheduleCategory.RaceScheduleCategoryImport(primaryID, clubID, action, filenotesID); 
                        break;
                    case "RaceCategoryGroup":
                        RaceCategoryGroup raceCategoryGroup = new RaceCategoryGroup();
                        raceCategoryGroup.RaceCategoryGroupImport(primaryID, clubID, action, filenotesID); 
                        break;
                    case "memberdetails":
                        MemberDetails memberDetails = new MemberDetails();
                        memberDetails.MemberDetailsImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "RaceScheduleDetails": 
                        RaceScheduleDetails raceScheduleDetails = new RaceScheduleDetails();
                        raceScheduleDetails.RaceScheduleDetailsImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "RaceReleasePoint":
                        RaceReleasePoint raceReleasePoint = new RaceReleasePoint();
                        raceReleasePoint.RaceReleasePointImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "RaceSchedule": 
                        RaceSchedule raceSchedule = new RaceSchedule();
                        raceSchedule.RaceScheduleImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "smsregisterednumber":
                        SmsRegisteredNumber smsregisterednumber = new SmsRegisteredNumber();
                        smsregisterednumber.SmsRegisteredNumberImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "BandNumber":
                        BandNumber BandNumber = new BandNumber();
                        BandNumber.BandNumberImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "Entry":
                        Entry Entry = new Entry();
                        Entry.EntryImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "Club":
                        Club Club = new Club();
                        Club.ClubImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "Location": 
                        Location Location = new Location();
                        Location.LocationImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "Racecategory":
                        Racecategory Racecategory = new Racecategory();
                        Racecategory.RacecategoryImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "MemberRingEnrolled":
                        MemberRingEnrolled MemberRingEnrolled = new MemberRingEnrolled();
                        MemberRingEnrolled.MemberRingEnrolledImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "Users":
                        User Users = new User();
                        Users.UserImport(primaryID, clubID, action, filenotesID);
                        break;
                    case "Region": break;
                        Region Region = new Region();
                        Region.RegionImport(primaryID, clubID, action, filenotesID);
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
