using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using BusinessLayer;
using DomainObjects;
using System.Net.Http;
using Newtonsoft.Json;

namespace SyncEclock
{
    class Program
    {
        public static String MemberID { get; set; }
        public static String MemberName { get; set; }
        public static String DateRelease { get; set; }
        public static String DataPath { get; set; }
        public static String ClubName { get; set; }
        public static string RaceCode { get; set; }
        public static string ClubID { get; set; }
        public static DateTime ReleaseDate { get; set; }
        public static String IsCopyLastCategory { get; set; }
        public static String TestRing { get; set; }
        static void Main(string[] args)
        {
        tryagain:
            try
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
                else if (actionValue[0].ToUpper() == "TOPPIGEONPIGDATA")
                {
                    ClubName = pigeonCollection[0];
                    SyncTopPigeonPigData(ClubName);

                }
                else if (actionValue[0].ToUpper() == "TOPPIGEONPIGRACEDATA")
                {
                    ClubName = pigeonCollection[0];
                    RaceCode = pigeonCollection[1];
                    ReleaseDate = Convert.ToDateTime(pigeonCollection[2]);
                    SyncTopPigeonPigRaceDataToMavc();

                }
                else if (actionValue[0].ToUpper() == "RESULTTRAINING")
                {
                    MemberID = pigeonCollection[0];
                    DateRelease = pigeonCollection[1];
                    DataPath = pigeonCollection[2].Trim();
                    GetResult("TRAINING");

                }
                else if (actionValue[0].ToUpper() == "READBANDED")
                {
                    MemberID = pigeonCollection[0];
                    MemberName = pigeonCollection[1];
                    TestRing = pigeonCollection[2];
                    DataPath = pigeonCollection[3].Trim();
                    ReadBandedFromEclock();

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
                    ClubID = pigeonCollection[1];
                    RaceCode = pigeonCollection[2];
                    DateRelease = pigeonCollection[3];
                    IsCopyLastCategory = pigeonCollection[4];
                    DataPath = pigeonCollection[5].Trim();
                    ReleaseDate = Convert.ToDateTime(pigeonCollection[6]);
                    SyncEntryToDatabase();
                }

                DataPath = DataPath + "\\";
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("A network-related or instance-specific"))
                {
                    Console.WriteLine("Network Problem Detected. Please try again!");
                    Console.WriteLine("Enter any key to try again.");
                    Console.ReadLine();
                    goto tryagain;
                }
                else
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }       
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


                BusinessLayer.ResultBLL entryBll = new ResultBLL();

                ResultBLL resultBLL = new ResultBLL();
                DataTable dtTopPigeonResult = new DataTable();

                dtTopPigeonResult = resultBLL.GetResultListByDate(ReleaseDate);

                foreach (DataRow item in dtTopPigeonResult.Rows)
                {

                    //sample ECLOCK 0001 15204188 19/07/05 07:48:18
                    String ResultStringFormat = "ECLOCK " + item["ClockId"].ToString() + " " + item["E_Ring"].ToString() + " " + item["Backtime"].ToString().Substring(2, 17);
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
                                if (Remarks.Contains("Bird Already Clock")) Remarks = "";
                                String LogContents = item["loftno"].ToString() + "|" + item["PRingNo"].ToString() + "|" + item["Backtime"].ToString().Substring(2, 17) + "|" + Remarks + "|";

                                Console.WriteLine("----------------");
                                Console.WriteLine("MemberID:" + item["loftno"].ToString());
                                Console.WriteLine("Ring No:" + item["PRingNo"].ToString());
                                Console.WriteLine("BackTime:" + item["Backtime"].ToString().Substring(2, 17));
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
                                Console.WriteLine("----------------");
                                Console.WriteLine("MemberID:" + item["loftno"].ToString());
                                Console.WriteLine("Ring No:" + item["PRingNo"].ToString());
                                Console.WriteLine("BackTime:" + item["Backtime"].ToString().Substring(2, 17));
                                Console.WriteLine(Remarks);
                                Console.WriteLine("----------------");
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
                //save racecode in database
                TopPigeonRaceCodeSave("");

                //manual entry
                SyncManualEntryToDatabase();

                //sync toppigeon entry
                SyncTopPigeonEntryToDatabase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void SyncTopPigeonPigRaceDataToMavc()
        {
            try
            {
                //batch time will indicate the batch of upload
                DateTime BatchDatetime = DateTime.Now;

                MemberBLL memberBLL = new MemberBLL();
                EclockEntryBLL entryBLL = new EclockEntryBLL();
                TopPigeonSyncDataBLL topPigeonSyncDataBLL = new TopPigeonSyncDataBLL();
                DataTable dt = new DataTable();
                dt = memberBLL.GetMemberWithMultipleClub();

                Console.WriteLine("Top Pigeon to Mavc Data sychronization process start....");
                foreach (DataRow item in dt.Rows)
                {
                    DataTable pigDataCol = new DataTable();
                    pigDataCol = entryBLL.GetEntryListByDateAndClockID(ReleaseDate, RaceCode.Substring(0, 4), item["ClockID"].ToString());
                    //sync pigeon list from top pigeon to mavc db
                    Console.WriteLine("Top Pigeon to Mavc Data sychronization");
                    foreach (DataRow pigRow in pigDataCol.Rows)
                    {
                        DomainObjects.TopPigeonPigRaceData pigData = new DomainObjects.TopPigeonPigRaceData()
                        {
                            ClockID = pigRow["ClockID"].ToString(),
                            ClubID = pigRow["ClubID"].ToString(),
                            PRingNo = pigRow["PRingNo"].ToString(),
                            RandomCode = Convert.ToInt32(pigRow["RandomCode"]),
                            LiberCode = pigRow["LiberCode"].ToString(),
                            LiberDate = Convert.ToDateTime(pigRow["LiberDate"]),
                            MarkedTime = Convert.ToDateTime(pigRow["MarkedTime"])
                        };


                        entryBLL.TopPigeonPigRaceDataSave(pigData);
                        Console.WriteLine("**************************");
                        Console.WriteLine("ClockID :" + pigData.ClockID.ToString());
                        Console.WriteLine("Ring Number: " + pigData.PRingNo.ToString());
                        Console.WriteLine("Race Code: " + pigData.LiberCode.ToString());
                        Console.WriteLine("Liber Date: " + pigData.LiberDate.ToString());
                        Console.WriteLine("Success");
                        Console.WriteLine("**************************");
                    }

                    //sync mavc db to toppigeon database
                    DataSet mavcpigDataCol = new DataSet();
                    mavcpigDataCol = entryBLL.GetTopPigeonPigRaceData(item["ClockId"].ToString(), ReleaseDate, RaceCode.Substring(0, 4));
                    if (mavcpigDataCol.Tables.Count > 0)
                    {
                        Console.WriteLine("mavc to Top Pigeon Data sychronization");
                        foreach (DataRow pigRow in mavcpigDataCol.Tables[0].Rows)
                        {
                            DomainObjects.TopPigeonPigRaceData pigData = new DomainObjects.TopPigeonPigRaceData()
                            {
                                ClockID = pigRow["ClockID"].ToString(),
                                ClubID = pigRow["ClubID"].ToString(),
                                PRingNo = pigRow["PRingNo"].ToString(),
                                RandomCode = Convert.ToInt32(pigRow["RandomCode"]),
                                LiberCode = pigRow["LiberCode"].ToString(),
                                LiberDate = Convert.ToDateTime(pigRow["LiberDate"]),
                                MarkedTime = Convert.ToDateTime(pigRow["MarkedTime"])
                            };

                            ProcessDataSavinginToppigeonRaceDataDB(pigData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void TopPigeonRaceCodeSave(string action)
        {
            try
            {
                TopPigeonRaceCode _racecode = new TopPigeonRaceCode() { ClubID = ClubID, Action = action, ClubName = ClubName, LastBackTime = DateTime.Now, RaceCode = RaceCode, ReleaseDate = ReleaseDate };
                EclockEntryBLL eclockEntryBLL = new EclockEntryBLL();
                eclockEntryBLL.TopPigeonRaceCodeSave(_racecode);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void SyncTopPigeonEntryToDatabase()
        {
            try
            {
                EclockEntryBLL entryBLL = new EclockEntryBLL();
                DataTable pigeons = new DataTable();
                pigeons = entryBLL.GetEntryListByDate(ReleaseDate, RaceCode.Substring(0, 4));

                foreach (DataRow item in pigeons.Rows)
                {
                    if (item["IsRegistered"].ToString() == "1")
                    {
                        ProcessEntrySaveToDatabase(item["LoftNo"].ToString(), item["PRingNo"].ToString(), item["E_ring"].ToString(), "TOP Pigeon");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void SyncManualEntryToDatabase()
        {
            try
            {
                MemberBLL memberBLL = new MemberBLL();
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
                                ProcessEntrySaveToDatabase(filenameValue[0], "", entry, "Manual Entry");
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

        private static void ProcessEntrySaveToDatabase(string memberid,string pringno, string e_ring, string source)
        {
            MemberBLL memberBLL = new MemberBLL();
            string path = DataPath;
            string dateString = DateRelease;

            DataTable dt = new DataTable();
            if (pringno == "")
            {
                dt = memberBLL.GetPigeonInfoByRingNo(e_ring, memberid);
                pringno = dt.Rows[0]["PRingNo"].ToString();
            }

            string rootApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string entryDirectory = path + "entry\\" + dateString;
            String EntryLogs = rootApplicationDirectory + "\\entrylogs.txt";

            BusinessLayer.EclockEntryBLL entryBll = new EclockEntryBLL();

            DomainObjects.Entry entryObject = new DomainObjects.Entry();
            entryObject.Clubname = ClubName;
            entryObject.MemberIDNo = memberid;
            entryObject.ReleaseDate = ReleaseDate;
            entryObject.RingNumber = pringno;  //dt.Rows[0]["PRingNo"].ToString();
            entryObject.RaceCategoryName = "None";
            entryObject.RaceCategoryGroupName = "EClock";
            entryObject.RFID = e_ring;  //dt.Rows[0]["E_ring"].ToString();
            entryObject.MobileNumber = "";
            entryObject.IsCopyLastCategory = IsCopyLastCategory;

            DataSet dtResult = new DataSet();
            dtResult = entryBll.EclockEntrySave(entryObject);

            if (dtResult.Tables.Count > 0)
            {
                if (dtResult.Tables[0].Rows.Count > 0)
                {
                    String Remarks = dtResult.Tables[0].Rows[0]["Remarks"].ToString();

                    if (Remarks.ToUpper() != "SUCCESS")
                    {
                        String LogContents = entryObject.MemberIDNo + "|" + entryObject.RFID + "|" + entryObject.RingNumber + "|" + Remarks + "|";

                        Console.WriteLine("----------------");
                        Console.WriteLine("MemberID :" + entryObject.MemberIDNo);
                        Console.WriteLine("E_Ring :" + e_ring);
                        Console.WriteLine("Band Number :" + entryObject.RingNumber);
                        Console.WriteLine("Remarks :" + Remarks);
                        Console.WriteLine("Source :" + source);
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
                        Console.WriteLine("MemberID :" + entryObject.MemberIDNo);
                        Console.WriteLine("E_Ring :" + e_ring);
                        Console.WriteLine("Band Number :" + entryObject.RingNumber);
                        Console.WriteLine("Remarks :" + Remarks);
                        Console.WriteLine("Source :" + source);
                        Console.WriteLine("----------------");
                    }
                }
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

        private static void SyncTopPigeonPigData(string source)
        {
            try
            {
                //batch time will indicate the batch of upload
                DateTime BatchDatetime = DateTime.Now;

                MemberBLL memberBLL = new MemberBLL();
                TopPigeonSyncDataBLL topPigeonSyncDataBLL = new TopPigeonSyncDataBLL();
                DataTable dt = new DataTable();
                dt = memberBLL.GetMemberWithMultipleClub();

                Console.WriteLine("Top Pigeon to Mavc Data sychronization process start....");
                foreach (DataRow item in dt.Rows)
                {
                    DataTable pigDataCol = new DataTable();
                    pigDataCol = memberBLL.GetPigeonList(item["loftNo"].ToString());
                    //sync pigeon list from top pigeon to mavc db
                    Console.WriteLine("Top Pigeon to Mavc Data sychronization");
                    foreach (DataRow pigRow in pigDataCol.Rows)
                    {
                        DomainObjects.TopPigeonPigData pigData = new DomainObjects.TopPigeonPigData()
                        {
                            ActiveStat = Convert.ToInt32(pigRow["ActiveStat"]),
                            PRingNo = pigRow["PRingNo"].ToString(),
                            RRegNumber = pigRow["RRegNumber"].ToString(),
                            RRegLetter = pigRow["RRegLetter"].ToString(),
                            AssignDate = Convert.ToDateTime(pigRow["AssignDate"]),
                            ClockId = item["ClockId"].ToString(),
                            ColorType = pigRow["ColorType"].ToString(),
                            Comment = pigRow["Comment"].ToString(),
                            CreateDate = Convert.ToDateTime(pigRow["CreateDate"]),
                            E_Ring = pigRow["E_Ring"].ToString(),
                            LoftName = item["LoftName"].ToString(),
                            LoftNo = item["loftNo"].ToString(),
                            OtherClub = Convert.ToInt32(String.IsNullOrEmpty(pigRow["OtherClub"].ToString()) ? 1 : 0),
                            RandomCode = Convert.ToInt32(pigRow["RandomCode"]),
                            RCountry = pigRow["RCountry"].ToString(),
                            RYear = pigRow["RYear"].ToString(),
                            Sex = pigRow["Sex"].ToString(),
                            Source = source,
                            SynchFlag = Convert.ToInt32(pigRow["SynchFlag"]),
                            UID = pigRow["UID"].ToString(),
                            Updatetime = Convert.ToDateTime(pigRow["Updatetime"]),
                            BatchDatetime = BatchDatetime
                        };


                        topPigeonSyncDataBLL.TopPigeonPigDataToMavcDB(pigData);
                        Console.WriteLine("**************************");
                        Console.WriteLine("ClockID :" + item["ClockId"].ToString());
                        Console.WriteLine("LoftName :" + item["LoftName"].ToString());
                        Console.WriteLine("Ring Number: " + pigRow["PRingNo"].ToString());
                        Console.WriteLine("E_Ring: " + pigRow["E_Ring"].ToString());
                        Console.WriteLine("UpdateTime: " + Convert.ToDateTime(pigRow["Updatetime"]).ToString());
                        Console.WriteLine("**************************");
                    }

                    //sync mavc db to toppigeon database
                    DataSet mavcpigDataCol = new DataSet();
                    mavcpigDataCol = topPigeonSyncDataBLL.GETTopPigeonDataByClockID(item["ClockId"].ToString(), BatchDatetime);
                    if (mavcpigDataCol.Tables.Count > 0)
                    {
                        Console.WriteLine("mavc to Top Pigeon Data sychronization");
                        foreach (DataRow pigRow in mavcpigDataCol.Tables[0].Rows)
                        {
                            DomainObjects.TopPigeonPigData pigData = new DomainObjects.TopPigeonPigData()
                            {
                                ActiveStat = Convert.ToInt32(pigRow["ActiveStat"]),
                                PRingNo = pigRow["PRingNo"].ToString(),
                                RRegNumber = pigRow["RRegNumber"].ToString(),
                                RRegLetter = pigRow["RRegLetter"].ToString(),
                                AssignDate = Convert.ToDateTime(pigRow["AssignDate"]),
                                ClockId = item["ClockId"].ToString(),
                                ColorType = pigRow["ColorType"].ToString(),
                                Comment = pigRow["Comment"].ToString(),
                                CreateDate = Convert.ToDateTime(pigRow["CreateDate"]),
                                E_Ring = pigRow["E_Ring"].ToString(),
                                LoftName = item["LoftName"].ToString(),
                                LoftNo = item["loftNo"].ToString(),
                                OtherClub = Convert.ToInt32(pigRow["OtherClub"]),
                                RandomCode = Convert.ToInt32(pigRow["RandomCode"]),
                                RCountry = pigRow["RCountry"].ToString(),
                                RYear = pigRow["RYear"].ToString(),
                                Sex = pigRow["Sex"].ToString(),
                                Source = pigRow["Source"].ToString(),
                                SynchFlag = Convert.ToInt32(pigRow["SynchFlag"]),
                                UID = pigRow["UID"].ToString(),
                                Updatetime = Convert.ToDateTime(pigRow["Updatetime"]),
                                BatchDatetime = BatchDatetime
                            };

                            ProcessDataSavinginToppigeonDB(pigData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ProcessDataSavinginToppigeonDB(DomainObjects.TopPigeonPigData pigData)
        {
            TopPigeonSyncDataBLL topPigeonSyncDataBLL = new TopPigeonSyncDataBLL();

            //check if pigeon exists in top pigeon db
            DataTable dtPigExists = new DataTable();
            dtPigExists = topPigeonSyncDataBLL.CheckPigeonExists(pigData);

            //if exists validate the data if updated
            if (dtPigExists.Rows.Count > 0)
            {
                DateTime updatetime = Convert.ToDateTime(dtPigExists.Rows[0]["Updatetime"]);
                DateTime lastUpdate = Convert.ToDateTime(pigData.Updatetime);

                if (lastUpdate > updatetime)
                {
                    //update record in toppigeon database from the mavc db
                    topPigeonSyncDataBLL.UpdatePigeonInTopPigeonDB(pigData, ClubName);
                    Console.WriteLine("**************************");
                    Console.WriteLine("ClockID :" + pigData.ClockId.ToString());
                    Console.WriteLine("Loft Name: " + pigData.LoftName.ToString());
                    Console.WriteLine("Ring Number: " + pigData.PRingNo.ToString());
                    Console.WriteLine("E_Ring: " + pigData.E_Ring.ToString());
                    Console.WriteLine("UpdateTime: " + pigData.Updatetime.ToString());
                    Console.WriteLine("Update Record");
                    Console.WriteLine("**************************");
                }
            }
            else
            {
                //insert the record in topigeon database  
                topPigeonSyncDataBLL.InsertPigeonInTopPigeonDB(pigData, ClubName);
                Console.WriteLine("**************************");
                Console.WriteLine("ClockID :" + pigData.ClockId.ToString());
                Console.WriteLine("Loft Name: " + pigData.LoftName.ToString());
                Console.WriteLine("Ring Number: " + pigData.PRingNo.ToString());
                Console.WriteLine("E_Ring: " + pigData.E_Ring.ToString());
                Console.WriteLine("UpdateTime: " + pigData.Updatetime.ToString());
                Console.WriteLine("New Record");
                Console.WriteLine("**************************");
            }
        }

        private static void ProcessDataSavinginToppigeonRaceDataDB(DomainObjects.TopPigeonPigRaceData pigData)
        {
            TopPigeonSyncDataBLL topPigeonSyncDataBLL = new TopPigeonSyncDataBLL();

            //check if pigeon exists in top pigeon db
            DataTable dtPigExists = new DataTable();
            dtPigExists = topPigeonSyncDataBLL.CheckPigeonRaceDataExists(pigData);

            //if exists validate the data if updated
            if (dtPigExists.Rows.Count > 0)
            {
                Console.WriteLine("**************************");
                Console.WriteLine("ClockID :" + pigData.ClockID.ToString());
                Console.WriteLine("Ring Number: " + pigData.PRingNo.ToString());
                Console.WriteLine("Race Code: " + pigData.LiberCode.ToString());
                Console.WriteLine("Liber Date: " + pigData.LiberDate.ToString());
                Console.WriteLine("Bird Already Marked");
                Console.WriteLine("**************************");
            }
            else
            {
                //insert the record in topigeon database  
                topPigeonSyncDataBLL.InsertPigeonRaceDataInTopPigeonDB(pigData);
                Console.WriteLine("**************************");
                Console.WriteLine("ClockID :" + pigData.ClockID.ToString());
                Console.WriteLine("Ring Number: " + pigData.PRingNo.ToString());
                Console.WriteLine("Race Code: " + pigData.LiberCode.ToString());
                Console.WriteLine("Liber Date: " + pigData.LiberDate.ToString());
                Console.WriteLine("Bird Marking Save");
                Console.WriteLine("**************************");
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

        private static void ReadBandedFromEclock()
        {
            try
            {
                Eclock eclock = new Eclock();
                string serialPort = eclock.GetPort();
                string[] ports = SerialPort.GetPortNames();
                string comPortNumber = "";

                foreach (var item in ports)
                {
                    if (serialPort.Contains(item)) comPortNumber = item;
                }

                SerialPort comPort = new SerialPort(comPortNumber, 9600, Parity.None, 8, StopBits.One);
                if (!String.IsNullOrEmpty(comPortNumber))
                {

                    DataPath = DataPath.Trim();

                    eclock.SendData("$Stat$", comPort);
                    System.Threading.Thread.Sleep(1000);

                    for (int i = 1; i <= 1500; i++)
                    {
                        bool transmit = false;
                        while (!transmit)
                        {
                            //eclock.SendData(, comPort);
                            String inComingData = eclock.ReceiveDataResult("$Read$" + i.ToString() + "#", comPort);

                            if (inComingData != "")
                            {
                                if (inComingData.Contains("noresult"))
                                    i = 1501;
                                else
                                    PrintReadData(inComingData, i.ToString());
                                transmit = true;
                            }
                        }
                    }

                    eclock.SendData("$Done$|#", comPort);
                }
                //Console.ReadLine();
                Console.WriteLine("Banded Sync Completed....");
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void PrintReadData(string data, string index)
        {
            Console.WriteLine();
            String[] value = data.Split('|');
            if (value[1] != "noresult")
            {
                Console.WriteLine("**************************");
                Console.WriteLine("Record # " + index);
                Console.WriteLine("Ring Number: " + value[1]);
                Console.WriteLine("RFID: " + value[2]);
                Console.WriteLine("Category: " + value[3]);
                Console.WriteLine("**************************");

                if (value[1] != "TEST RING") SavetoFile(value[1], value[2], value[3]);
            }
            else
            {
                Console.WriteLine("No Result");
            }

        }

        private static Boolean SavetoFile(string BandNumber, string RFID, string Category)
        {
            try
            {
                string path = DataPath.Trim();
                string detailspath = path + "\\pigeondetails\\" + MemberID + "\\" + RFID + ".txt";
                string memberpath = path + "\\members\\" + MemberID + ".txt";
                string pigeonlistpath = path + "\\pigeonlist\\" + MemberID + ".txt";


                string Sex = "NA";
                string Color = "NA";
                //string Category = "YB";


                string[] memberDetails = { MemberID, MemberName, TestRing };
                string[] pigeonDetails = { BandNumber, RFID, Category, Sex, Color };

                string[] pigeonList = SetPigeonList("", 0.ToString(), BandNumber, Sex, Color, RFID, Category, MemberID).ToArray();

                System.IO.File.WriteAllLines(memberpath, memberDetails); //memberdetails
                System.IO.File.WriteAllLines(pigeonlistpath, pigeonList); //memberpigeonlist

                string pigeondetailsDirectory = path + "\\pigeondetails\\" + MemberID;
                if (!Directory.Exists(pigeondetailsDirectory))
                {
                    Directory.CreateDirectory(pigeondetailsDirectory);
                }
                System.IO.File.WriteAllLines(detailspath, pigeonDetails); //pigeondetails


                string pigeonimageDirectory = path + "\\images\\" + MemberID;
                if (!Directory.Exists(pigeonimageDirectory))
                {
                    Directory.CreateDirectory(pigeonimageDirectory);
                }

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<string> SetPigeonList(string action, string PigeonID, string BandNumber, string Sex, string Color, string RFID, String Category, string memberid)
        {
            try
            {
                string path = DataPath.Trim();
                string pigeonlistpath = path + "\\pigeonlist\\" + memberid + ".txt";

                string[] pigeonCollection = ReadText.ReadTextFile(pigeonlistpath);

                List<string> pigeonListCollection = new List<string>();

                if (pigeonCollection != null)
                {
                    foreach (var item in pigeonCollection)
                    {
                        if (!item.Contains(RFID) && !item.Contains(BandNumber))
                        {
                            pigeonListCollection.Add(item);
                        }
                    }
                }

                int count = 1;
                if (pigeonListCollection.Count > 0) count = pigeonListCollection.Count();

                if (string.IsNullOrEmpty(action))
                {
                    pigeonListCollection.Add(count + "|" + BandNumber + "|" + RFID + "|" + Category + "|" + Color + "|" + Sex);
                }

                return pigeonListCollection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
