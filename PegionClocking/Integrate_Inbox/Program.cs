using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMSWindowService.Entity;

namespace Integrate_Inbox
{
    class Program
    {
        static void Main(string[] args)
        {
            SMSComponent smscomponent = new SMSComponent();
            smscomponent = SMSWindowService.Entity.Config.GetSMSComponent();
            IntegrateInboxDAL integrateInbox = new IntegrateInboxDAL();
            DataSet dtSMSModemID = new DataSet();

            string modemid = "";
            string modemIDValue = smscomponent.ModemID;
            string integrationType = smscomponent.IntegrationType;
            int counter = 0;

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
                        foreach (DataRow item in dtResult.Tables[0].Rows)
                        {
                            id = item["ID"].ToString();

                            integrateInbox.SaveInboxLocal("local", item["SMSID"].ToString(), item["SMSContent"].ToString(), item["Sender"].ToString(), item["SMSDate"].ToString(), item["SMSTime"].ToString(), item["ActivationCode"].ToString(), item["ModemID"].ToString(), item["IsProcess"].ToString(), "WEB", item["ReplyMessage"].ToString(), item["Keyword"].ToString());
                            integrateInbox.UpdateInboxImport("web", id, ReplyMessage,Keyword);
                        }
                        Console.WriteLine("Finished Inbox Integration from Web to Local");
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
                        foreach (DataRow item in dtResult.Tables[0].Rows)
                        {
                            id = item["ID"].ToString();
                            integrateInbox.SaveInbox("web", item["SMSID"].ToString(), item["SMSContent"].ToString(), item["Sender"].ToString(), item["SMSDate"].ToString(), item["SMSTime"].ToString(), item["ActivationCode"].ToString(), item["ModemID"].ToString(), item["IsProcess"].ToString(), Common.GetSource(), out ReplyMessage, out Keyword);
                            integrateInbox.UpdateInboxImport("local", id, ReplyMessage, Keyword);
                        }
                        Console.WriteLine("Finished Inbox Integration from Local to Web");
                    }
                } while (a < 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
                goto error;
            }

        }
    }
}
