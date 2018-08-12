using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PegionClocking.DAL
{
    class Calculator
    {

        DAL.DatabaseConnection dbconn;

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
        public String  RaceScheduleDetailsID { get; set; }
        public String MemberID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public String ClubID { get; set; }
        #endregion

        //public DataSet Calculate()
        //{
        //    DataSet dataResult = new DataSet();
        //    dbconn = new DatabaseConnection();
        //    dbconn.DatabaseConn("Calculator");

        //    if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
        //    dbconn.sqlConn.Open();
        //    dbconn.sqlComm.Parameters.Clear();
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegree", DistanceLatDegree);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutes", DistanceLatMinutes);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSecond", DistanceLatSecond);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSign", DistanceLatSign);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegree", DistanceLongDegree);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutes", DistanceLongMinutes);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSecond", DistanceLongSecond);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSign", DistanceLongSign);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatDegreeDestination", DistanceLatDegreeDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatMinutesDestination", DistanceLatMinutesDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSecondDestination", DistanceLatSecondDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLatSignDestination", DistanceLatSignDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongDegreeDestination", DistanceLongDegreeDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongMinutesDestination", DistanceLongMinutesDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSecondDestination", DistanceLongSecondDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@DistanceLongSignDestination", DistanceLongSignDestination);
        //    dbconn.sqlComm.Parameters.AddWithValue("@Distance", Distance);
        //    dbconn.sqlComm.Parameters.AddWithValue("@ReleaseTime", ReleaseTime);
        //    dbconn.sqlComm.Parameters.AddWithValue("@ArrivalTime", ArrivalTime);
        //    dbconn.sqlComm.Parameters.AddWithValue("@Flight", Flight);
        //    dbconn.sqlComm.Parameters.AddWithValue("@Speed", Speed);
        //    dbconn.sqlComm.Parameters.AddWithValue("@CutOff", CutOff);
        //    dbconn.sqlComm.Parameters.AddWithValue("@Type", Type);
            
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = dbconn.sqlComm;
        //    da.Fill(dataResult);
        //    dbconn.sqlConn.Close();
        //    return dataResult;
        //}

        public DataSet Calculate()
        {
            DataSet dataResult = new DataSet();
            dbconn = new DatabaseConnection();
            dbconn.DatabaseConn("Calculator");

            if (dbconn.sqlConn.State == ConnectionState.Open) dbconn.sqlConn.Close();
            dbconn.sqlConn.Open();
            dbconn.sqlComm.Parameters.Clear();
            dbconn.sqlComm.Parameters.AddWithValue("@ClubID", ClubID);
            dbconn.sqlComm.Parameters.AddWithValue("@ReleasepointID", ReleasePointID);
            dbconn.sqlComm.Parameters.AddWithValue("@ScheduleDetailsID", RaceScheduleDetailsID);
            dbconn.sqlComm.Parameters.AddWithValue("@MemberID", MemberID);
            dbconn.sqlComm.Parameters.AddWithValue("@ArrivalDate", ArrivalDate);
            dbconn.sqlComm.Parameters.AddWithValue("@ArrivalTime", ArrivalTime);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = dbconn.sqlComm;
            da.Fill(dataResult);
            dbconn.sqlConn.Close();
            return dataResult;
        }
    }
}
