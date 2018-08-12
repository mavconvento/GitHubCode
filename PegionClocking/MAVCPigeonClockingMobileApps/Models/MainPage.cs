using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using MAVCPigeonClockingMobileApps;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace MAVCPigeonClockingMobileApps.Models
{
    public class MainPage
    {

        public String ReleasePoint { get; set; }
        public String Coordinates { get; set; }
        public String TimeRelease { get; set; }
        public String MinSpeed { get; set; }
        public String TotalBirdEntry { get; set; }
        public String TotalBirdClock { get; set; }
        public String IsStop { get; set; }
        public String StopFromDate { get; set; }
        public String StopFromTime { get; set; }
        public String StopToDate { get; set; }
        public String StopToTime { get; set; }
        public String Lap { get; set; }
        public String LapNo { get; set; }

        public String ActionMessage { get; set; }
        public String ClubID { get; set; }
        public String ClubName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String StickerNumber { get; set; }
        public String PinNumber { get; set; }
        public String MobileNumber { get; set; }
        public Boolean IsValid { get; set; }
        public DataTable CommonResult { get; set; }
        public String ResultMessage { get; set; }

        public MainPage()
        {
            this.ActionMessage = "";
        }

        public MainPage GetRaceDetails()
        {
            try
            {
                DAL.MainPageDAL mainPage = new DAL.MainPageDAL();

                DataSet dsResult = mainPage.GetRaceDetails(ClubID, ReleaseDate);
                this.ReleasePoint = ""; //initialize value for emptry string
                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        this.ReleasePoint = dsResult.Tables[0].Rows[0]["LocationName"].ToString();
                        this.Coordinates = dsResult.Tables[0].Rows[0]["Coordinates"].ToString();
                        this.TimeRelease = dsResult.Tables[0].Rows[0]["ReleaseTime"].ToString();
                        //this.TotalBirdClock = dsResult.Tables[0].Rows[0]["TotalBirdClock"].ToString();
                        this.TotalBirdEntry = dsResult.Tables[0].Rows[0]["TotalEntry"].ToString();
                        this.Coordinates = dsResult.Tables[0].Rows[0]["Coordinates"].ToString();
                        this.MinSpeed = dsResult.Tables[0].Rows[0]["MinSpeed"].ToString();
                        this.IsStop = dsResult.Tables[0].Rows[0]["IsStop"].ToString();
                        this.StopFromDate = dsResult.Tables[0].Rows[0]["StopFromDate"].ToString();
                        this.StopFromTime = dsResult.Tables[0].Rows[0]["StopFromTime"].ToString();
                        this.StopToDate = dsResult.Tables[0].Rows[0]["StopToDate"].ToString();
                        this.StopToTime = dsResult.Tables[0].Rows[0]["StopToTime"].ToString();
                        this.Lap = dsResult.Tables[0].Rows[0]["Lap"].ToString();
                        this.LapNo = dsResult.Tables[0].Rows[0]["LapNo"].ToString();
                    }
                }

                return this;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public MainPage SendStickerNumber()
        {
            try
            {
                DAL.MainPageDAL mainPage = new DAL.MainPageDAL();

                DataSet dsResult = mainPage.SendStickerNumber(StickerNumber, ClubName, MobileNumber);
                this.ActionMessage = "Invalid Sticker";
                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        this.ResultMessage = ((string)dsResult.Tables[0].Rows[0]["Result"]);
                        this.ActionMessage = ((string)dsResult.Tables[0].Rows[0]["Result"]);
                        this.IsValid = LWT.Common.LWTSafeTypes.SafeBool(dsResult.Tables[0].Rows[0]["IsValid"]);
                        if (this.IsValid) this.ActionMessage = "Success";   //ignore the value of ActionMessage from DB
                    }
                }

                return this;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MainPage SendPinNumber()
        {
            try
            {
                //DAL.MainPageDAL mainPage = new DAL.MainPageDAL();

                //DataSet dsResult = mainPage.SendPinNumber(MobileNumber,PinNumber);
                //this.ActionMessage = "Invalid PinNumber";
                //if (dsResult.Tables.Count > 0)
                //{
                //    if (dsResult.Tables[0].Rows.Count > 0)
                //    {
                //        this.ResultMessage = ((string)dsResult.Tables[0].Rows[0]["Result"]);
                //        this.ActionMessage = ((string)dsResult.Tables[0].Rows[0]["Result"]);
                //        this.IsValid = LWT.Common.LWTSafeTypes.SafeBool(dsResult.Tables[0].Rows[0]["IsValid"]);
                //        if (this.IsValid) this.ActionMessage = "Success";   //ignore the value of ActionMessage from DB
                //    }
                //}

                return this;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MainPage SendForecast()
        {
            try
            {
                DAL.MainPageDAL mainPage = new DAL.MainPageDAL();
                DataSet dsResult = mainPage.SendForecast(StickerNumber, ClubName, MobileNumber);
                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        this.ResultMessage = ((string)dsResult.Tables[0].Rows[0]["Result"]).Replace("<br/>", Environment.NewLine);
                        this.IsValid = LWT.Common.LWTSafeTypes.SafeBool(dsResult.Tables[0].Rows[0]["IsValid"]);
                        if (this.IsValid) this.ActionMessage = "Success";   //ignore the value of ActionMessage from DB
                    }
                }

                return this;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<TestResult> GetForecastTable()
        {
            try
            {
                return GetTestResult();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private List<TestResult> GetTestResult()
        {
            try
            {
                DAL.MainPageDAL mainPage = new DAL.MainPageDAL();
                DataSet dsResult = mainPage.testresult();
               
                var items = new List<TestResult>();
                foreach (DataRow dataRow in dsResult.Tables[0].Rows)
                {
                    items.Add((new TestResult()).GetTestReult(dataRow));
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}