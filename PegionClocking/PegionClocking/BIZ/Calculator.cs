using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PegionClocking.BIZ
{
    class Calculator
    {
        #region Constant
        #endregion

        #region Variable
        DAL.Calculator calculator;
        #endregion

        #region Properties
        public string DistanceLatDegree { get; set; }
        public string DistanceLatMinutes { get; set; }
        public string DistanceLatSecond { get; set; }
        public string DistanceLatSign { get; set; }
        public string DistanceLongDegree { get; set; }
        public string DistanceLongMinutes { get; set; }
        public string DistanceLongSecond { get; set; }
        public string DistanceLongSign { get; set; }
        public string DistanceLatDegreeDestination { get; set; }
        public string DistanceLatMinutesDestination { get; set; }
        public string DistanceLatSecondDestination { get; set; }
        public string DistanceLatSignDestination { get; set; }
        public string DistanceLongDegreeDestination { get; set; }
        public string DistanceLongMinutesDestination { get; set; }
        public string DistanceLongSecondDestination { get; set; }
        public string DistanceLongSignDestination { get; set; }
        public string Distance { get; set; }
        public string ReleaseTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Flight { get; set; }
        public string Speed { get; set; }
        public string CutOff { get; set; }
        public string Type { get; set; }

        public String ReleasePointID { get; set; }
        public String RaceScheduleDetailsID { get; set; }
        public String MemberID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public String ClubID { get; set; }
        #endregion

        #region Public Methods
        public DataSet Calculate()
        {
            try
            {
                DataSet dtResult = new DataSet();
                calculator = new DAL.Calculator();
                PopulateDataLayer();
                dtResult = calculator.Calculate();
                return dtResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        
        }
        #endregion

        #region Private Methods
        private void PopulateDataLayer()
        {
            try
            {
                calculator.DistanceLatDegree = DistanceLatDegree;
                calculator.DistanceLatMinutes = DistanceLatMinutes;
                calculator.DistanceLatSecond = DistanceLatSecond;
                calculator.DistanceLatSign = DistanceLatSign;
                calculator.DistanceLongDegree = DistanceLongDegree;
                calculator.DistanceLongMinutes = DistanceLongMinutes;
                calculator.DistanceLongSecond = DistanceLongSecond;
                calculator.DistanceLongSign = DistanceLongSign;
                calculator.DistanceLatDegreeDestination = DistanceLatDegreeDestination;
                calculator.DistanceLatMinutesDestination = DistanceLatMinutesDestination;
                calculator.DistanceLatSecondDestination = DistanceLatSecondDestination;
                calculator.DistanceLatSignDestination = DistanceLatSignDestination;
                calculator.DistanceLongDegreeDestination = DistanceLongDegreeDestination;
                calculator.DistanceLongMinutesDestination = DistanceLongMinutesDestination;
                calculator.DistanceLongSecondDestination = DistanceLongSecondDestination;
                calculator.DistanceLongSignDestination = DistanceLongSignDestination;
                calculator.Distance = Distance;
                calculator.ReleaseTime = ReleaseTime;
                calculator.ArrivalTime = ArrivalTime;
                calculator.Flight = Flight;
                calculator.Speed = Speed;
                calculator.CutOff = CutOff;
                calculator.Type = Type;

                calculator.ReleasePointID = ReleasePointID;
                calculator.RaceScheduleDetailsID = RaceScheduleDetailsID;
                calculator.ClubID = ClubID;
                calculator.MemberID = MemberID;
                calculator.ArrivalDate = ArrivalDate;
                calculator.ArrivalTime = ArrivalTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
