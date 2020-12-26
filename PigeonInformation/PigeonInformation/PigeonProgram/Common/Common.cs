using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace PigeonProgram.Common
{
    public static class Common
    {
        private const string cryptoKey = "cryptoKey";

        private static readonly byte[] EncryptDecrypt = new byte[8] { 240, 8, 45, 29, 0, 76, 173, 59 };
        public static string Encrypt(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;
            string result = string.Empty;
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                des.IV = EncryptDecrypt;
                result = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                throw;
            }
            return result;
        }
        public static string Decrypt(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;
            string result = string.Empty;
            try
            {
                byte[] buffer = Convert.FromBase64String(s);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                des.IV = EncryptDecrypt;
                result = Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                throw;
            }
            return result;
        }
        public static void SetBackColor(string color, TextBox txtbox)
        {
            string[] rgb = color.Split(' ');
            txtbox.BackColor = System.Drawing.Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]), int.Parse(rgb[3]));
        }
        public static String CustomError(string message)
        {
            try
            {
                string errormessage = message;

                if (message.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible."))
                {
                    errormessage = "Please check you internet connection." + Environment.NewLine +
                                   "You either lost your connection or" + Environment.NewLine +
                                   "Your internet is too slow." + Environment.NewLine +
                                   "Try again!";
                }

                return errormessage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static byte[] GetImage(System.Windows.Forms.PictureBox pbPigenPicture)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (pbPigenPicture.Image != null)
                {
                    pbPigenPicture.Image.Save(ms, pbPigenPicture.Image.RawFormat);
                }

                byte[] image = ms.GetBuffer();
                return image;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public enum MemberMenu
        {
            MemberDataEntry,
            Renewal,
            MemberMasterlist,
            RaceEntry,
            RaceResult,
            RaceResults,
            Location,
            Club,
            Schedule,
            RaceScheduleDetails,
            RaceCategory,
            RaceCategoryGroup,
            User,
            RingMangement,
            Region,
            Calculator,
            InboxView,
            ByReleasePoint,
            ByMemberID,
            StickerGeneration,
            Transaction,
            TransactionHistory,
            ViewClubRace,
            ExportEClockData,
            LoadMAVCAccessCard,
            PaymentHistory,
            ChangePassword
        }
        public enum RaceEntryClassType
        {
            RaceSchedule,
            RaceScheduleCategory,
            RaceReleasePoint,
            Member,
            RaceCategory,
            Entry,
            Location
        }
        public enum RaceResult
        {
            RaceResult,
            RaceCategory,
            RaceScheduleCategory,
            RaceSchedule
        }
        public enum RaceScheduleDetails
        { 
            Location,
            RaceScheduleDetails,
            Member
        }
        public enum ApplyBusinessRules
        {
            Save,
            Delete,
            Clear,
        }
        public enum ReportGeneration
        {
            Masterlist,
            ResultSummary,
            RaceResult,
            ScheduleDetails
        }
        public enum RingManagement
        {
            Schedule,
            Member,
            RaceScheduleCategory
        }

        public enum Location
        {
            Location,
            Region
        }

        public enum Schedule
        {
            Schedule,
            Region
        }
    }
}
