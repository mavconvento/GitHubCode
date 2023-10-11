using BusinessLayer;
using DomainObjects;
using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integrate_TopPigeon
{
    class Program
    {
        static async Task Main(string[] args)
        {
        start:
            try
            {
                string sysDir = "";
                string connectionString = "";
                sysDir = AppDomain.CurrentDomain.BaseDirectory;
                connectionString = sysDir + @"\action" + ".txt";
                string action = "";

                if (File.Exists(connectionString))
                {
                    TextReader tr = new StreamReader(connectionString);
                    using (tr)
                    {
                        action = tr.ReadLine();
                    }
                }

                
                DataSet racecodeCollection = new DataSet();
                EclockEntryBLL eclockEntryBLL = new EclockEntryBLL();
                TopPigeonSyncDataBLL topPigeonSyncDataBLL = new TopPigeonSyncDataBLL();

                racecodeCollection = eclockEntryBLL.GetTopPigeonRaceCode();

                Console.WriteLine("Start Time :" + DateTime.Now.ToString());
                if (racecodeCollection.Tables.Count > 0)
                {
                    if (racecodeCollection.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in racecodeCollection.Tables[0].Rows)
                        {
                            Console.WriteLine("Processing Top Pigeon result for " + item["Clubname"].ToString());

                            string racecode = "";
                            if (action == "training")
                            {
                                racecode = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2) + DateTime.Now.Day.ToString().PadLeft(2,'0');
                            }
                            else
                            {
                                racecode = item["RaceCode"].ToString();
                            }

                            TopPigeonAPIResponce result = await GetTopPigeonWebResult(racecode, Convert.ToDateTime(item["ReleaseDate"]).Year.ToString(), item["ClubId"].ToString(), action);

                            if (result.Data != null)
                            {
                                Console.WriteLine("Total Records :" + result.Data.Count.ToString());
                                int count = 0;
                                foreach (var iresult in result.Data)
                                {

                                    DataSet dataSet = new DataSet();
                                    dataSet = topPigeonSyncDataBLL.EclockReturnBirdSave(item["Clubname"].ToString(), iresult.deviceno, Convert.ToDateTime(iresult.backtime), iresult.ringno.Substring(0, iresult.ringno.Length - 2),action, false);
                                    count++;
                                    Console.WriteLine("Record No. :" + count);
                                    if (dataSet.Tables.Count > 0)
                                    {
                                        if (dataSet.Tables[0].Rows.Count > 0)
                                        {
                                            if (dataSet.Tables[0].Rows[0]["Remarks"].ToString() == "New Record")
                                            {
                                                ProcessResult(iresult, item["ClubName"].ToString());
                                                topPigeonSyncDataBLL.EclockReturnBirdSave(item["Clubname"].ToString(), iresult.deviceno, Convert.ToDateTime(iresult.backtime), iresult.ringno.Substring(0, iresult.ringno.Length - 2), action, true);
                                            }
                                            else
                                            {
                                                Console.WriteLine("----------------");
                                                Console.WriteLine("ClubName: " + item["Clubname"].ToString());
                                                Console.WriteLine("ClockID: " + iresult.deviceno);
                                                Console.WriteLine("Ring No: " + iresult.pring_no);
                                                Console.WriteLine("BackTime: " + iresult.backtime.ToString().Substring(2, 17));
                                                Console.WriteLine("Already Process");
                                                Console.WriteLine("----------------");
                                            }
                                        }
                                    }
                                }
                            }

                            eclockEntryBLL.TopPigeonRaceCodeSave(new TopPigeonRaceCode() { RaceCode = item["RaceCode"].ToString(), Action = "UpdateBacktime", ClubID = item["ClubId"].ToString(), LastBackTime = DateTime.Now, ReleaseDate = Convert.ToDateTime(item["ReleaseDate"]) });
                        }
                    }
                }

                Console.WriteLine("End Time :" + DateTime.Now.ToString());
                string delay = GetSynDataDelay();
                Console.WriteLine("Sync Data Top pigeon delay :" + delay);
                Thread.Sleep(Convert.ToInt32(delay));
                Console.Clear();
                goto start;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto start;
            }

        }

        private static void ProcessResult(ApiResponse result, string clubname)
        {
            try
            {
                ResultBLL resultBLL = new ResultBLL();

                //sample ECLOCK 0001 15204188 19/07/05 07:48:18
                String ResultStringFormat = "ECLOCK " + result.deviceno + " " + result.ringno.Substring(0, result.ringno.Length-2) + " " +  result.backtime.ToString().Substring(2, 17);
                DomainObjects.Result dObject = new DomainObjects.Result
                {
                    ClubName = clubname,
                    SMSContent = ResultStringFormat,
                    ActionFrom = "E-Clock Apps"
                };

                DataSet dtResult = new DataSet();
                dtResult = resultBLL.EclockResultSave(dObject);

                if (dtResult.Tables.Count > 0)
                {
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        String Remarks = dtResult.Tables[0].Rows[0]["Remarks"].ToString();

                        Console.WriteLine("----------------");
                        Console.WriteLine("ClubName: " + clubname);
                        Console.WriteLine("ClockID: " + result.deviceno);
                        Console.WriteLine("Ring No: " + result.pring_no);
                        Console.WriteLine("BackTime: " + result.backtime.ToString().Substring(0, 19));
                        Console.WriteLine(Remarks);

                        if (dtResult.Tables.Count == 2)
                        {
                            if (dtResult.Tables[1].Rows.Count > 0)
                            {
                                Console.WriteLine(dtResult.Tables[1].Rows[0]["EclockReply"].ToString().Replace("\r", " "));
                            }
                        }
                        Console.WriteLine("----------------");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static async Task<TopPigeonAPIResponce> GetTopPigeonWebResult(string racecode, string year, string clubid, string action)
        {
            try
            {
                //racecode value is equal to date if training
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string filepath = path + "config.txt";
                string[] configCol = ReadText.ReadTextFile(filepath);
                string url;
                if (action == "training")
                {
                    url = @"https://www.topigeon.com/api/?act=get_train&traindate=" + racecode + "&raceyear=" + year + "&uname=" + configCol[1] + "&ukey=" + configCol[0] + "&clubno=" + clubid;

                }
                else 
                    url = @"https://www.topigeon.com/api/?act=get_race&raceno=" + racecode + "&raceyear=" + year + "&uname=" + configCol[1] + "&ukey=" + configCol[0] + "&clubno=" + clubid;
                
                
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    string result = response.Content.ReadAsStringAsync().Result;
                    TopPigeonAPIResponce resultCol = JsonConvert.DeserializeObject<TopPigeonAPIResponce>(result);
                    return resultCol;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static string GetSynDataDelay()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string filepath = path + "TPMavcIntegDelay.txt";
                string[] configCol = ReadText.ReadTextFile(filepath);

                return configCol[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
