using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StandAloneApi.Model;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace StandAloneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        public static IWebHostEnvironment _environement;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _environement = webHostEnvironment;
        }

        [HttpPost("[action]")]
        public async Task<string> UploadProfile([FromForm]Files files)
        {
            try
            {
                if (files.file.Length > 0)
                {
                    if (!Directory.Exists(_environement.WebRootPath + "\\Images\\Profile\\"))
                    {
                        Directory.CreateDirectory(_environement.WebRootPath + "\\Images\\Profile\\");
                    }

                    string extension = Path.GetExtension(files.file.FileName);
                    using (FileStream fileStream = System.IO.File.Create(_environement.WebRootPath + "\\Images\\Profile\\" + files.filename + extension))
                    {
                        await files.file.CopyToAsync(fileStream);
                        fileStream.Flush();
                    };
                }

                return "upload";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
           
        }

    }
}
