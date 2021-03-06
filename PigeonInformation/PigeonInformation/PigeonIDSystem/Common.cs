﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Helper;

namespace PigeonIDSystem
{
    //Design by Pongsakorn Poosankam
    public class Common
    {

        public static void SaveImageCapture(System.Drawing.Image image)
        {

            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "Image";// Default file name
            s.DefaultExt = ".Jpg";// Default file extension
            s.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            // Process save file dialog box results
            if (s.ShowDialog()==DialogResult.OK)
            {
                // Save Image
                string filename = s.FileName;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();

            }

        }

        public static void Logs(string ErrorMessage)
        {
            string path = ReadText.ReadFilePath("datapath");
            string dateString = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0');
            string errorLogs = path + @"\" + dateString + ".txt";

            if (File.Exists(errorLogs))
            {
                using (StreamWriter sw = File.AppendText(errorLogs))
                {
                    sw.WriteLine(ErrorMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(errorLogs))
                {
                    sw.WriteLine(ErrorMessage);
                }
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

        public static void CreateStorageFolder()
        {
            try
            {
                string path = ReadText.ReadFilePath("datapath");
                string images = path + @"\Images";
                if (!Directory.Exists(images))
                {
                    Directory.CreateDirectory(images);
                }

                string members = path + @"\Members";
                if (!Directory.Exists(members))
                {
                    Directory.CreateDirectory(members);
                }

                string pigeonDetails = path + @"\PigeonDetails";
                if (!Directory.Exists(pigeonDetails))
                {
                    Directory.CreateDirectory(pigeonDetails);
                }

                string pigeonList = path + @"\PigeonList";
                if (!Directory.Exists(pigeonList))
                {
                    Directory.CreateDirectory(pigeonList);
                }

                string pigeonMobileNumberList = path + @"\PigeonMobileList";
                if (!Directory.Exists(pigeonMobileNumberList))
                {
                    Directory.CreateDirectory(pigeonMobileNumberList);
                }

                string entryList = path + @"\Entry";
                if (!Directory.Exists(entryList))
                {
                    Directory.CreateDirectory(entryList);
                }

                string resultList = path + @"\Result";
                if (!Directory.Exists(resultList))
                {
                    Directory.CreateDirectory(resultList);
                }

                string pigeonLogs = path + @"\Logs";
                if (!Directory.Exists(pigeonLogs))
                {
                    Directory.CreateDirectory(pigeonLogs);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        public static string GetClub()
        {
            try
            {
                string sysDir = AppDomain.CurrentDomain.BaseDirectory;
                string filepath = sysDir + "club.txt";

                if (!File.Exists(filepath))
                {
                    frmSetClub clubform = new frmSetClub();
                    clubform.ShowDialog();
                }
                else
                {
                    if (File.Exists(filepath))
                    {
                        string[] clublist = ReadText.ReadTextFile(filepath);
                        return clublist[0];
                    }
                    else
                    {
                        return "";
                    }
                }

                return "";
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
