using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;
using System.IO;

namespace Eclock.BIZ
{
    public class Common
    {
        private const string cryptoKey = "cryptoKey";

        public String BaudRate { get; set; }
        public String PortNumber { get; set; }
        public String Drive { get; set; }
        public String RFIDKey { get; set; }
        public String RFIDKeybackup { get; set; }
        public String Mode { get; set; }

        public void OpenForm(Form child, Form Parent)
        {
            child.Owner = Parent;
            Parent.Visible = false;
            child.ShowDialog();

        }
        public void CloseSubForm(Form child)
        {
            child.Owner.Visible = true;
        }
        public DataSet EclockSetupSave()
        {
            try
            {
                DAL.Common common = new DAL.Common();
                return common.ElockSetupSave(this);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet ReadKeyID(string KeyIDTypeAction)
        {

            try
            {
                DAL.Common common = new DAL.Common();
                return common.ReadRFIDKey(KeyIDTypeAction);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static readonly byte[] EncryptDecrypt = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };
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
        public static DriveInfo GetEclockSDCardDriveInfo()
        {
            foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
            {
                if (driveInfo.DriveType.ToString().ToUpper() != "CDROM")
                {
                    if (driveInfo.VolumeLabel == "ECLOCK")
                    {
                        return driveInfo;
                    }
                }
            }
            return null;
        }
        public static string GetApplicationDirectory()
        {
            try
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string GetSystemConfigValue(string type)
        {
            try
            {
                string sysDir = "";
                string fullpath = "";
                string systemName = "";
                string systemType = "";
                string systemVersion = "";
                string assignedTo = "";
                string smsactivated = "";
                string mobileNumber = "";
                string readerID = "";
                string clubname = "";
                string value = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                fullpath = sysDir + "SystemConfig.inf";

                if (File.Exists(fullpath))
                {
                    TextReader tr = new StreamReader(fullpath);
                    using (tr)
                    {
                        systemName = tr.ReadLine();
                        systemVersion = tr.ReadLine();
                        clubname = tr.ReadLine();
                        systemType = tr.ReadLine();
                        readerID = tr.ReadLine();
                        assignedTo = tr.ReadLine();
                        smsactivated = tr.ReadLine();
                        if (smsactivated == "SMS Activated") mobileNumber = tr.ReadLine();
                    }
                }

                switch (type)
                {
                    case "systemType": 
                        value = systemType;
                        break;
                    case "mobileNumber":
                        value = mobileNumber;
                        break;
                    case "smsactivated":
                        value = smsactivated;
                        break;
                    default:
                        break;
                }
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Boolean CheckSystemConfig(string FileName)
        {
            try
            {

                string sysDir = "";
                string fullpath = "";

                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                fullpath = sysDir + FileName + ".inf";

                if (!File.Exists(fullpath))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Boolean GetSystemConfig(string FileName)
        {
            try
            {
                string sysDir = "";
                string fullpath = "";

                DriveInfo driveInfo = BIZ.Common.GetEclockSDCardDriveInfo();
                if (driveInfo != null)
                {
                    sysDir = AppDomain.CurrentDomain.BaseDirectory;
                    fullpath = sysDir + FileName + ".inf";
                    File.Copy(driveInfo.RootDirectory + "Eclock\\" + FileName + ".inf", fullpath, true);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
        public static DataTable ConvertTextDataToTable(string filepath)
        {

            try
            {
                DataTable dt = new DataTable();
                Boolean Istitle = false;

                if (File.Exists(filepath))
                {
                    TextReader tr = new StreamReader(filepath);

                    using (tr)
                    {
                        //Set Header of Datatable;
                        string[] Header = tr.ReadLine().Split('|');
                        foreach (string item in Header)
                        {
                            dt.Columns.Add(item);
                        }

                        string readline = "";
                        do
                        {
                            readline = tr.ReadLine();
                            if (readline != null)
                            {
                                string[] content = Common.Decrypt(readline).Split('|');
                                DataRow dr = dt.NewRow();
                                for (int i = 0; i < content.Length; i++)
                                {
                                    dr[i] = content[i];
                                }

                                dt.Rows.Add(dr);
                            }
                        } while (readline != null);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static String Speed(Decimal Distance, TimeSpan Flight)
        {
            try
            {
                Decimal flightToMinutes = Convert.ToDecimal((TimeSpan.Parse(Flight.ToString()).TotalMinutes).ToString("N3"));
                String speed = (Distance * 1000 / flightToMinutes).ToString("N3");

                return speed;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static TimeSpan Flight(DateTime ReleaseTime, DateTime ArrivalTime, DateTime TimeStopFrom, DateTime TimeStopTo, Boolean IsStop = false)
        {
            try
            {
                DateTime TimeArrival = ArrivalTime;

                if (IsStop)
                {

                    if (TimeStopTo.Subtract(ArrivalTime).TotalSeconds < 0)
                    {
                        TimeSpan stopStimeSpan = TimeStopTo.Subtract(TimeStopFrom);
                        TimeArrival = ArrivalTime.Subtract(stopStimeSpan); //less the span stoptime to calculate flight
                    }
                    else if (TimeStopFrom.Subtract(ArrivalTime).TotalSeconds < 0)
                    {
                        TimeArrival = TimeStopTo; //time will be the sunrise on day 2
                    }

                }

                TimeSpan flight = TimeArrival.Subtract(ReleaseTime);
                return flight;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
