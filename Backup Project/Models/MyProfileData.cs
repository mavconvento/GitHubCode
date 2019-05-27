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
    public class MemberDetails
    {
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String ExtensionName { get; set; }
        public String LoftName { get; set; }
        public String MemberIDNo { get; set; }
        public String Coordinates { get; set; }
    }

    public class MemberDistanceCollection
    {
        public String LocationName { get; set; }
        public String Coordinates { get; set; }
        public String Distance { get; set; }
        public String Region { get; set; }
    }

    public class MyProfileData
    {
        public MemberDetails MemberInfo { get; set; }
        public List<MemberDistanceCollection> MemberDistance { get; set; }

        public MyProfileData GetMemberDistance(string ClubID, String MemberID)
        {
            DAL.MyProfile myProfile = new DAL.MyProfile();
            DataSet dsResult = new DataSet();
            dsResult = myProfile.GetMemberDistance(ClubID, MemberID);
            MyProfileData profile = new MyProfileData();

            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    MemberDetails memInfo = new MemberDetails();
                    memInfo.LastName = LWT.Common.LWTSafeTypes.SafeString(dsResult.Tables[0].Rows[0]["LastName"]);
                    memInfo.FirstName = LWT.Common.LWTSafeTypes.SafeString(dsResult.Tables[0].Rows[0]["FirstName"]);
                    memInfo.MiddleName = LWT.Common.LWTSafeTypes.SafeString(dsResult.Tables[0].Rows[0]["MiddleName"]);
                    memInfo.LoftName = LWT.Common.LWTSafeTypes.SafeString(dsResult.Tables[0].Rows[0]["LoftName"]);
                    memInfo.Coordinates = LWT.Common.LWTSafeTypes.SafeString(dsResult.Tables[0].Rows[0]["Coordinates"]);
                    memInfo.MemberIDNo = LWT.Common.LWTSafeTypes.SafeString(dsResult.Tables[0].Rows[0]["MemberIDNo"]);
                    profile.MemberInfo = memInfo;
                }

                List<MemberDistanceCollection> collection = new List<MemberDistanceCollection>();
                if (dsResult.Tables[1].Rows.Count > 0)
                {
                    
                    foreach (DataRow item in dsResult.Tables[1].Rows)
                    {
                        MemberDistanceCollection distanceCollection = new MemberDistanceCollection();
                        distanceCollection.LocationName = LWT.Common.LWTSafeTypes.SafeString(item["Release Point"]);
                        distanceCollection.Coordinates = LWT.Common.LWTSafeTypes.SafeString(item["Coordinates"]);
                        distanceCollection.Distance = LWT.Common.LWTSafeTypes.SafeString(item["Distance"]);
                        distanceCollection.Region = "North";
                        collection.Add(distanceCollection);
                    }
                    profile.MemberDistance = collection;
                }

                if (dsResult.Tables[2].Rows.Count > 0)
                {
                    //List<MemberDistanceCollection> collection = new List<MemberDistanceCollection>();
                    foreach (DataRow item in dsResult.Tables[2].Rows)
                    {
                        MemberDistanceCollection distanceCollection = new MemberDistanceCollection();
                        distanceCollection.LocationName = LWT.Common.LWTSafeTypes.SafeString(item["Release Point"]);
                        distanceCollection.Coordinates = LWT.Common.LWTSafeTypes.SafeString(item["Coordinates"]);
                        distanceCollection.Distance = LWT.Common.LWTSafeTypes.SafeString(item["Distance"]);
                        distanceCollection.Region = "South";
                        collection.Add(distanceCollection);
                    }
                   
                }
                profile.MemberDistance = collection;
            }

            return profile;
        }

        public DataTable GetRegisterMobileList(String Mobilenumber)
        {
            DAL.MyProfile myProfile = new DAL.MyProfile();
            DataSet dsResult = myProfile.GetRegisteredMobileList(Mobilenumber);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }

        public DataTable LoadMavcCard(string ClubID, String Mobilenumber, string PinNumber)
        {
            DAL.MyProfile myProfile = new DAL.MyProfile();
            string keyword = "LOAD " + PinNumber;
            DataSet dsResult = myProfile.LoadMavcCard(ClubID, Mobilenumber, keyword);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }

        public DataTable Pasaload(string MobilenumberFrom, String MobilenumberTo, string Amount)
        {
            DAL.MyProfile myProfile = new DAL.MyProfile();
            DataSet dsResult = myProfile.Pasaload(MobilenumberFrom, MobilenumberTo, Amount);
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }

        public DataTable UnregMobileNumber(string ClubID, String Mobilenumber)
        {
            DAL.MyProfile myProfile = new DAL.MyProfile();
            DataSet dsResult = myProfile.UnregMobileNumber(ClubID, Mobilenumber, "UNREG");
            DataTable dtResult = new DataTable();

            if (dsResult.Tables.Count > 0)
            {
                dtResult = dsResult.Tables[0];
            }

            return dtResult;
        }
    }
}