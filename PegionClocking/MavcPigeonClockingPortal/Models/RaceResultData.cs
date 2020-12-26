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
    public class ClubData
    {
        public String clubID { get; set; }
        public String clubName { get; set; }
        public String clubAbbreviation { get; set; }
        public String accessNumber { get; set; }
        public String daysRemaining { get; set; }
        public String dbName { get; set; }
    }

    public class BirdCategoryList
    {
        public String birdCategoryName { get; set; }
        public String birdCategoryID { get; set; }
    }

    public class GroupCategoryList
    {
        public String groupCategoryName { get; set; }
        public String groupCategoryID { get; set; }
    }

    public class RaceResultSearchData
    {
        public String ClubID { get; set; }
        public String ClubName { get; set; }
        public DateTime RaceReleaseDate { get; set; }
        public String SearchName { get; set; }
        public String BirdCategory { get; set; }
        public String GroupCategory { get; set; }
        public String Version { get; set; }
    }

    public class RaceResultDetailsData
    {
        public String locationName { get; set; }
        public String coordinates { get; set; }
        public String latitude { get; set; }
        public String longtitude { get; set; }
        public String lap { get; set; }
        public String totalBird { get; set; }
        public String sMSCount { get; set; }
        public String birdEntryCount { get; set; }
        public String releaseTime { get; set; }
        public String minSpeed { get; set; }
        public String stopTime { get; set; }
        public String stopFrom { get; set; }
        public String stopTo { get; set; }
        public String description { get; set; }

    }

    public class RaceResultData
    {
        public String CategoryGroup { get; set; }
        public String Rank { get; set; }
        public String MemberName { get; set; }
        public String Distance { get; set; }
        public String latitude { get; set; }
        public String longtitude { get; set; }
        public String RingNumber { get; set; }
        public String StickerCode { get; set; }
        public String Arrival { get; set; }
        public String Flight { get; set; }
        public String Speed { get; set; }
        public String Remarks { get; set; }

        public List<ClubData> GetClubList(String UserID)
        {
            DAL.RaceResult raceResult = new DAL.RaceResult();
            DataSet dtResult = raceResult.GetClubList(UserID);
            List<ClubData> lClubData = new List<ClubData>();

            if (dtResult.Tables.Count > 0)
            {
                foreach (DataRow item in dtResult.Tables[0].Rows)
                {
                    ClubData iClubData = new ClubData();
                    iClubData.clubID = LWT.Common.LWTSafeTypes.SafeString(item["Club Abbreviation"]);
                    iClubData.clubName = LWT.Common.LWTSafeTypes.SafeString(item["Club Name"]);
                    iClubData.clubAbbreviation = LWT.Common.LWTSafeTypes.SafeString(item["Club Abbreviation"]);
                    iClubData.accessNumber = LWT.Common.LWTSafeTypes.SafeString(item["AccessNumber"]);
                    iClubData.daysRemaining = LWT.Common.LWTSafeTypes.SafeString(item["DaysRemaining"]);
                    lClubData.Add(iClubData);
                }
            }

            return lClubData;
        }

        public DataTable GetBirdCategory(String ClubID)
        {
            if (ClubID == null) ClubID = "";
            DAL.RaceResult raceResult = new DAL.RaceResult();
            DataSet dsResult = raceResult.GetBirdCategory(ClubID);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }

        public DataTable GetGroupCategory(String ClubID)
        {
            if (ClubID == null) ClubID = "";
            DAL.RaceResult raceResult = new DAL.RaceResult();
            DataSet dsResult = raceResult.GetGroupCategory(ClubID);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }

        public RaceResultDetailsData GetRaceDetails(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName, String Sender)
        {
            DAL.RaceResult raceResult = new DAL.RaceResult();
            if (string.IsNullOrEmpty(BirdCategory)) BirdCategory = "All";
            if (string.IsNullOrEmpty(RaceCategory)) RaceCategory = "All";
            DataSet dsResult = raceResult.GetRaceDetails(ClubID, BirdCategory, RaceCategory, ReleaseDate, SearchName, Sender);
            DataTable dtResult = new DataTable();
            RaceResultDetailsData details = new RaceResultDetailsData();
            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
                if (dtResult.Rows.Count > 0)
                {
                    details.locationName = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["LocationName"]);
                    details.stopTime = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["StopTime"]);
                    details.stopFrom = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["StopFrom"]);
                    details.stopTo = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["StopTo"]);
                    details.coordinates = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["Coordinates"]);
                    details.latitude =  LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["Latitude"]);
                    details.longtitude = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["Longtitude"]);
                    details.totalBird = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["TotalBird"]);
                    details.lap = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["Lap"]);
                    details.sMSCount = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["SMSCount"]);
                    details.description = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["Description"]);
                    details.releaseTime = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["ReleaseTime"]);
                    details.minSpeed = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["MinSpeed"]);
                    details.birdEntryCount = LWT.Common.LWTSafeTypes.SafeString(dtResult.Rows[0]["BirdEntryCount"]);
                }
            }
            return details;
        }

        public DataTable GetRaceResult(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName)
        {
            DAL.RaceResult raceResult = new DAL.RaceResult();
            if (string.IsNullOrEmpty(BirdCategory)) BirdCategory = "All";
            if (string.IsNullOrEmpty(RaceCategory)) RaceCategory = "All";
            DataSet dsResult = raceResult.GetRaceResult(ClubID, BirdCategory, RaceCategory, ReleaseDate, SearchName);
            DataTable dtResult = new DataTable();
            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }
            return dtResult;
        }

        public DataTable GetRaceEntry(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName, String Sender,String Source = "")
        {
            DAL.RaceResult raceResult = new DAL.RaceResult();
            if (string.IsNullOrEmpty(BirdCategory)) BirdCategory = "All";
            if (string.IsNullOrEmpty(RaceCategory)) RaceCategory = "All";
            DataSet dsResult = raceResult.GetRaceEntry(ClubID, BirdCategory, RaceCategory, ReleaseDate, SearchName, Sender, Source);
            DataTable dtResult = new DataTable();
            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[1];
            }
            return dtResult;
        }

        public DataTable SendSticker(string ClubID, String Mobilenumber, string StickerNumber)
        {
            DAL.RaceResult raceResult = new DAL.RaceResult();
            DataSet dsResult = raceResult.SendSticker(ClubID, Mobilenumber, StickerNumber);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }

        public DataTable Forecast(string ClubID, String Mobilenumber)
        {
            DAL.RaceResult raceResult = new DAL.RaceResult();
            DataSet dsResult = raceResult.Forecast(ClubID, Mobilenumber, "Forecast");
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }
    }
}