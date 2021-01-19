using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository.Helper
{
    public class ImageUpload
    {
        public static byte[] GetImageAsBytes(IFormFile file)
        {
            //new memorystream
            using (var ms = new MemoryStream())
            {

                //copy bytes to memorystream
                file.CopyTo(ms);

                return ms.ToArray();
            }
        }
    }
}
