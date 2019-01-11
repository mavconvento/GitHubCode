using System;
using System.Data;
using SMSWindowService.Entity;
using System.IO;
using DataAccess.IntegrationInbox;
using DataAccess;

namespace Integrate_Inbox
{
    class Program
    {
        static void Main(string[] args)
        {
            SMSComponent smscomponent = new SMSComponent();
            smscomponent = SMSWindowService.Entity.Config.GetSMSComponent();
            DataAccess.IntegrationInbox.IntegrateInboxDAL integrateInbox = new DataAccess.IntegrationInbox.IntegrateInboxDAL();
            DataSet dtSMSModemID = new DataSet();

            //string modemid = "";
            string modemIDValue = smscomponent.ModemID;
            string integrationType = smscomponent.IntegrationType;
            //int counter = 0;

            Integrate_Inbox(modemIDValue, integrationType);
        }

        private static void Integrate_Inbox(string modemID, string integrationType = "1")
        {
            try
            {
                x:
                Console.WriteLine("Start Integrate Receiver :" + modemID);

                if (integrationType == "1")
                {
                    if (Common.GetConnStringTypeSource() == "local")
                    {
                        LocalStorageToWeb(modemID);
                    }
                    else
                        LocalToWeb(modemID);
                }
                else if (integrationType == "2")
                {
                    WebToLocal(modemID);
                }
                else
                {
                    goto x;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
            }
        }

        private static void WebToLocal(string modemID)
        {
            error:
            try
            {
                int a = 1;
                string id = "0";
                string ReplyMessage = "";
                string Keyword = "";
                do
                {
                    DataSet dtResult = new DataSet();
                    IntegrateInboxDAL integrateInbox = new IntegrateInboxDAL();

                    dtResult = integrateInbox.GetInbox("web", modemID);
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        ReplyMessage = "";
                        Keyword = "";

                        Console.WriteLine(modemID + ":Start Inbox Integration from Web to Local");
                        Console.WriteLine("Start Time: " + DateTime.Today.ToShortDateString() + " " + DateTime.Today.ToLongTimeString());
                        foreach (DataRow item in dtResult.Tables[0].Rows)
                        {
                            id = item["ID"].ToString();

                            integrateInbox.SaveInboxLocal("local", item["SMSID"].ToString(), item["SMSContent"].ToString(), item["Sender"].ToString(), item["SMSDate"].ToString(), item["SMSTime"].ToString(), item["ActivationCode"].ToString(), item["ModemID"].ToString(), item["IsProcess"].ToString(), "WEB", item["ReplyMessage"].ToString(), item["Keyword"].ToString());

                            integrateInbox.UpdateInboxImport("web", id, ReplyMessage, Keyword);
                        }
                        Console.WriteLine("Finished Inbox Integration from Web to Local. Process End Time: " + DateTime.Today.ToShortDateString() + " " + DateTime.Today.ToLongTimeString());
                    }
                } while (a < 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
                goto error;
            }
        }
        private static void LocalToWeb(string modemID)
        {
            error:
            try
            {
                int a = 1;
                string id = "0";
                string ReplyMessage = "";
                string Keyword = "";
                do
                {
                    DataSet dtResult = new DataSet();
                    IntegrateInboxDAL integrateInbox = new IntegrateInboxDAL();

                    dtResult = integrateInbox.GetInbox("local", modemID);
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        ReplyMessage = "";
                        Keyword = "";
                        Console.WriteLine(modemID + "Start Inbox Integration from Local to Web");
                        Console.WriteLine("Start Time: " + DateTime.Today.ToShortDateString() + " " + DateTime.Today.ToLongTimeString());
                        foreach (DataRow item in dtResult.Tables[0].Rows)
                        {
                            id = item["ID"].ToString();
                            integrateInbox.SaveInbox("web", item["SMSID"].ToString(), item["SMSContent"].ToString(), item["Sender"].ToString(), item["SMSDate"].ToString(), item["SMSTime"].ToString(), item["ActivationCode"].ToString(), item["ModemID"].ToString(), item["IsProcess"].ToString(), Common.GetSource(), out ReplyMessage, out Keyword);
                            integrateInbox.UpdateInboxImport("local", id, ReplyMessage, Keyword);
                        }
                        Console.WriteLine("Finished Inbox Integration from Local to Web. Process End Time: " + DateTime.Today.ToShortDateString() + " " + DateTime.Today.ToLongTimeString());
                    }
                } while (a < 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
                goto error;
            }

        }

        private static void LocalStorageToWeb(string modemID)
        {
            error:
            try
            {
                string id = "0";
                string ReplyMessage = "";
                string Keyword = "";
                do
                {
                    DataSet dtResult = new DataSet();
                    IntegrateInboxDAL integrateInbox = new IntegrateInboxDAL();

                    string sysDir = "";
                    Int64 counter = 1;
                    sysDir = AppDomain.CurrentDomain.BaseDirectory;
                    string line;
                    foreach (string file in Directory.EnumerateFiles(sysDir + @"\SMSStorage", "*.txt"))
                    {
                        System.IO.StreamReader filelog = new System.IO.StreamReader(file);
                        Console.WriteLine(modemID + ": Start Inbox Integration from Local to Web");
                        while ((line = filelog.ReadLine()) != null)
                        {
                            string[] item = line.Split('|');

                            id = item[0].ToString();
                            if (item.Length == 6 )
                            {
                                string[] content = item[1].ToString().Split(' ');
                                if (content.Length == 3)
                                {
                                    Decimal sticker;
                                    if (Decimal.TryParse(content[2].ToString(), out sticker))
                                    {
                                        integrateInbox.SaveInbox("web", item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString(), item[5].ToString(), item[5].ToString(), 1.ToString(), Common.GetSource(), out ReplyMessage, out Keyword);
                                    }
                                }
                            } 
                            counter++;
                        }
                        Console.WriteLine("Finished Inbox Integration from Local to Web");
                        filelog.Dispose();
                        File.Copy(file, sysDir + @"\ProcessLogs\InboxLogProcess_" + DateTime.Today.ToString("dd-MMM-yyyy") + ".txt", true);
                        File.Delete(file);
                    }
                } while (1 < 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
                goto error;
            }

        }
    }
}
