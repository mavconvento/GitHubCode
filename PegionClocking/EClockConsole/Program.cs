using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace EClockConsole
{
    class Program
    {
        private const string cryptoKey = "cryptoKey";

        static void Main(string[] args)
        {
            StartReader();
        }

        private static void StartReader()
        {

        Start:
            try
            {

                Console.Clear();
                int a = 1;
                do
                {
                    EClockConsoleDAL dal = new EClockConsoleDAL();
                    DataSet dtResult = dal.GetBirdArrivedNotProcess("");

                    string id = "0";
                    //dtResult = integrateInbox.GetInbox("local", modemID);
                    if (dtResult.Tables[0].Rows.Count > 0)
                    {
                        Console.WriteLine("Start Integration from Local to Web");
                        foreach (DataRow item in dtResult.Tables[0].Rows)
                        {
                            id = item["ID"].ToString();
                            dal.EclockSaveReply(item["MobileNumber"].ToString(), item["RFID"].ToString() ,Convert.ToDateTime(item["SMSContent"]),"");
                            dal.UpdateBirdProcess(id, "_Web");
                        }
                        Console.WriteLine("Finished Inbox Integration from Local to Web");
                    }
                } while (a < 2);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
                goto Start;
            }
        }

    }
}
