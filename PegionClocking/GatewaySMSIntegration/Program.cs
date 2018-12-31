using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GatewaySMSIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessReply();
        }

        private static void ProcessReply()
        {
            start:
            try
            {
                DataSet dsResult = new DataSet();
                DataAccess.GatewaySMSIntegration.GatewaySMS gateway = new DataAccess.GatewaySMSIntegration.GatewaySMS();
                SMSWindowService.Entity.SMSComponent smscomponent = new SMSWindowService.Entity.SMSComponent();
               
                smscomponent = SMSWindowService.Entity.Config.GetSMSComponent();
                var modemID = smscomponent.ModemID;

                dsResult = gateway.GetRecordForReply("local", modemID, "");

                if (dsResult.Tables.Count > 0)
                {
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in dsResult.Tables[0].Rows)
                        {
                            string SMSContent = item["SMSContent"].ToString();
                            string Sender = item["Sender"].ToString();
                            string Keyword = item["Keyword"].ToString();
                            string ReplyMessage = item["ReplyMessage"].ToString();
                            string InboxID = item["InboxID"].ToString();

                            //send sms reply
                            string Status = PostMessageForReply(Sender, ReplyMessage);
                            Console.WriteLine("==============================================================");
                            Console.WriteLine("      ModemID: " + modemID);
                            Console.WriteLine("   SMSContent: " + SMSContent);
                            Console.WriteLine(" ReplyMessage: " + ReplyMessage);
                            Console.WriteLine("       Sender: " + Sender);
                            Console.WriteLine("      Keyword: " + Keyword);
                            Console.WriteLine("       Status: " + Status);
                            Console.WriteLine("==============================================================");

                            //save outbox into database
                            gateway.SMSGatewayOutboxSave("local", SMSContent, Keyword, Sender, Status, InboxID);
                        }
                    }
                }
                goto start;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto start;
            }
        }
        private static string PostMessageForReply(string MobileNumber, string ReplyMessage)
        {
            var ret = itexmo(MobileNumber, ReplyMessage, "PR-MARKA754822_4H5EX");
            var status = "";
            if (ret.ToString() == "0")
            {
                status = "success";
            }
            else
            {
                switch (ret.ToString())
                {
                    case "1":
                        status = "Invalid Number.";
                        break;
                    case "2":
                        status = "Number prefix not supported. Please contact us so we can add.";
                        break;
                    case "3":
                        status = "Invalid ApiCode.";
                        break;
                    case "4":
                        status = "Maximum Message per day reached. This will be reset every 12MN.";
                        break;
                    case "5":
                        status = "Maximum allowed characters for message reached.";
                        break;
                    case "6":
                        status = "System OFFLINE.";
                        break;
                    case "7":
                        status = "Expired ApiCode.";
                        break;
                    case "8":
                        status = "iTexMo Error. Please try again later.";
                        break;
                    case "9":
                        status = "Invalid Function Parameters.";
                        break;
                    case "10":
                        status = "Recipient's number is blocked due to FLOODING, message was ignored.";
                        break;
                    case "11":
                        status = "Recipient's number is blocked temporarily due to HARD sending (after 3 retries of sending and message still failed to send) and the message was ignored. Try again after an hour.";
                        break;
                    case "12":
                        status = "Invalid request. You can't set message priorities on non corporate apicodes.";
                        break;
                    case "13":
                        status = "Invalid or Not Registered Custom Sender ID.";
                        break;
                    case "14":
                        status = "Invalid preferred server number.";
                        break;
                    default:
                        break;
                }
            }
            return status;
        }

        //########################################################################################
        //iTexmo API for C# / ASP --> go to www.itexmo.com/developers.php for API Documentation
        //########################################################################################
        private static object itexmo(string Number, string Message, string API_CODE, Boolean isImportant = false)
        {
            object functionReturnValue = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = "https://www.itexmo.com/php_api/api.php";
                parameter.Add("1", Number);
                parameter.Add("2", Message);
                parameter.Add("3", API_CODE);

                if (isImportant)
                {
                    parameter.Add("5", "HIGH");
                }
                dynamic rpb = client.UploadValues(url, "POST", parameter);
                functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
            }
            return functionReturnValue;
        }
        //########################################################################################
        //API END     '###########################################################################
        //########################################################################################
    }
}
