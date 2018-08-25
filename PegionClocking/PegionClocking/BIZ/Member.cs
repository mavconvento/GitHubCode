using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PegionClocking.BIZ
{
    class Member 
    {

        #region Variables
        DAL.Member member;
        #endregion

        #region Properties
        public String ID { get; set; }
        public Int64 ClubID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 MemberID { get; set; }
        public Int64 BandID { get; set; }
        public string RingNumber { get; set; }
        public string RaceScheduleName { get; set; }
        public string RaceScheduleCategoryName { get; set; }
        public string MemberIDNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtensionName { get; set; }
        public string LoftName { get; set; }
        public string Address { get; set; }
        public DateTime DateofBirth { get; set; }
        public Int64 DistanceLatDegree { get; set; }
        public Int64 DistanceLatMinutes { get; set; }
        public Double DistanceLatSeconds { get; set; }
        public String DistanceLatSign { get; set; }
        public Int64 DistanceLongDegree { get; set; }
        public Int64 DistanceLongMinutes { get; set; }
        public Double DistanceLongSeconds { get; set; }
        public String DistanceLongSign { get; set; }
        public DateTime DateofMembership { get; set; }
        public DateTime LastRenewalDate { get; set; }
        public DateTime DateofExpiration { get; set; }
        public Boolean DeactivateMember { get; set; }

        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Int64 LocationID { get; set; }

        public string Type { get; set; }
        public String ReaderID { get; set; }
        public String ClubName { get; set; }
        public Boolean Overwrite { get; set; }
        #endregion

        #region Private Methods
        private void PopulateDataLayer()
        {
            try
            {
                member.ID = ID;
                member.ClubID = ClubID;
                member.UserID = UserID;
                member.MemberID = MemberID;
                member.BandID = BandID;
                member.RingNumber = RingNumber;
                member.RaceScheduleName = RaceScheduleName;
                member.RaceScheduleCategoryName = RaceScheduleCategoryName;
                member.MemberIDNo = MemberIDNo;
                member.LastName = LastName;
                member.FirstName = FirstName;
                member.MiddleName = MiddleName;
                member.ExtensionName = ExtensionName;
                member.LoftName = LoftName;
                member.Address = Address;
                member.DateofBirth = DateofBirth.Date;
                member.DistanceLatDegree = DistanceLatDegree;
                member.DistanceLatMinutes = DistanceLatMinutes;
                member.DistanceLatSeconds = DistanceLatSeconds;
                member.DistanceLatSign = DistanceLatSign;
                member.DistanceLongDegree = DistanceLongDegree;
                member.DistanceLongMinutes = DistanceLongMinutes;
                member.DistanceLongSeconds = DistanceLongSeconds;
                member.DistanceLongSign = DistanceLongSign;
                member.DateofMembership = DateofMembership.Date;
                member.LastRenewalDate = LastRenewalDate.Date;
                member.DateofExpiration = DateofExpiration.Date;
                member.Name = Name;
                member.MobileNumber = MobileNumber;
                member.IsUploaded = false;
                member.LocationID = LocationID;
                member.DeactivateMember = DeactivateMember;
                member.Type = Type;
                member.ClubName = ClubName;
                member.ReaderID = ReaderID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Public Methods
        public Boolean Save()
        {
            try
            {
                Boolean status = false;
                member = new DAL.Member();
                PopulateDataLayer();
                member.Save();
                MessageBox.Show("Member Record Save!", "Record Save");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean MemberRingSave()
        {
            try
            {
                Boolean status = false;
                member = new DAL.Member();
                PopulateDataLayer();
                member.MemberRingSave();
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean ReaderRegistrationSave()
        {
            try
            {
                Boolean status = false;
                member = new DAL.Member();
                PopulateDataLayer();
                member.ReaderRegistrationSave();
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean RFIDRegisterSave()
        {
            try
            {
                Boolean status = false;
                member = new DAL.Member();
                PopulateDataLayer();
                member.RegisterRFIDSave();
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MemberDetailsSelectAll(DataGridView memberList)
        {
            try
            {
                member = new DAL.Member();
                PopulateDataLayer();
                memberList.DataSource = member.MemberDetailsSelectAll().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ExportEClockMasterlist()
        {
            try
            {
                member = new DAL.Member();
                PopulateDataLayer();
                DataTable dt = member.MemberDetailsSelectAll().Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet MemberDetailsSearchByKey()
        {
            try
            {
                member = new DAL.Member();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = member.MemberDetailsSearchByKey();
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public DataTable MemberRingSearchByKey()
        {
            try
            {
                member = new DAL.Member();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = member.MemberRingSearchByKey().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean MemberDetailsDelete()
        {
            try
            {
                Boolean status = false;
                member = new DAL.Member();
                PopulateDataLayer();
                member.MemberDetailsDelete();
                MessageBox.Show("Record Successfully Deleted!", "Delete Record");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public Boolean MemberRingDelete()
        {
            try
            {
                Boolean status = false;
                member = new DAL.Member();
                PopulateDataLayer();
                member.MemberRingDelete();
                MessageBox.Show("Record Successfully Deleted!", "Delete Record");
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet GetMemberDistance()
        {
            try
            {
                member = new DAL.Member();
                DataSet dataResult = new DataSet();
                PopulateDataLayer();
                dataResult = member.GetMemberDistance();
                return dataResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public DataTable GetMembetDistancePerReleasePoint()
        {
            try
            {
                 member = new DAL.Member();
                DataTable dataResult = new DataTable();
                PopulateDataLayer();
                dataResult = member.GetMembetDistancePerReleasePoint().Tables[0];
                return dataResult;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion
    }
}
