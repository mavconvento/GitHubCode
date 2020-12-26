using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL_Class;
using System.Threading;

namespace SMSSendingQue
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            //Restart:
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;

                SMSBIZ SMS = new SMSBIZ();

                string mobileNumber = System.IO.File.ReadAllText(path + @"\mobileNumber.txt");
                string message = System.IO.File.ReadAllText(path + @"\\message.txt");

                var numberCollection = mobileNumber.Split(';');

                Console.WriteLine("Total Number :" + numberCollection.Length.ToString());
                Console.WriteLine("SMS Broadcast Started..");

                var counter = 1;
                foreach (var number in numberCollection)
                {
                    SMS.SendSMS(number, message, false, "-1", "Broadcast");
                    Console.WriteLine("Sequence No.:" + counter);
                    Console.WriteLine("Sending to mobile number :" + number);
                    Thread.Sleep(2000);
                    counter++;
                }

                Console.WriteLine("SMS Broadcast Complete..");
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception)
            {
                //var path = AppDomain.CurrentDomain.BaseDirectory;
                //var a = Process.Start(path + "\\SMSSendingQue.exe");
                //goto Restart;
            }
        }
    }
}
