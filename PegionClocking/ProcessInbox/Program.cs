using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessInbox
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessInbox();
        }

        private static void ProcessInbox()
        {
            try
            {
                int a = 1;
                DAL dal = new DAL();
                do
                {
                    Console.WriteLine("Processing Inbox now.....");
                    dal.ProcessInbox("local");
                    Console.WriteLine("");
                    Console.WriteLine("Processing Inbox Finished");
                } while (a < 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error");
            }
        }
    }
}
