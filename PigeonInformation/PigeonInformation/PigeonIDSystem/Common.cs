using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace PigeonIDSystem
{
    //Design by Pongsakorn Poosankam
    public class Common
    {

        public static void SaveImageCapture(System.Drawing.Image image)
        {

            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "Image";// Default file name
            s.DefaultExt = ".Jpg";// Default file extension
            s.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            // Process save file dialog box results
            if (s.ShowDialog()==DialogResult.OK)
            {
                // Save Image
                string filename = s.FileName;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();

            }

        }

        public static String CustomError(string message)
        {
            try
            {
                string errormessage = message;

                if (message.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible."))
                {
                    errormessage = "Please check you internet connection." + Environment.NewLine +
                                   "You either lost your connection or" + Environment.NewLine +
                                   "Your internet is too slow." + Environment.NewLine +
                                   "Try again!";
                }

                return errormessage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
