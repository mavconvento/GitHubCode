using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MAVC_IntegrationV2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SyncData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SyncData()
        {
            try
            {
                int a = 1;

                Console.WriteLine("*****************************************");
                Console.WriteLine("Data Integration for MAVC Clocking System");
                Console.WriteLine("Vesion 1.0");
                Console.WriteLine("*****************************************");
                Console.WriteLine(" ");
                Console.WriteLine(" ");

                do
                {
                    Start();
                    System.Threading.Thread.Sleep(5000);
                } while (a > 0);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void Start()
        {
            try
            {
                IntegrationBLL bll = new IntegrationBLL();
                PrintTransfer(bll.GetFileNotes());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PrintTransfer(DataSet transferRecord)
        {
            try
            {
                string accountName = "";
                if (transferRecord.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine("Transfer Record Start");
                    Console.WriteLine("*********************");

                    foreach (DataRow item in transferRecord.Tables[0].Rows)
                    {

                        accountName = item["AccountName"].ToString();
                        Console.WriteLine("FileNotesID:", item["FileNotesID"].ToString());
                        Console.WriteLine("AccountName:", accountName);
                        Console.WriteLine("AccountID:", item["AccountID"].ToString());
                        Console.WriteLine("Action:", item["Action"].ToString());

                        switch (accountName)
                        {
                            case "BandNumber":
                                BLL.BandNumberBLL bandnumber = new BLL.BandNumberBLL();
                                bandnumber.GetDetails(item["FileNotesID"].ToString(), item["AccountID"].ToString(), item["Action"].ToString());
                                break;
                            case "Club":
                                //BLL.ClubBLL club = new BLL.ClubBLL();
                                //club.GetDetails(item["FileNotesID"].ToString(), item["AccountID"].ToString(), item["Action"].ToString());
                                break;
                        }

                    }

                    Console.WriteLine("*********************");
                    Console.WriteLine("Transfer Record End");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
