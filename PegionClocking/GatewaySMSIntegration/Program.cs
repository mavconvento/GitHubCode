﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;

namespace GatewaySMSIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            //Delete_outgoing("PR-MARKA754822_4H5EX");
            ProcessReply().Wait();

            //PostMessageForReply("+639688530922","This number is already Registered to JOHNDEE V. CONVENTO  with MemberID :000253.Load: 353,003.00","sample sending").Wait();
        }

        private static async Task ProcessReply()
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
                            if (ReplyMessage != "")
                            {
                                string Status = await PostMessageForReply(Sender, ReplyMessage, SMSContent);
                                Console.WriteLine("==============================================================");
                                Console.WriteLine("      ModemID: " + modemID);
                                Console.WriteLine("   SMSContent: " + SMSContent);
                                //Console.WriteLine(" ReplyMessage: " + ReplyMessage);
                                Console.WriteLine("       Sender: " + Sender);
                                Console.WriteLine("      Keyword: " + Keyword);
                                Console.WriteLine("       Status: " + Status);
                                Console.WriteLine("    ReplyTime: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                Console.WriteLine("==============================================================");

                                //save outbox into database
                                gateway.SMSGatewayOutboxSave("local", SMSContent, Keyword, Sender, Status, InboxID);
                            }
                            else if (item["Keyword"].ToString() == "DELAYTEXT")
                            {
                                Console.WriteLine("==============================================================");
                                Console.WriteLine("      ModemID: " + modemID);
                                Console.WriteLine("   SMSContent: " + SMSContent);
                                //Console.WriteLine(" ReplyMessage: " + ReplyMessage);
                                Console.WriteLine("       Sender: " + Sender);
                                Console.WriteLine("      Keyword: " + Keyword);
                                Console.WriteLine("       Status: " + "DELAYTEXT");
                                Console.WriteLine("    ReplyTime: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                Console.WriteLine("==============================================================");
                            }
                            else
                            {
                                Console.WriteLine("==============================================================");
                                Console.WriteLine("      ModemID: " + modemID);
                                Console.WriteLine("   SMSContent: " + SMSContent);
                                //Console.WriteLine(" ReplyMessage: " + ReplyMessage);
                                Console.WriteLine("       Sender: " + Sender);
                                Console.WriteLine("      Keyword: " + Keyword);
                                Console.WriteLine("       Status: " + "FAILED");
                                Console.WriteLine("    ReplyTime: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                Console.WriteLine("==============================================================");
                            }


                            
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
        private static async Task<string> PostMessageForReply(string MobileNumber, string ReplyMessage, string SMSContent)
        {
            string SenderID = "ITS-MAVC";
            string email = "mavconvento@gmail.com";
            string password = "172227cv@M";
            //if (SMSContent.ToUpper().Contains("RMCOLR")) SenderID = "RMC-OLR";

            var ret = await itexmo(MobileNumber, ReplyMessage, "PR-MARKA754822_4H5EX", SenderID, email, password);

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
                        status = ret.ToString();
                        break;
                }
            }
            return status;
        }

        //########################################################################################
        //iTexmo API for C# / ASP --> go to www.itexmo.com/developers.php for API Documentation
        //########################################################################################
        private static async Task<string> itexmo(string Number, string Message, string API_CODE, string SenderID, string email, string Password, Boolean isImportant = false)
        {
            try
            {
                var itextmoparam = new ItextMoParameter() { ApiCode = API_CODE, Email = email, Message = Message, Password = Password, Recipients = new string[] { Number }, SenderId = SenderID };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(itextmoparam);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var url = "https://api.itexmo.com/api/broadcast";
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, data);
                    string result = response.Content.ReadAsStringAsync().Result;
                    var itxtres = JsonConvert.DeserializeObject<ItextMoResponse>(result);

                    if (!itxtres.Error)
                    {
                        return "0";
                    }

                    return itxtres.Message;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static object Delete_outgoing(string API_CODE)
        {
            object functionReturnValue = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = "https://www.itexmo.com/php_api/delete_outgoing_all.php";
                //parameter.Add("1", Number);
                //parameter.Add("2", Message);
                parameter.Add("apicode", API_CODE);


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
