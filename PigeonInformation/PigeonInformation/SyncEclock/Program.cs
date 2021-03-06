﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using BusinessLayer;

namespace SyncEclock
{
    class Program
    {
        public static String MemberID { get; set; }
        public static String DateRelease { get; set; }
        public static String DataPath { get; set; }
        public static String ClubName { get; set; }
        public static DateTime ReleaseDate { get; set; }
        static void Main(string[] args)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory;
            string filepath = path + "resultinfo.txt";
            string Actionfilepath = path + "Action.txt";

            string[] pigeonCollection = ReadText.ReadTextFile(filepath);
            string[] actionValue = ReadText.ReadTextFile(Actionfilepath);


            if (actionValue[0].ToUpper() == "RESULTRACE")
            {
                MemberID = pigeonCollection[0];
                DateRelease = pigeonCollection[1];
                DataPath = pigeonCollection[2].Trim();
                GetResult("RACE");

            }
            if (actionValue[0].ToUpper() == "RESULTTRAINING")
            {
                MemberID = pigeonCollection[0];
                DateRelease = pigeonCollection[1];
                DataPath = pigeonCollection[2].Trim();
                GetResult("TRAINING");

            }
            else if (actionValue[0].ToUpper() == "RESULTDB")
            {
                ClubName = pigeonCollection[0];
                DateRelease = pigeonCollection[1];
                DataPath = pigeonCollection[2].Trim();
                ReleaseDate = Convert.ToDateTime(pigeonCollection[3]);
                SyncResultToDatabase();
            }
            else if (actionValue[0].ToUpper() == "ENTRYDB")
            {
                ClubName = pigeonCollection[0];
                DateRelease = pigeonCollection[1];
                DataPath = pigeonCollection[2].Trim();
                ReleaseDate = Convert.ToDateTime(pigeonCollection[3]);
                SyncEntryToDatabase();
            }
        }

        private static void SyncResultToDatabase()
        {
            try
            {
                string path = DataPath;
                string dateString = DateRelease;

                string rootApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string resultDirectory = path + "result\\" + dateString;
                string resultLogs = rootApplicationDirectory + "resultlogs.txt";

                //delete log file
                if (File.Exists(resultLogs))
                {
                    File.Delete(resultLogs);
                }

                if (Directory.Exists(resultDirectory))
                {
                    String[] filelist = Directory.GetFiles(resultDirectory);

                    foreach (var item in filelist)
                    {
                        string filepath = item;
                        string[] filename = filepath.Split('\\');
                        if (File.Exists(filepath))
                        {
                            string[] resultList = ReadText.ReadTextFile(filepath);
                            string[] memberid = filename[filename.Length - 1].Split('.');

                            //result is value is RFID
                            foreach (var result in resultList)
                            {
                                string resultFileName = resultDirectory + "\\" + memberid[0] + "\\" + result + ".txt";
                                if (File.Exists(resultFileName))
                                {
                                    string[] resultdetails = ReadText.ReadTextFile(resultFileName);
                                    BusinessLayer.ResultBLL entryBll = new ResultBLL();

                                    //sample ECLOCK 0001 15204188 19/07/05 07:48:18
                                    String ResultStringFormat = "ECLOCK " + resultdetails[0] + " " + resultdetails[1] + " " + resultdetails[3] + " " + resultdetails[4];
                                    DomainObjects.Result dObject = new DomainObjects.Result
                                    {
                                        ClubName = ClubName,
                                        SMSContent = ResultStringFormat,
                                        ActionFrom = "E-Clock Apps"
                                    };

                                    DataSet dtResult = new DataSet();
                                    dtResult = entryBll.EclockResultSave(dObject);

                                    if (dtResult.Tables.Count > 0)
                                    {
                                        if (dtResult.Tables[0].Rows.Count > 0)
                                        {
                                            String Remarks = dtResult.Tables[0].Rows[0]["Remarks"].ToString();

                                            if (!Remarks.ToUpper().Contains("SUCCESS"))
                                            {
                                                //string[] rdetails = Remarks.Split('|');

                                                String LogContents = memberid[0] + "|" + result + "|" + resultdetails[3] + "|" + Remarks + "|";

                                                Console.WriteLine("----------------");
                                                Console.WriteLine(memberid[0]);
                                                Console.WriteLine(result);
                                                Console.WriteLine(resultdetails[0]);
                                                Console.WriteLine(Remarks);
                                                Console.WriteLine("----------------");

                                                if (File.Exists(resultLogs))
                                                {
                                                    using (StreamWriter sw = File.AppendText(resultLogs))
                                                    {
                                                        sw.WriteLine(LogContents);
                                                    }
                                                }
                                                else
                                                {
                                                    using (StreamWriter sw = File.CreateText(resultLogs))
                                                    {
                                                        sw.WriteLine(LogContents);
                                                    }
                                                }
                                            }
                                            else
                                            {

                                                string[] rdetails = Remarks.Split('|');

                                                //String LogContents = memberid[0] + "|" + result + "|" + resultdetails[3] + "|" + Remarks + "|";

                                                Console.WriteLine("----------------");
                                                Console.WriteLine(memberid[0]);
                                                Console.WriteLine(result);
                                                Console.WriteLine(resultdetails[0]);
                                                Console.WriteLine(rdetails[1]); //distance
                                                Console.WriteLine(rdetails[2]); //flight
                                                Console.WriteLine(rdetails[3]); //speed
                                                Console.WriteLine(rdetails[0]); //remarks
                                                Console.WriteLine("----------------");


                                                if (File.Exists(resultFileName))
                                                {
                                                    string[] resultdetail = new string[] {rdetails[2], rdetails[3]};
                                                    resultdetails = resultdetails.Concat(resultdetail).Distinct().ToArray();

                                                    //string[] datacol = { MemberID, item, value[1], value[2], value[3] };
                                                    System.IO.File.WriteAllLines(resultFileName, resultdetails); //memberpigeonlist

                                                    //using (StreamWriter sw = File.AppendText(resultFileName))
                                                    //{
                                                    //    sw.WriteLine(rdetails[2]);
                                                    //}

                                                    //using (StreamWriter sw = File.AppendText(resultFileName))
                                                    //{
                                                    //    sw.WriteLine(rdetails[3]);
                                                    //}
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void SyncEntryToDatabase()
        {
            try
            {
                string path = DataPath;
                string dateString = DateRelease;

                string rootApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string entryDirectory = path + "entry\\" + dateString;
                String EntryLogs = rootApplicationDirectory + "\\entrylogs.txt";

                //delete log file
                if (File.Exists(EntryLogs))
                {
                    File.Delete(EntryLogs);
                }

                if (Directory.Exists(entryDirectory))
                {
                    String[] filelist = Directory.GetFiles(entryDirectory);

                    foreach (var item in filelist)
                    {
                        string filepath = item;
                        string[] filename = filepath.Split('\\');
                        if (File.Exists(filepath))
                        {
                            string[] entryList = ReadText.ReadTextFile(filepath);
                            string[] filenameValue = filename[filename.Length - 1].Split('.');
                            foreach (var entry in entryList)
                            {
                                string bandedFileName = path + "PigeonDetails\\" + filenameValue[0] + "\\" + entry + ".txt";
                                string MobileFileName = path + "\\PigeonMobileList\\" + entry + ".txt";
                                if (File.Exists(bandedFileName))
                                {
                                    string[] pigeondetails = ReadText.ReadTextFile(bandedFileName);
                                    BusinessLayer.EclockEntryBLL entryBll = new EclockEntryBLL();
                                    DomainObjects.Entry entryObject = new DomainObjects.Entry();
                                    entryObject.Clubname = ClubName;
                                    entryObject.MemberIDNo = filenameValue[0];
                                    entryObject.ReleaseDate = ReleaseDate;
                                    entryObject.RingNumber = pigeondetails[0];
                                    entryObject.RaceCategoryName = pigeondetails[2];
                                    entryObject.RaceCategoryGroupName = "EClock";
                                    entryObject.RFID = entry;
                                    entryObject.MobileNumber = "";

                                    if (File.Exists(MobileFileName))
                                    {
                                        string[] pigeonMobileCollection = ReadText.ReadTextFile(MobileFileName);
                                        string[] values = pigeonMobileCollection[0].ToString().Split('|');
                                        entryObject.MobileNumber = values[1].ToString().Trim();
                                    }

                                    DataSet dtResult = new DataSet();
                                    dtResult = entryBll.EclockEntrySave(entryObject);

                                    if (dtResult.Tables.Count > 0)
                                    {
                                        if (dtResult.Tables[0].Rows.Count > 0)
                                        {
                                            String Remarks = dtResult.Tables[0].Rows[0]["Remarks"].ToString();

                                            if (Remarks.ToUpper() != "SUCCESS")
                                            {
                                                String LogContents = filenameValue[0] + "|" + entry + "|" + pigeondetails[0] + "|" + Remarks + "|";

                                                Console.WriteLine("----------------");
                                                Console.WriteLine(filenameValue[0]);
                                                Console.WriteLine(entry);
                                                Console.WriteLine(pigeondetails[0]);
                                                Console.WriteLine(Remarks);
                                                Console.WriteLine("----------------");

                                                if (File.Exists(EntryLogs))
                                                {
                                                    using (StreamWriter sw = File.AppendText(EntryLogs))
                                                    {
                                                        sw.WriteLine(LogContents);
                                                    }
                                                }
                                                else
                                                {
                                                    using (StreamWriter sw = File.CreateText(EntryLogs))
                                                    {
                                                        sw.WriteLine(LogContents);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("----------------");
                                                Console.WriteLine(filenameValue[0]);
                                                Console.WriteLine(entry);
                                                Console.WriteLine(pigeondetails[0]);
                                                Console.WriteLine(Remarks);
                                                Console.WriteLine("----------------");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void SyncTime()
        {
            Eclock eclock = new Eclock();
            string serialPort = eclock.GetPort();
            string[] ports = SerialPort.GetPortNames();
            string commPort = "";
            foreach (var item in ports)
            {
                if (serialPort.Contains(item)) commPort = item;
            }

            if (!String.IsNullOrEmpty(commPort))
            {

                eclock.SyncTime(commPort);
            }
        }

        private static void GetResult(string action)
        {
            try
            {
                Eclock eclock = new Eclock();
                string serialPort = eclock.GetPort();
                string[] ports = SerialPort.GetPortNames();
                string comPortNumber = "";
                string rtype = "";

                if (action == "TRAINING")
                {
                    rtype = "T";
                }

                foreach (var item in ports)
                {
                    if (serialPort.Contains(item)) comPortNumber = item;
                }

                SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
                if (!String.IsNullOrEmpty(comPortNumber))
                {

                    DataPath = DataPath.Trim();
                    string entryDirectory = DataPath + "entry\\" + DateRelease;
                    string filepath = entryDirectory + "\\" + MemberID + ".txt";

                    if (File.Exists(filepath))
                    {
                        string[] entryCollection = ReadText.ReadTextFile(filepath);

                        string resultDirectory = DataPath + "result\\" + DateRelease;
                        string memberDirectory = resultDirectory + "\\" + MemberID;
                        string filepathList = resultDirectory + "\\" + MemberID + ".txt";

                        if (File.Exists(filepathList))
                        {
                            File.Delete(filepathList);
                        }

                        eclock.SendData("$Stat$", comPort);
                        System.Threading.Thread.Sleep(1000);

                        foreach (var item in entryCollection)
                        {
                            bool transmit = false;
                            while (!transmit)
                            {
                                //eclock.SendData(, comPort);
                                String inComingData = eclock.ReceiveDataResult("$Race$" + rtype + item + "#", comPort);
                                if (inComingData != "")
                                {
                                    PrintData(inComingData, item);
                                    //eclock.SendData("Done" + item + "#", commPort);
                                    transmit = true;
                                }
                            }
                        }

                        eclock.SendData("$Done$|#", comPort);

                    }

                }
                //Console.ReadLine();
                Console.WriteLine("Result Sync Completed....");
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void PrintData(string data, string item)
        {
            Console.WriteLine();
            String[] value = data.Split('|');
            if (value[1] != "noresult")
            {
                Console.WriteLine("**************************");
                Console.WriteLine("Ring Number: " + value[1]);
                Console.WriteLine("Arrival Date: " + value[2]);
                Console.WriteLine("Arrival Time: " + value[3]);
                Console.WriteLine("**************************");

                string entryDirectory = DataPath + "Result\\" + DateRelease;
                string memberDirectory = entryDirectory + "\\" + MemberID;
                string filepath = entryDirectory + "\\" + MemberID + "\\" + item + ".txt";
                string filepathList = entryDirectory + "\\" + MemberID + ".txt";

                if (!Directory.Exists(entryDirectory))
                {
                    Directory.CreateDirectory(entryDirectory);
                }

                if (!Directory.Exists(memberDirectory))
                {
                    Directory.CreateDirectory(memberDirectory);
                }

                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                if (File.Exists(filepathList))
                {
                    using (StreamWriter sw = File.AppendText(filepathList))
                    {
                        sw.WriteLine(item);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(filepathList))
                    {
                        sw.WriteLine(item);
                    }
                }



                string[] datacol = { MemberID, item, value[1], value[2], value[3] };
                System.IO.File.WriteAllLines(filepath, datacol); //memberpigeonlist
            }
            else
            {
                Console.WriteLine("No Result");
            }

        }
    }
}
