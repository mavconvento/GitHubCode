using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class Club
    {
        #region Constant
        #endregion

        #region Variable
        DAL.Club club;
        #endregion

        #region Properties
        public Int64 UserID { get; set; }
        public Int64 ClubID { get; set; }
        public String ClubName { get; set; }
        public String ClubAbbreviation { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSecond { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSecond { get; set; }
        public String DistanceLongSign { get; set; }
        public String Version { get; set; }
        public String DateTimeSource { get; set; }
        public Boolean IsBackUp { get; set; }
        public Boolean IsMAVCStickerUsed { get; set; }
        public DateTime LastSubcription { get; set; }
        public DateTime SubcriptionDate { get; set; }
        public String Server { get; set; }
        #endregion

        #region Public Methods
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                club = new DAL.Club();
                PopulateDataLayer();
                club.Save();
                MessageBox.Show("Club Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ClubSelectAll(DataGridView ClubList)
        {
            try
            {
                club = new DAL.Club();
                PopulateDataLayer();
                ClubList.DataSource = club.ClubSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ClubSelectAll()
        {
            try
            {
                DataTable dtResult;
                club = new DAL.Club();
                PopulateDataLayer();
                dtResult = club.ClubSelectAll().Tables[0];
                return dtResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ClubSearchByKey()
        {
            try
            {
                club = new DAL.Club();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = club.ClubSearchByKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean ClubDelete()
        {
            try
            {
                Boolean status = false;
                club = new DAL.Club();
                PopulateDataLayer();
                club.ClubDelete();
                MessageBox.Show("Record Successfully Deleted!", "Delete Record");
                status = true;
                return status;
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
                club.ClubID = ClubID;
                club.UserID = UserID;
                club.ClubName = ClubName;
                club.ClubAbbreviation = ClubAbbreviation;
                club.DistanceLatDegree = DistanceLatDegree;
                club.DistanceLatMinutes = DistanceLatMinutes;
                club.DistanceLatSecond = DistanceLatSecond;
                club.DistanceLatSign = DistanceLatSign;
                club.DistanceLongDegree = DistanceLongDegree;
                club.DistanceLongMinutes = DistanceLongMinutes;
                club.DistanceLongSecond = DistanceLongSecond;
                club.DistanceLongSign = DistanceLongSign;
                club.Version = Version;
                club.DateTimeSource = DateTimeSource;
                club.IsBackUp = IsBackUp;
                club.IsMAVCStickerUsed = IsMAVCStickerUsed;
                club.LastSubcription = LastSubcription;
                club.SubcriptionDate = SubcriptionDate;
                club.Server = Server;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
