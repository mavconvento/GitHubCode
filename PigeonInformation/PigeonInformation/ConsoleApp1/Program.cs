using ArduinoUploader;
using ArduinoUploader.Hardware;
using Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Eclock eclock = new Eclock();
            string serialPort = eclock.GetPort();
            string[] ports = SerialPort.GetPortNames();
            string commPort = "";

            Console.WriteLine("Start Uploading Eclock Program.");
            foreach (var item in ports)
            {
                if (serialPort.Contains(item)) commPort = item;
            }

            string userName = Environment.UserName;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string programFileName = path + "Eclock.ino.mega.hex";

            if (File.Exists(programFileName) && commPort != "")
            {
                Console.WriteLine("Program Uploading...");
                var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    FileName = programFileName,
                    PortName = commPort,
                    ArduinoModel = ArduinoModel.Mega2560
                });

                uploader.UploadSketch();
                Console.WriteLine("Program Uploading Completed...");
            }
            else
            {
                if (commPort == "")
                {
                    Console.WriteLine("Check E-Clock connection.");
                }
                else
                { Console.WriteLine("Program File Missing."); }
                
            }
        }
    }
}
