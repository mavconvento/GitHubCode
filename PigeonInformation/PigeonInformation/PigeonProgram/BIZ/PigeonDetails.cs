using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PigeonProgram.BIZ
{
    public class PigeonDetails
    {
        public String RFIDTag { get; set; }
        public Int64 PigeonID { get; set; }
        public String PigeonName { get; set; }
        public String BandNumber { get; set; }
        public String Gender { get; set; }
        public String EyeColor { get; set; }
        public String Color { get; set; }
        public String Line { get; set; }
        public String Owner { get; set; }
        public String Achievement { get; set; }
        public byte[] Picture { get; set; }
        public Int64 UserID { get; set; }
        public Boolean IsUnknown { get; set; }
        public DateTime HatchDate { get; set; }
        public String Year { get; set; }
        public String Season { get; set; }
        public String Category { get; set; }
        public String ParentCock { get; set; }
        public String ParentHen { get; set; }
        public Int64 RaceResultID { get; set; }
        public Int64 TreatmentID { get; set; }
        public String Remarks { get; set; }
        public String TypeofBreeding { get; set; }
        public String PigeonType { get; set; }

        public String ReleasePoint { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String WeatherCondition { get; set; }
        public String BirdEntry { get; set; }
        public String BirdClock { get; set; }
        public String Flight { get; set; }
        public String Rank { get; set; }
        public String Distance { get; set; }
        public String Speed { get; set; }
        public String BackColor { get; set; }

        public String Treatment { get; set; }
        public DateTime TreatmentDate { get; set; }
        public String Illness { get; set; }

        public String FilterGender { get; set; }
        public String FilterYear { get; set; }
        public String FilterBreed { get; set; }
        public String FilterPigeonName { get; set; }
        public bool IsFourthGen { get; set; }

        public DataSet PigeonDetailsSave()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.PigeonDetailsSave();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetPigeonByGender()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                return pigeonDetails.GetPigeonByGender(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetPigeonList()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                return pigeonDetails.GetPigeonList(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetPigeonDetails()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.PigeonDetailsGetByKey();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DeletePigeonDetails()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.PigeonDetailsDelete();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet PigeonPedigree()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.GetPigeonPedigree();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetPigeonOffspring()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.GetPigeonOffspring();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet GetPedigreeSetup()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.GetPedigreeSetup();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet RaceResultSave()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.RaceResultSave();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultGetByKey()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.RaceResultGetByKey();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultGetAll()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.RaceResultGetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RaceResultDelete()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.RaceResultDelete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet TreatmentSave()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.TreatmentSave();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet TreatmentGetByKey()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.TreatmentGetByKey();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet TreatmentGetAll()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.TreatmentGetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet TreatmentDelete()
        {
            try
            {
                DAL.PigeonDetails pigeonDetails = new DAL.PigeonDetails();
                pigeonDetails.PigeonDetail = this;
                return pigeonDetails.TreatmentDelete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
