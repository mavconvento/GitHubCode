using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

using LWT.Common;
using LWT.Common.Biz;
using WebRaceResult.DAL;
//using LWT.LoanTrust.DAL;

namespace WebRaceResult.BIZ
{
    public class RaceResult
    {
        public DataSet GetBirdCategory(String ClubID)
        {
            try
            {
                DataSet dsResult = new DataSet();
                DAL.RaceResult raceResult = new DAL.RaceResult();
                dsResult = raceResult.GetBirdCategory(ClubID);

                return dsResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet GetGroupCategory(String ClubID)
        {
            try
            {
                DataSet dsResult = new DataSet();
                DAL.RaceResult raceResult = new DAL.RaceResult();
                dsResult = raceResult.GetGroupCategory(ClubID);
                return dsResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataSet GetRaceResultDetails(String ClubID, String BirdCategory, String RaceCategory, DateTime ReleaseDate, String SearchName, String Sender)
        {
            try
            {
                DataSet dsResult = new DataSet();
                DAL.RaceResult raceResult = new DAL.RaceResult();
                dsResult = raceResult.GetRaceDetails(ClubID,BirdCategory,RaceCategory,ReleaseDate,SearchName,Sender);
                return dsResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}