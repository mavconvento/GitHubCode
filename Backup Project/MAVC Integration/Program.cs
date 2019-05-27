using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MAVC_Integration
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data Integration for MAVC Clocking System");
            Console.WriteLine("Version 1.0");
            Console.WriteLine("");
            Start();
        }

        #region Variables

        #endregion

        private static void Start()
        {
            while (true)
            {
                try
                {
                    GetData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void GetData()
        {
            DataSet dtresult = new DataSet();
            MAVC_Integration.IntegrationBLL bll; bll = new IntegrationBLL();
            dtresult = bll.GetData();

            if (dtresult.Tables.Count > 0)
            {
                if (dtresult.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine("Processing : " + dtresult.Tables[0].Rows.Count + " Record.");
                    foreach (DataRow dt in dtresult.Tables[0].Rows)
                    {
                        bll.FileNotesID = dt["FileNotesID"].ToString();
                        bll.AccountName = dt["AccountName"].ToString();
                        bll.AccountID = dt["AccountID"].ToString();
                        bll.Action = dt["Action"].ToString();
                        bll.ClubID = dt["ClubID"].ToString();

                        bll.TransferRecord();
                        Console.WriteLine(".................................");
                        Console.WriteLine("File Notes ID : " + bll.FileNotesID);
                        Console.WriteLine("Ext.  Club ID : " + bll.ClubID);
                        Console.WriteLine(" Account Name : " + bll.AccountName);
                        Console.WriteLine("   Account ID : " + bll.AccountID);
                        Console.WriteLine("       Action : " + bll.Action);
                        Console.WriteLine("       Status : " + bll.Status);
                        Console.WriteLine("  External ID : " + bll.ExternalID);
                        Console.WriteLine(".................................");
                        Console.WriteLine(" ");

                        bll.FileNotesID = "0";
                        bll.AccountName = "";
                        bll.AccountID = "0";
                        bll.Action = "";
                        bll.ClubID = "0";
                        bll.Status = "";
                        bll.ExternalID = "0";
                        System.Threading.Thread.Sleep(300);
                    }
                    Console.WriteLine("End Processing Record.");
                }
            }

        }
    }
}
