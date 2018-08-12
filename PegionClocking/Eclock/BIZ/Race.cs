using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Eclock.BIZ
{
    public class Race
    {

        #region Variables
        DAL.Race DalRace;
        #endregion

        #region Properties
        public Int64 ClubID { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 RaceReleasePointID { get; set; }
        public String SerialRFIDNo { get; set; }
        public DateTime ArrivalTime { get; set; }
        public String MobileNumber { get; set; }
        public String SMSActivated { get; set; }
        #endregion

        #region Public Methods
        public DataSet GetArrivalTime()
        {
            try
            {
                DalRace = new DAL.Race();
                return DalRace.GetArrivalTime(this);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public Boolean SubmitRaceResult()
        {
            try
            {
                DalRace = new DAL.Race();
                return DalRace.SubmitRaceResult(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
    }
}
